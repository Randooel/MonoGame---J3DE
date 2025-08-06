using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GeometryDrawer
{
    public class WindmillDrawer
    {
        private GraphicsDevice _graphicsDevice;

        // Moinho
        private VertexBuffer _vertexBuffer;
        private VertexPositionColor[] verts;

        // Hélices
        /*
        private VertexBuffer _hVertexBuffer;
        private VertexPositionColor[] hVerts;
        */

        // Matriz
        private Matrix _world = Matrix.Identity;
        private List<Matrix> _helixWorlds = new List<Matrix>();

        private List<float> _helixRotations = new List<float>();
        private List<float> _helixRotationSpeeds = new List<float>();
        private static readonly Random _rng = new Random();

        public void SetWorld(Matrix world) { _world = world; }

        public void AddHelix(Matrix helixWorld)
        {
            _helixWorlds.Add(helixWorld);
            _helixRotations.Add(0f);
            _helixRotationSpeeds.Add((float)(_rng.NextDouble() * 2 + 1)); // velocidade entre 1‑3 rad/s
        }

        public void SetWindmillInitialPos(Vector3 position, float rotationYDegrees, float scale)
        {
            Matrix windmillScale = Matrix.CreateScale(scale);
            Matrix windmillRotation = Matrix.CreateRotationY(MathHelper.ToRadians(rotationYDegrees));
            Matrix windmillTranslation = Matrix.CreateTranslation(position);

            Matrix windmillInitialTransform = windmillScale * windmillRotation * windmillTranslation;
            this.SetWorld(windmillInitialTransform);
        }

        public void AddHelixInitialPos(Vector3 position, float rotationYDegrees, float rotationZDegrees, float scale)
        {
            Matrix helixScale = Matrix.CreateScale(scale);
            Matrix helixRotation = Matrix.CreateRotationY(MathHelper.ToRadians(rotationYDegrees));
            Matrix helixRotationZ = Matrix.CreateRotationZ(MathHelper.ToRadians(rotationZDegrees));
            Matrix helixTranslation = Matrix.CreateTranslation(position);

            Matrix helixInitialTransform = helixScale * helixRotation * helixRotationZ * helixTranslation;
            AddHelix(helixInitialTransform);
        }

        public WindmillDrawer(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            CreateVertices();
            CreateBuffers();
        }

        // No geral, a mesma coisa com o cubo, só que com uma base traseira mais afastada
        // e topo mais alto
        // Em suma: SUBSTITUIR TODOS OS Y +1 por +4 e o Z -2 da traseira e lados por -4
        private void CreateVertices()
        {
            verts = new VertexPositionColor[36];

            //FRENTE DO CUBO
            verts[0] = new VertexPositionColor(new Vector3(-1, 2, 0), Color.Gray);
            verts[1] = new VertexPositionColor(new Vector3(1, 2, 0), Color.Gray);
            verts[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black);

            verts[3] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black);
            verts[4] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black);
            verts[5] = new VertexPositionColor(new Vector3(-1, 2, 0), Color.Gray);

            // TRASEIRA
            verts[6] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);
            verts[7] = new VertexPositionColor(new Vector3(1, 2, -2), Color.Gray);
            verts[8] = new VertexPositionColor(new Vector3(-1, 2, -2), Color.Gray);

            verts[9] = new VertexPositionColor(new Vector3(-1, -1, -4), Color.Black);
            verts[10] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);
            verts[11] = new VertexPositionColor(new Vector3(-1, 2, -2), Color.Gray);

            // TOPO
            verts[12] = new VertexPositionColor(new Vector3(1, 2, -2), Color.Gray);
            verts[13] = new VertexPositionColor(new Vector3(1, 2, 0), Color.Gray);
            verts[14] = new VertexPositionColor(new Vector3(-1, 2, 0), Color.Gray);

            verts[15] = new VertexPositionColor(new Vector3(-1, 2, -2), Color.Gray);
            verts[16] = new VertexPositionColor(new Vector3(1, 2, -2), Color.Gray);
            verts[17] = new VertexPositionColor(new Vector3(-1, 2, 0), Color.Gray);

            // ESQUERDA
            verts[18] = new VertexPositionColor(new Vector3(-1, 2, 0), Color.Gray);
            verts[19] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black);
            verts[20] = new VertexPositionColor(new Vector3(-1, -1, -4), Color.Black);

            verts[21] = new VertexPositionColor(new Vector3(-1, -1, -4), Color.Black);
            verts[22] = new VertexPositionColor(new Vector3(-1, 2, -2), Color.Gray);
            verts[23] = new VertexPositionColor(new Vector3(-1, 2, 0), Color.Gray);

            // DIREITA
            verts[24] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);
            verts[25] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black);
            verts[26] = new VertexPositionColor(new Vector3(1, 2, 0), Color.Gray);

            verts[27] = new VertexPositionColor(new Vector3(1, 2, 0), Color.Gray);
            verts[28] = new VertexPositionColor(new Vector3(1, 2, -2), Color.Gray);
            verts[29] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);

            // BAIXO
            verts[30] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black);
            verts[31] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black);
            verts[32] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);

            verts[33] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black);
            verts[34] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);
            verts[35] = new VertexPositionColor(new Vector3(-1, -1, -4), Color.Black);

            // HÉLICE
            /*
            hVerts = new VertexPositionColor[6];
            hVerts[0] = new VertexPositionColor(new Vector3(0, 0.15f, 0), Color.Brown);
            hVerts[1] = new VertexPositionColor(new Vector3(1, 0.15f, 0), Color.Brown);
            hVerts[2] = new VertexPositionColor(new Vector3(1, -0.15f, 0), Color.Brown);
            hVerts[3] = new VertexPositionColor(new Vector3(1, -0.15f, 0), Color.Brown);
            hVerts[4] = new VertexPositionColor(new Vector3(0, -0.15f, 0), Color.Brown);
            hVerts[5] = new VertexPositionColor(new Vector3(0, 0.15f, 0), Color.Brown);
            */
        }

        private void CreateBuffers()
        {
            _vertexBuffer = new VertexBuffer(_graphicsDevice, typeof(VertexPositionColor), verts.Length, BufferUsage.None);
            _vertexBuffer.SetData(verts);

            /*
            _hVertexBuffer = new VertexBuffer(_graphicsDevice, typeof(VertexPositionColor), hVerts.Length, BufferUsage.None);
            _hVertexBuffer.SetData(hVerts);
            */
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < _helixRotations.Count; i++)
            {
                _helixRotations[i] += _helixRotationSpeeds[i] * delta;
                if (_helixRotations[i] > MathHelper.TwoPi) _helixRotations[i] -= MathHelper.TwoPi;
            }
        }

        public void Draw(BasicEffect effect)
        {
            effect.World = _world;
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.SetVertexBuffer(_vertexBuffer);
                _graphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, verts.Length / 3);
            }

            /*
            for (int i = 0; i < _helixWorlds.Count; i++)
            {
                Matrix rotationZ = Matrix.CreateRotationZ(_helixRotations[i]);
                effect.World = rotationZ * _helixWorlds[i] * _world;
                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    _graphicsDevice.SetVertexBuffer(_hVertexBuffer);
                    _graphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, hVerts.Length / 3);
                }
            }
            */
        }
    }
}
