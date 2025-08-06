using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GeometryDrawer
{
    public class BladesDrawer
    {
        Game game;
        VertexPositionColor[] verts;
        VertexBuffer vBuffer;
        short[] indices;
        IndexBuffer iBuffer;
        Matrix world;
        Vector3 position, scale, rotation;
        BasicEffect effect;

        public BladesDrawer(Game game)
        {
            this.game = game;

            // posição inicial
            this.position = new Vector3(0, 1.5f, 0);

            // atribuição de matriz que representa as transformações desse objeto (zerada)
            this.world = Matrix.Identity;
            // aplica a transformação na posição da matriz
            this.world *= Matrix.CreateTranslation(this.position);

            this.effect = new BasicEffect(this.game.GraphicsDevice);

            this.CreateVertex();
            this.CreateVertexBuffer();
            this.CreateIndex();
            this.CreateIndexBuffer();
        }

        private void CreateVertex()
        {
            verts = new VertexPositionColor[6];
            verts[0] = new VertexPositionColor(new Vector3(0, 0.15f, 0), Color.Brown);
            verts[1] = new VertexPositionColor(new Vector3(1, 0.15f, 0), Color.Brown);
            verts[2] = new VertexPositionColor(new Vector3(1, -0.15f, 0), Color.Brown);
            verts[3] = new VertexPositionColor(new Vector3(1, -0.15f, 0), Color.Brown);
            verts[4] = new VertexPositionColor(new Vector3(0, -0.15f, 0), Color.Brown);
            verts[5] = new VertexPositionColor(new Vector3(0, 0.15f, 0), Color.Brown);
        }

        private void CreateVertexBuffer()
        {
            this.vBuffer = new VertexBuffer(this.game.GraphicsDevice,
                                            typeof(VertexPositionColor),
                                            this.verts.Length,
                                            BufferUsage.None);
            this.vBuffer.SetData<VertexPositionColor>(this.verts);
        }

        private void CreateIndex()
        {
            this.indices = new short[]
            {
                //frente
                6,5,0,

                //direita
                4,3,0,

                //tras
                2,1,0,

                //esquerda
                8,7,0,

            };
        }
        private void CreateIndexBuffer()
        {
            this.iBuffer = new IndexBuffer(this.game.GraphicsDevice,
                                           IndexElementSize.SixteenBits,
                                           this.indices.Length,
                                           BufferUsage.None);
            this.iBuffer.SetData<short>(this.indices);
        }

        // Matrix _base faz se posicionar em relação à essa matriz
        public void Update(GameTime gameTime, Matrix _base)
        {
            this.rotation.Y += gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            this.world = Matrix.Identity;
            this.world *= Matrix.CreateRotationY(this.rotation.Y);
            this.world *= Matrix.CreateTranslation(this.position);
            this.world *= _base;
        }

        public void Draw(Camera camera)
        {
            this.game.GraphicsDevice.SetVertexBuffer(this.vBuffer);
            this.game.GraphicsDevice.Indices = this.iBuffer;

            this.effect.World = this.world;
            this.effect.View = camera.GetView();
            this.effect.Projection = camera.GetProjection();
            this.effect.VertexColorEnabled = true;

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                                                    PrimitiveType.TriangleList,
                                                    this.verts,
                                                    0,
                                                    this.verts.Length,
                                                    this.indices,
                                                    0,
                                                    this.indices.Length / 3);
            }

        }
    }
}
