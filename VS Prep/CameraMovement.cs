using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VS_Prep
{
    public class CameraMovement
    {
        protected Matrix world;

        protected Vector3 position, scale, rotationY;

        protected float speed = 10;
        protected float speedY = 100;

        protected float angleY = 0;
        protected float angleX = 0;

        public CameraMovement()
        {
            // Posição inicial
            this.position = new Vector3(3, 2, 5);

            world = Matrix.Identity;
        }

        public void Update(GameTime gameTime)
        {
            this.Translation(gameTime);
        }

        private void Translation(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.position.X -= (float)Math.Sin(MathHelper.ToRadians(this.angleY)) * speed * dt;
                this.position.Z -= (float)Math.Cos(MathHelper.ToRadians(this.angleY)) * speed * dt;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(this.angleY)) * speed * dt;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(this.angleY)) * speed * dt;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.position.X -= (float)Math.Sin(MathHelper.ToRadians(this.angleY + 90)) * speed * dt;
                this.position.Z -= (float)Math.Cos(MathHelper.ToRadians(this.angleY + 90)) * speed * dt;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(this.angleY + 90)) * speed * dt;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(this.angleY + 90)) * speed * dt;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                this.position.Y += speed * dt;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                this.position.Y -= speed * dt;
            }
        }
    }
}
