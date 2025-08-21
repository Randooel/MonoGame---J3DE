using Gusty_Golbat.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Gusty_Golbat
{
    public class Coracao : Collider
    {
        Game game;
        Vector3 position, scale;
        float moveSpeed;
        CubeDrawer[] cubes;
        Texture2D texture;
        Matrix world;

        public Coracao(Game game, Vector3 position, Vector3 dimension, Color color, bool visible = true)
            : base(game, position, dimension, color, visible)
        {
            this.game = game;
            this.position = position;
            this.scale = new Vector3(0.5f, 0.5f, 0.3f);
            this.moveSpeed = new Random().Next(1, 15);

            this.world = Matrix.Identity;
            this.world *= Matrix.CreateScale(this.scale);
            this.world *= Matrix.CreateTranslation(this.position);

            Initialize();
        }

        public void Initialize()
        {
            cubes = new CubeDrawer[]
            {
                new CubeDrawer(game, Vector3.Zero, Vector3.Zero, Vector3.One, this.world, texture)
            };

            this.SetPosition(this.position);
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.position.X -= moveSpeed * delta * 0.5f;

            this.SetPosition(this.position);

            this.world = Matrix.CreateScale(scale) * Matrix.CreateTranslation(position);
            foreach (var cube in cubes)
            {
                cube.UpdateMatrix(this.world);
            }
        }

        public void Draw(Camera camera)
        {
            foreach (var cube in cubes)
            {
                cube.Draw(camera, texture);
            }
        }
    }
}
