using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryDrawer.Content
{
    public class CharacterInstance //: CharacterClass
    {
        /*
        private Vector3 startPos, endPos, currentPos;
        private float speed = 2f;
        private bool movingToEnd = true;

        public CharacterInstance(Game game, Vector3 pos, Vector3 rot, Vector3 sca, State initialState)
           : base(game, pos, rot, sca, initialState)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(currentState == State.Walk)
            {
                startPos = new Vector3(0f, 0f, 0f);
                endPos = new Vector3(4f, 0f, 0f);

                currentPos = startPos;

                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                Vector3 target = movingToEnd ? endPos : startPos;

                Vector3 direction = Vector3.Normalize(target - currentPos);
                float distance = Vector3.Distance(currentPos, target);

                if (distance > 0.01f)
                {
                    currentPos += direction * speed * dt;
                }
                else
                {
                    // Chegou no alvo, inverte a direção
                    movingToEnd = !movingToEnd;
                }
            }
            base.Update(gameTime);
        }

        protected override void HandleIdle()
        {
            
        }

        protected override void HandleWalk()
        {
            
        }

        protected override void HandleAction1()
        {
            
        }

        protected override void HandleAction2()
        {

        }

        public override void Draw(Camera camera)
        {
            
            base.Draw(camera);
        }
        */
    }
}
