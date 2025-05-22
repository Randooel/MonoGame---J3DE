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
    internal class Controller
    {
        public List<Asteroid> asteroids = new List<Asteroid>();
        public double timer = 2;
        public double maxTime = 2;
        public int nextSpeed = 240;
        //public bool inGame = false;

        public void ConUpdate(GameTime gameTime)
        {
            /*
            if(inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            */
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                asteroids.Add(new Asteroid(nextSpeed));

                timer = maxTime;
                if (maxTime > 0.5)
                {
                    maxTime -= 0.1;
                }

                if (nextSpeed < 720)
                {
                    nextSpeed += 4;
                }
            }
        }
    }
}
