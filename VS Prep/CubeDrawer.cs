using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS_Prep
{
    public class CubeDrawer : Collider
    {
        Game game;
        private VertexPositionColor[] verts;
        private VertexBuffer vBuffer;
        private short[] indices;
        private IndexBuffer iBuffer;
        public Matrix world;
        private Vector3 position, scale, rotation;
        private BasicEffect effect;

        //public Matrix World { get => world; set => world = value; }

        public CubeDrawer(Game game, Vector3 position, Vector3 rotationY, Vector3 scale, Matrix _base)
            : base(game, Vector3.Zero, Vector3.Zero, Color.Green, false)
        {
            this.game = game;

            this.rotation = rotationY;
            this.position = position;
            this.scale = scale;

            this.world = Matrix.Identity;
            this.world *= _base;

            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateRotationY(this.rotation.Y);
            this.world *= Matrix.CreateTranslation(this.position);

            this.effect = new BasicEffect(this.game.GraphicsDevice);

            this.CreateVertex();
            this.CreateVertexBuffer();
            //this.CreateIndex();
            //this.CreateIndexBuffer();

            Vector3 centerLocal = CalculateColliderCenter();
            Vector3 dimensionLocal = CalculateColliderDimension();

            // Apagar isso pro colisor ficar do tamanho exato do cubo
            float extraScale = 1.1f;

            Vector3 colliderCenter = Vector3.Transform(centerLocal, this.world);
            Vector3 colliderDimension = dimensionLocal * scale * 1.01f;

            this.SetPosition(colliderCenter);
            this.dimension = colliderDimension;

            this.UpdateBoundingBox();
            this.lineBox = new LineBox(game, colliderCenter, colliderDimension, Color.Green);
        }

        private void CreateVertex()
        {
            verts = new VertexPositionColor[36];
            // IPC: Caso algum triângulo esteja com a normal errada pra consertar é muito
            // simples - basta trocar a posição e cor do primeiro e último vértice e...... (<- qtd de pontos pra indicar o tamanho da tela dividida)
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

        private void CreateVertexBuffer()
        {
            this.vBuffer = new VertexBuffer(this.game.GraphicsDevice,
                                            typeof(VertexPositionColor),
                                            this.verts.Length,
                                            BufferUsage.None);
            this.vBuffer.SetData<VertexPositionColor>(this.verts);
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                this.SetVisible(true);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                this.SetVisible(false);
            }
        }

        public void Draw(Camera camera)
        {
            this.game.GraphicsDevice.SetVertexBuffer(this.vBuffer);

            this.effect.World = this.world;
            this.effect.View = camera.GetView();
            this.effect.Projection = camera.GetProjection();
            this.effect.VertexColorEnabled = true;

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.game.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    this.verts,
                    0,
                    this.verts.Length / 3);
            }

            this.DrawCollider(camera);
        }

        public void DrawCollider(Camera camera)
        {
            BasicEffect effect = new BasicEffect(this.game.GraphicsDevice)
            {
                View = camera.GetView(),
                Projection = camera.GetProjection(),
                VertexColorEnabled = true
            };

            base.Draw(effect);
        }

        private Vector3 CalculateColliderCenter()
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (var vert in verts)
            {
                min = Vector3.Min(min, vert.Position);
                max = Vector3.Max(max, vert.Position);
            }

            return (min + max) / 2f;
        }

        private Vector3 CalculateColliderDimension()
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            foreach (var vert in verts)
            {
                min = Vector3.Min(min, vert.Position);
                max = Vector3.Max(max, vert.Position);
            }

            return max - min;
        }
    }
}
