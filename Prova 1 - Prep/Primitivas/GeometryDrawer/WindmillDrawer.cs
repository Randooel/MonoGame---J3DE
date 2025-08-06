using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GeometryDrawer
{
    public class WindmillDrawer
    {
        Game game;
        private VertexPositionColor[] verts;
        private VertexBuffer vBuffer;
        private short[] indices;
        private IndexBuffer iBuffer;
        private Matrix world;
        private Vector3 position, scale, rotation;
        private BasicEffect effect;

        public WindmillDrawer(Game game, Matrix _base)
        {
            this.game = game;

            this.world = Matrix.Identity;
            // Permite que posicione esse objeto em relação a outro
            this.world *= _base;

            this.effect = new BasicEffect(this.game.GraphicsDevice);

            this.CreateVertex();
            this.CreateVertexBuffer();
            this.CreateIndex();
            this.CreateIndexBuffer();
        }

        // No geral, a mesma coisa com o cubo, só que com uma base traseira mais afastada
        // e topo mais alto
        // Em suma: SUBSTITUIR TODOS OS Y +1 por +4 e o Z -2 da traseira e lados por -4
        private void CreateVertex()
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
                3,2,7,
                2,6,7,
                //direita
                2,1,6,
                1,5,6,
                //tras
                1,0,5,
                0,4,5,
                //esquerda
                0,3,4,
                3,7,4,
                //cima
                0,1,3,
                1,2,3,
                //baixo
                7,6,4,
                6,5,4
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

        public void Update(GameTime gameTime)
        {
            
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
