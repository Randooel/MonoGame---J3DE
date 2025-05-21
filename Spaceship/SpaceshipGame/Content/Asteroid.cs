using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceshipGame.Content
{
    class Asteroid
    {
        #region ASTEROID STUFF
        public Texture2D sprite;
        public Vector2 position = new Vector2(1000, 100);
        public float speed;
        public const int radius = 59;
        #endregion;

        public Asteroid(int newSpeed)
        {
            speed = newSpeed;
        }

        public void AsteroidUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}
