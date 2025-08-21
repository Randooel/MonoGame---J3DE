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

            world = Matrix.CreateScale(this.scale)
                * Matrix.CreateRotationX(this.rotation.X)
                * Matrix.CreateRotationY(this.rotation.Y)
                * Matrix.CreateRotationZ(this.rotation.Z)
                * Matrix.CreateTranslation(this.position);

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
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y += moveSpeed * dt;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y -= moveSpeed * dt;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += moveSpeed * dt;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= moveSpeed * dt;
            }

            world = Matrix.CreateScale(scale)
                    * Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X))
                    * Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y))
                    * Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z))
                    * Matrix.CreateTranslation(position);

            foreach(var cube in cubes)
            {
                cube.UpdateMatrix(this.world);
            }
        }
    }
}
