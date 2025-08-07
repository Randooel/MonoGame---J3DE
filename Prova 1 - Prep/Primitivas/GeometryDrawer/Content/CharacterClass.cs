using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDrawer.Content
{
    public class CharacterClass
    {
        Game game;
        Matrix world;
        Vector3 position, rotation, scale;

        CubeDrawer[] cubes;

        public enum State
        {
            Idle,
            Walk,
            Action1,
            Action2
        }

        public State currentState;

        private Vector3 startPos = new Vector3(0f, 0f, 0f);
        private Vector3 endPos = new Vector3(4f, 0f, 0f);
        private bool movingToEnd = true;
        private float speed = 2f;

        public CharacterClass(Game game, Vector3 pos, Vector3 rot, Vector3 scale, State state)
        {
            this.game = game;
            this.position = pos;
            this.rotation = rot;
            this.scale = scale;
            this.currentState = state;

            Initialize();
        }

        public virtual void Initialize()
        {
            cubes = new CubeDrawer[]
            {
            new CubeDrawer(game, new Vector3(1f,1f,1f), Vector3.Zero, new Vector3(1f,2f,1f), Matrix.Identity),
            };
        }

        public virtual void Update(GameTime gameTime)
        {
            // Atualiza movimentação e estado
            switch (currentState)
            {
                case State.Idle:
                    // Não faz nada
                    break;

                case State.Walk:
                    MoveBackAndForth(gameTime);
                    break;

                case State.Action1:
                    // Ação 1
                    break;

                case State.Action2:
                    // Ação 2
                    break;
            }

            // Atualiza matriz world baseado na posição, rotação e escala atuais
            world = Matrix.CreateScale(scale) *
                    Matrix.CreateRotationY(rotation.Y) *
                    Matrix.CreateTranslation(position);

            // Atualiza matriz dos cubos para desenharem corretamente
            foreach (var cube in cubes)
            {
                cube.SetParentMatrix(world);
            }

            // Exemplo de input para mudar estado e escala
            var kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.U))
            {
                currentState = State.Idle;
                scale = new Vector3(3f);
            }
            if (kb.IsKeyDown(Keys.I))
            {
                currentState = State.Walk;
                scale = Vector3.One;
            }
            if (kb.IsKeyDown(Keys.O))
            {
                currentState = State.Action1;
            }
            if (kb.IsKeyDown(Keys.P))
            {
                currentState = State.Action2;
            }
        }

        private void MoveBackAndForth(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector3 target = movingToEnd ? endPos : startPos;
            Vector3 direction = Vector3.Normalize(target - position);
            float distance = Vector3.Distance(position, target);

            if (distance > 0.01f)
                position += direction * speed * dt;
            else
                movingToEnd = !movingToEnd;
        }

        public virtual void Draw(Camera camera)
        {
            foreach (var cube in cubes)
            {
                cube.Draw(camera);
            }
        }
    }
}
