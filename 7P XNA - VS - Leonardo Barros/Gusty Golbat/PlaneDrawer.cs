using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gusty_Golbat
{
    public class PlaneDrawer
    {
        private GraphicsDevice _graphicsDevice;
        private VertexBuffer _vertexBuffer;
        private VertexPositionTexture[] verts;

        // Transforms
        public Matrix _world = Matrix.Identity;

        public void SetWorld(Matrix world)
        {
            _world = world;
        }

        public void SetPlaneInitialTransform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Matrix planeScale = Matrix.CreateScale(scale);

            Matrix planeRotationX = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            Matrix planeRotationY = Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            Matrix planeRotationZ = Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
            Matrix planeFinalRotation = planeRotationX * planeRotationY * planeRotationZ;

            Matrix planeTranslation = Matrix.CreateTranslation(position);


            Matrix planeInitialTransform = planeScale * planeFinalRotation * planeTranslation;
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
            verts = new VertexPositionTexture[6];
            // IPC: Caso algum triângulo esteja com a normal errada pra consertar é muito
            // simples - basta trocar a posição e cor do primeiro e último vértice e...... (<- qtd de pontos pra indicar o tamanho da tela divida)
            // VOILÀ

            //FRENTE DO CUBO

            verts[0] = new VertexPositionTexture(new Vector3(10, -1, -10), new Vector2(1, 1));
            verts[1] = new VertexPositionTexture(new Vector3(10, -1, 10), new Vector2(1, 0));
            verts[2] = new VertexPositionTexture(new Vector3(-10, -1, 10), new Vector2(0, 0));

            verts[3] = new VertexPositionTexture(new Vector3(-10, -1, -10), new Vector2(0, 1));
            verts[4] = new VertexPositionTexture(new Vector3(10, -1, -10), new Vector2(1, 1));
            verts[5] = new VertexPositionTexture(new Vector3(-10, -1, 10), new Vector2(0, 0));
        }

        private void CreateBuffer()
        {
            _vertexBuffer = new VertexBuffer(
                _graphicsDevice,
                typeof(VertexPositionTexture),
                verts.Length,
                BufferUsage.None
            );
            _vertexBuffer.SetData(verts);
        }

        public void Draw(BasicEffect effect, Texture2D texture)
        {
            // DESENHO DOS VÉRTICES ARMAZENADOS NO BUFFER USANDO EFEITOS
            _graphicsDevice.SetVertexBuffer(_vertexBuffer);

            effect.TextureEnabled = true;
            effect.Texture = texture;
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