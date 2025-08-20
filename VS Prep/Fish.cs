using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace VS_Prep
{
    public class Fish
    {
        Game game;
        public Matrix world;
        Vector3 position, rotation, scale;
        float moveSpeed;
        Vector3 currentPos;

        Vector3 targetPos1;

        CubeDrawer[] cubes;

        State currentState;
        public enum State
        {
            Idle,
            Swim,
            LayEggs,
        }

        public Fish(Game game, Vector3 pos, Vector3 rot, Vector3 sca, float speed)
        {
            this.game = game;

            this.rotation = rot;
            this.position = pos;
            this.scale = sca;

            this.moveSpeed = speed;
            this.currentPos = position;

            world = Matrix.Identity;
            world *= Matrix.CreateRotationY(this.rotation.Y);
            world *= Matrix.CreateTranslation(this.position);
            world *= Matrix.CreateScale(this.scale);

            Initialize();
        }

        public void Initialize()
        {
            currentState = State.Idle;

            cubes = new CubeDrawer[]
            {
                new CubeDrawer(game, new Vector3(0f,0f,0f), Vector3.Zero, new Vector3(1f,1f,1f), this.world),
            };

            // CUBE INITIALIZE
            /*
            foreach (var cube in cubes)
            {
                cube.Initialize();
            }
             */
        }


        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case State.Idle:
                    HandleIdle();
                    break;
                case State.Swim:
                    HandleSwim();
                    break;
                case State.LayEggs:
                    HandleLayEggs();
                    break;
            }

            // CUBE UPDATE
            foreach (var cube in cubes)
            {
                cube.Update(gameTime);
            }


            if (currentState == State.Idle)
            {
                targetPos1 = new Vector3(9f, 1f, -5f);

                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                Vector3 direction = Vector3.Normalize(targetPos1 - currentPos);
                float distance = Vector3.Distance(currentPos, targetPos1);

                if (distance > 0.01f)
                {
                    currentPos += direction * moveSpeed * dt;
                    world = Matrix.CreateTranslation(currentPos);
                }
            }

            #region
            // DEBUG
            if (Keyboard.GetState().IsKeyDown(Keys.U))
            {
                currentState = State.Idle;
                Console.WriteLine(currentState);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                currentState = State.Swim;
                Console.WriteLine(currentState);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.O))
            {
                currentState = State.LayEggs;
                Console.WriteLine(currentState);
            }
            #endregion
        }

        public void Draw(Camera camera)
        {

            foreach (var cube in cubes)
            {
                cube.Draw(camera);
            }
        }

        // FUNÇÕES
        public void SetTargetPos(Vector3 targetPos)
        {
            //targetPos1 = targetPos;
        }

        private void HandleIdle()
        {

        }

        private void HandleSwim()
        {

        }

        private void HandleLayEggs()
        {

        }
    }
}
