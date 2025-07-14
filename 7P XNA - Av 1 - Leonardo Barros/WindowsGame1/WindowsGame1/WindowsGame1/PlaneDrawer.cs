using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsGame1
{
    public class PlaneDrawer
    {
        private GraphicsDevice _graphicsDevice;
        private VertexBuffer _vertexBuffer;
        private VertexPositionColor[] verts;

        // Transforms
        public Matrix _world = Matrix.Identity;

        public void SetWorld(Matrix world)
        {
            _world = world;
        }

        public void SetPlaneInitialPos(Vector3 position, float rotationYDegrees, float scale)
        {
            Matrix planeScale = Matrix.CreateScale(scale);
            Matrix planeRotation = Matrix.CreateRotationY(MathHelper.ToRadians(rotationYDegrees));
            Matrix planeTranslation = Matrix.CreateTranslation(position);

            Matrix planeInitialTransform = planeScale * planeRotation * planeTranslation;
            this.SetWorld(planeInitialTransform);
        }

        public PlaneDrawer(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            CreateVertices();
            CreateBuffer();
        }

        private void CreateVertices()
        {
            verts = new VertexPositionColor[6];
            // IPC: Caso algum triângulo esteja com a normal errada pra consertar é muito
            // simples - basta trocar a posição e cor do primeiro e último vértice e...... (<- qtd de pontos pra indicar o tamanho da tela divida)
            // VOILÀ

            //FRENTE DO CUBO

            verts[0] = new VertexPositionColor(new Vector3(10, -1, -10), Color.Green);
            verts[1] = new VertexPositionColor(new Vector3(10, -1, 10), Color.Green);
            verts[2] = new VertexPositionColor(new Vector3(-10, -1, 10), Color.Green);

            verts[3] = new VertexPositionColor(new Vector3(-10, -1, -10), Color.Green);
            verts[4] = new VertexPositionColor(new Vector3(10, -1, -10), Color.Green);
            verts[5] = new VertexPositionColor(new Vector3(-10, -1, 10), Color.Green);
        }

        private void CreateBuffer()
        {
            _vertexBuffer = new VertexBuffer(
                _graphicsDevice,
                typeof(VertexPositionColor),
                verts.Length,
                BufferUsage.None
            );
            _vertexBuffer.SetData(verts);
        }

        public void Draw(BasicEffect effect)
        {
            // DESENHO DOS VÉRTICES ARMAZENADOS NO BUFFER USANDO EFEITOS
            _graphicsDevice.SetVertexBuffer(_vertexBuffer);

            effect.World = _world;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _graphicsDevice.DrawPrimitives(
                    PrimitiveType.TriangleList, 0, verts.Length / 3
                );
            }
        }
    }
}
