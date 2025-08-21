using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace Gusty_Golbat.Content
{
    public class CubeDrawer : Collider
    {
        Game game;
        private VertexPositionTexture[] verts;

        private VertexBuffer vBuffer;
        private short[] indices;
        public Matrix world;
        private Vector3 position, scale, rotation;
        private BasicEffect effect;
        private Texture2D texture;

        public CubeDrawer(Game game, Vector3 position, Vector3 rotation, Vector3 scale, Matrix _base, Texture2D texture)
            : base(game, Vector3.Zero, Vector3.Zero, Color.Green, false)
        {
            this.game = game;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.texture = texture;

            this.world = Matrix.Identity;
            this.world *= _base;
            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateRotationY(this.rotation.Y);
            this.world *= Matrix.CreateTranslation(this.position);

            this.effect = new BasicEffect(this.game.GraphicsDevice)
            {
                TextureEnabled = true,
                Texture = texture
            };

            CreateVertex();
            CreateVertexBuffer();

            Vector3 colliderCenter = Vector3.Transform(CalculateColliderCenter(), this.world);
            Vector3 colliderDimension = CalculateColliderDimension() * scale;

            this.SetPosition(colliderCenter);
            this.dimension = colliderDimension;
            this.UpdateBoundingBox();
            this.lineBox = new LineBox(game, colliderCenter, colliderDimension, Color.Green);
        }

        private void CreateVertex()
        {
            verts = new VertexPositionTexture[36];

            Vector2 uv00 = new Vector2(0, 0);
            Vector2 uv10 = new Vector2(1, 0);
            Vector2 uv01 = new Vector2(0, 1);
            Vector2 uv11 = new Vector2(1, 1);

            // FRENTE (Z = 0)
            verts[0] = new VertexPositionTexture(new Vector3(-1, 1, 0), uv00);
            verts[1] = new VertexPositionTexture(new Vector3(1, 1, 0), uv10);
            verts[2] = new VertexPositionTexture(new Vector3(1, -1, 0), uv11);
            verts[3] = new VertexPositionTexture(new Vector3(1, -1, 0), uv11);
            verts[4] = new VertexPositionTexture(new Vector3(-1, -1, 0), uv01);
            verts[5] = new VertexPositionTexture(new Vector3(-1, 1, 0), uv00);

            // TRASEIRA (Z = -2)
            verts[6] = new VertexPositionTexture(new Vector3(1, -1, -2), uv11);
            verts[7] = new VertexPositionTexture(new Vector3(1, 1, -2), uv10);
            verts[8] = new VertexPositionTexture(new Vector3(-1, 1, -2), uv00);
            verts[9] = new VertexPositionTexture(new Vector3(-1, -1, -2), uv01);
            verts[10] = new VertexPositionTexture(new Vector3(1, -1, -2), uv11);
            verts[11] = new VertexPositionTexture(new Vector3(-1, 1, -2), uv00);

            // TOPO (Y = 1)
            verts[12] = new VertexPositionTexture(new Vector3(-1, 1, 0), uv01);
            verts[13] = new VertexPositionTexture(new Vector3(1, 1, 0), uv11);
            verts[14] = new VertexPositionTexture(new Vector3(1, 1, -2), uv10);
            verts[15] = new VertexPositionTexture(new Vector3(1, 1, -2), uv10);
            verts[16] = new VertexPositionTexture(new Vector3(-1, 1, -2), uv00);
            verts[17] = new VertexPositionTexture(new Vector3(-1, 1, 0), uv01);

            // BAIXO (Y = -1)
            verts[18] = new VertexPositionTexture(new Vector3(-1, -1, 0), uv01);
            verts[19] = new VertexPositionTexture(new Vector3(1, -1, 0), uv11);
            verts[20] = new VertexPositionTexture(new Vector3(1, -1, -2), uv10);
            verts[21] = new VertexPositionTexture(new Vector3(1, -1, -2), uv10);
            verts[22] = new VertexPositionTexture(new Vector3(-1, -1, -2), uv00);
            verts[23] = new VertexPositionTexture(new Vector3(-1, -1, 0), uv01);

            // ESQUERDA (X = -1)
            verts[24] = new VertexPositionTexture(new Vector3(-1, 1, 0), uv00);
            verts[25] = new VertexPositionTexture(new Vector3(-1, -1, 0), uv01);
            verts[26] = new VertexPositionTexture(new Vector3(-1, -1, -2), uv11);
            verts[27] = new VertexPositionTexture(new Vector3(-1, -1, -2), uv11);
            verts[28] = new VertexPositionTexture(new Vector3(-1, 1, -2), uv10);
            verts[29] = new VertexPositionTexture(new Vector3(-1, 1, 0), uv00);

            // DIREITA (X = 1)
            verts[30] = new VertexPositionTexture(new Vector3(1, 1, 0), uv00);
            verts[31] = new VertexPositionTexture(new Vector3(1, -1, 0), uv01);
            verts[32] = new VertexPositionTexture(new Vector3(1, -1, -2), uv11);
            verts[33] = new VertexPositionTexture(new Vector3(1, -1, -2), uv11);
            verts[34] = new VertexPositionTexture(new Vector3(1, 1, -2), uv10);
            verts[35] = new VertexPositionTexture(new Vector3(1, 1, 0), uv00);
        }

        private void CreateVertexBuffer()
        {
            this.vBuffer = new VertexBuffer(game.GraphicsDevice,
                                            typeof(VertexPositionTexture),
                                            verts.Length,
                                            BufferUsage.None);
            this.vBuffer.SetData<VertexPositionTexture>(verts);
        }

        public void UpdateMatrix(Matrix newWorld)
        {
            this.world = newWorld;
        }

        public void Draw(Camera camera, Texture2D texture)
        {
            game.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            game.GraphicsDevice.SetVertexBuffer(this.vBuffer);

            effect.World = this.world;
            effect.View = camera.GetView();
            effect.Projection = camera.GetProjection();

            effect.TextureEnabled = true;
            effect.Texture = texture;
            effect.VertexColorEnabled = false;
            effect.LightingEnabled = false;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                game.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleList,
                    verts,
                    0,
                    verts.Length / 3
                );
            }

            DrawCollider(camera);  
        }

        public void DrawCollider(Camera camera)
        {
            BasicEffect effect = new BasicEffect(game.GraphicsDevice)
            {
                View = camera.GetView(),
                Projection = camera.GetProjection(),
                VertexColorEnabled = true
            };

            base.Draw(effect);
        }

        private Vector3 CalculateColliderCenter()
        {
            Vector3 min = new Vector3(float.MaxValue);
            Vector3 max = new Vector3(float.MinValue);

            foreach (var vert in verts)
            {
                min = Vector3.Min(min, vert.Position);
                max = Vector3.Max(max, vert.Position);
            }

            return (min + max) / 2f;
        }

        private Vector3 CalculateColliderDimension()
        {
            Vector3 min = new Vector3(float.MaxValue);
            Vector3 max = new Vector3(float.MinValue);

            foreach (var vert in verts)
            {
                min = Vector3.Min(min, vert.Position);
                max = Vector3.Max(max, vert.Position);
            }

            return max - min;
        }
    }
}
