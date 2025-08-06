using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GeometryDrawer
{
    public class Windmill
    {
        Game game;
        Matrix world;
        Vector3 position, scale, rotation;
        WindmillDrawer wind;
        List<BladesDrawer> blades = new List<BladesDrawer>();

        public Windmill(Game game, Vector3 position, int bladesNumber)
        {
            this.game = game;
            this.position = position;

            this.world = Matrix.Identity;
            this.world *= Matrix.CreateTranslation(this.position);

            this.wind = new WindmillDrawer(this.game, this.world);

            for(int i = 0; i < bladesNumber; i++)
            {
                Vector3 initialPos = new Vector3(0f, 1.5f, 0f);
                Vector3 initialRot = new Vector3(0f, 0f, MathHelper.ToRadians(90f * i));
                BladesDrawer blade = new BladesDrawer(this.game, initialPos, initialRot);

                this.blades.Add(blade);
            }
        }

        public void Update(GameTime gameTime)
        {
            this.wind.Update(gameTime);
            foreach(var blade in blades)
            {
                blade.Update(gameTime, this.world);
            }
        }

        public void Draw(Camera camera)
        {
            this.wind.Draw(camera);
            
            foreach(var blades in this.blades)
            {
                blades.Draw(camera);
            }
        }
    }
}
