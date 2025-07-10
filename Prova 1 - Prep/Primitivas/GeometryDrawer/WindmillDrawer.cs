using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDrawer
{
    public class WindmillDrawer
    {
        private GraphicsDevice _graphicsDevice;
        private VertexBuffer _vertexBuffer;
        private VertexPositionColor[] verts;

        // Matriz
        public Matrix _world = Matrix.Identity;

        public void SetWorld(Matrix world)
        {
            _world = world;
        }

        public void SetWindmillInitialPos(Vector3 position, float rotationYDegrees, float scale)
        {
            Matrix windmillScale = Matrix.CreateScale(scale);
            Matrix windmillRotation = Matrix.CreateRotationY(MathHelper.ToRadians(rotationYDegrees));
            Matrix windmillTranslation = Matrix.CreateTranslation(position);

            Matrix windmillInitialTransform = windmillScale * windmillRotation * windmillTranslation;
            this.SetWorld(windmillInitialTransform);
        }

        public WindmillDrawer(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            CreateVertices();
            CreateBuffer();
        }

        // No geral, a mesma coisa com o cubo, só que com uma base traseira mais afastada
        // e topo mais alto
        // Em suma: SUBSTITUIR TODOS OS Y +1 por +4 e o Z -2 da traseira e lados por -4
        private void CreateVertices()
        {
            verts = new VertexPositionColor[36];
            // IPC: Caso algum triângulo esteja com a normal errada pra consertar é muito
            // simples - basta trocar a posição e cor do primeiro e último vértice e...... (<- qtd de pontos pra indicar o tamanho da tela divida)
            // VOILÀ

            //FRENTE DO CUBO
            verts[0] = new VertexPositionColor(new Vector3(-1, 2, 0), Color.Gray); // Define posição e cor do vértice
            verts[1] = new VertexPositionColor(new Vector3(1, 2, 0), Color.Gray); // " "
            verts[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black); // " "

            // Segundo triângulo para criação de um plano
            verts[3] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black);
            verts[4] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black);
            verts[5] = new VertexPositionColor(new Vector3(-1, 2, 0), Color.Gray);

            // TRASEIRA (Igual a frente do cubo, mas todo Z é -2)
            // Precisa ser desenhado em direção anti-horária para a normal ficar para trás já que é a parte traseira do cubo
            verts[6] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);
            verts[7] = new VertexPositionColor(new Vector3(1, 2, -2), Color.Gray);
            verts[8] = new VertexPositionColor(new Vector3(-1, 2, -2), Color.Gray);

            verts[9] = new VertexPositionColor(new Vector3(-1, -1, -4), Color.Black);
            verts[10] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);
            verts[11] = new VertexPositionColor(new Vector3(-1, 2, -2), Color.Gray);

            // TOPO (Mesma coisa que a frente só que todo Y é 1 e alguns Zs são -2)
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

            // DIREITA (mesma coisa que esquerda, mas todo X é +1
            verts[24] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);
            verts[25] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black);
            verts[26] = new VertexPositionColor(new Vector3(1, 2, 0), Color.Gray);

            verts[27] = new VertexPositionColor(new Vector3(1, 2, 0), Color.Gray);
            verts[28] = new VertexPositionColor(new Vector3(1, 2, -2), Color.Gray);
            verts[29] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);

            // BAIXO (mesma coisa que o topo, mas todo Y é -1)
            verts[30] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black);
            verts[31] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Black);
            verts[32] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);

            verts[33] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Black);
            verts[34] = new VertexPositionColor(new Vector3(1, -1, -4), Color.Black);
            verts[35] = new VertexPositionColor(new Vector3(-1, -1, -4), Color.Black);

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
