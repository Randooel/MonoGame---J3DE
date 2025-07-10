using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GeometryDrawer.Content
{
    public class CubeDrawer
    {
        private GraphicsDevice _graphicsDevice;
        private VertexBuffer _vertexBuffer;
        private VertexPositionColor[] verts;

        // Matrix mundo para poder aplicar rotações
        public Matrix _world = Matrix.Identity;

        public void SetWorld(Matrix world)
        {
            _world = world;
        }

        public void SetCubeInitialPos(Vector3 position, float rotationYDegrees, float scale)
        {
            Matrix cubeScale = Matrix.CreateScale(scale);
            Matrix cubeRotation = Matrix.CreateRotationY(MathHelper.ToRadians(rotationYDegrees));
            Matrix cubeTranslation = Matrix.CreateTranslation(position);

            Matrix cubeInitialTransform = cubeScale * cubeRotation * cubeTranslation;
            this.SetWorld(cubeInitialTransform);
        }

        public CubeDrawer(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            CreateVertices();
            CreateBuffer();
        }

        private void CreateVertices()
        {
            verts = new VertexPositionColor[36];
            // IPC: Caso algum triângulo esteja com a normal errada pra consertar é muito
            // simples - basta trocar a posição e cor do primeiro e último vértice e...... (<- qtd de pontos pra indicar o tamanho da tela divida)
            // VOILÀ

            //FRENTE DO CUBO

            verts[0] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red); // Define posição e cor do vértice
            verts[1] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Green); // " "
            verts[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Blue); // " "

            // Segundo triângulo para criação de um plano
            verts[3] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Blue);
            verts[4] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Purple);
            verts[5] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);

            // TRASEIRA (Igual a frente do cubo, mas todo Z é -2)
            // Precisa ser desenhado em direção anti-horária para a normal ficar para trás já que é a parte traseira do cubo
            verts[6] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);
            verts[7] = new VertexPositionColor(new Vector3(1, 1, -2), Color.Green);
            verts[8] = new VertexPositionColor(new Vector3(-1, 1, -2), Color.Red);

            verts[9] = new VertexPositionColor(new Vector3(-1, -1, -2), Color.Purple);
            verts[10] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);
            verts[11] = new VertexPositionColor(new Vector3(-1, 1, -2), Color.Red);

            // TOPO (Mesma coisa que a frente só que todo Y é 1 e alguns Zs são -2)
            verts[12] = new VertexPositionColor(new Vector3(1, 1, -2), Color.Blue);
            verts[13] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Green);
            verts[14] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);

            verts[15] = new VertexPositionColor(new Vector3(-1, 1, -2), Color.Purple);
            verts[16] = new VertexPositionColor(new Vector3(1, 1, -2), Color.Blue);
            verts[17] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);

            // ESQUERDA
            verts[18] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);
            verts[19] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Purple);
            verts[20] = new VertexPositionColor(new Vector3(-1, -1, -2), Color.Blue);

            verts[21] = new VertexPositionColor(new Vector3(-1, -1, -2), Color.Blue);
            verts[22] = new VertexPositionColor(new Vector3(-1, 1, -2), Color.Purple);
            verts[23] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Red);

            // DIREITA (mesma coisa que esquerda, mas todo X é +1
            verts[24] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);
            verts[25] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Purple);
            verts[26] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Red);

            verts[27] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Red);
            verts[28] = new VertexPositionColor(new Vector3(1, 1, -2), Color.Purple);
            verts[29] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);

            // BAIXO (mesma coisa que o topo, mas todo Y é -1)
            verts[30] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red);
            verts[31] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Green);
            verts[32] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);

            verts[33] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red);
            verts[34] = new VertexPositionColor(new Vector3(1, -1, -2), Color.Blue);
            verts[35] = new VertexPositionColor(new Vector3(-1, -1, -2), Color.Purple);

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
