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
    class Ship
    {
        #region SHIP STUFF
        public Texture2D sprite;
        public Vector2 position;
        public float speed = 5 * 60;
        public SpriteFont spaceFont;
        #endregion

        public void ShipUdpate(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(kState.IsKeyDown(Keys.D))
            {
                position.X += speed * dt;
            }
            if(kState.IsKeyDown(Keys.A))
            {
                position.X -= speed * dt;
            }
            if(kState.IsKeyDown(Keys.W))
            {
                position.Y -= speed * dt;
            }
            if (kState.IsKeyDown(Keys.S))
            {
                position.Y += speed * dt;
            }          
        }
    }
}
