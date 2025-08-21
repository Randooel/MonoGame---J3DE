using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Gusty_Golbat.Content
{
    public class Golbat
    {
        Game game;
        Matrix world;
        Vector3 position, rotation, scale;
        float moveSpeed;

        // Substituir para uma geometria do Golbat, se der tempo
        CubeDrawer[] cubes;

        State currentState;
        public enum State
        {
            Idle,
            Flying,
            Damaged
        }

        public Golbat(Game game, Vector3 pos, Vector3 rot, Vector3 sca, float speed)
        {
            this.game = game;

            this.scale = sca;
            this.rotation = rot;
            this.position = pos;

            this.moveSpeed = speed;

            world = Matrix.Identity;

            world = Matrix.CreateScale(this.scale);

            world = Matrix.CreateRotationX(this.rotation.X);
            world = Matrix.CreateRotationY(this.rotation.Y);
            world = Matrix.CreateRotationZ(this.rotation.Z);

            world = Matrix.CreateTranslation(this.position);

            Initialize();
        }

        public void Initialize()
        {
            currentState = State.Idle;

            cubes = new CubeDrawer[]
            {
                new CubeDrawer(game, new Vector3(0f, 0f, 0f), Vector3.Zero, new Vector3(1f,1f,1f), this.world)
            };
        }

        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case State.Idle:
                    HandleIdle();
                    break;
                case State.Flying:
                    HandleFly();
                    break;
                case State.Damaged:
                    HandleDamage();
                    break;
            }

            Translation(gameTime);

        }

        public void Draw(Camera camera)
        {
            foreach(var cube in cubes)
            {
                cube.Draw(camera);
            }
        }

        // FUNÇÕES DE ESTADOS
        private void HandleIdle()
        {

        }

        private void HandleFly()
        {

        }

        private void HandleDamage()
        {

        }

        // FUNÇÕES DE AÇÃO
        private void Translation(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.position.X -= (float)Math.Sin(MathHelper.ToRadians(0)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
                this.position.Z -= (float)Math.Cos(MathHelper.ToRadians(0)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(0)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(0)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(0 + 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(0 + 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(0 - 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(0 - 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                this.position.Y -= (float)Math.Sin(MathHelper.ToRadians(0 - 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                this.position.Y += (float)Math.Sin(MathHelper.ToRadians(0 - 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.moveSpeed;
            }
        }
    }
}
