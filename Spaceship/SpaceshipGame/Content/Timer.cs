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
    class Timer
    {
        
        #region TIMER STUFF
        public SpriteFont timerFont;
        public double timer = 10;
        #endregion

        public void TimerUpdate(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 0)
            {
                timer --;
            }
            if (timer < 0)
            {
                timer = 0;

                // restart game logic
            }
        }
    }
}
