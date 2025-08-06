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
        BladesDrawer blades;

        public Windmill(Game game, Vector3 position)
        {
            this.game = game;
            this.position = position;

            this.world = Matrix.Identity;
            this.world *= Matrix.CreateTranslation(this.position);

            this.wind = new WindmillDrawer(this.game, this.world);
            this.blades = new BladesDrawer(this.game);
        }

        public void Update(GameTime gameTime)
        {
            this.wind.Update(gameTime);
            this.blades.Update(gameTime, this.world);
        }

        public void Draw(Camera camera)
        {
            this.wind.Draw(camera);
            this.blades.Draw(camera);
        }
    }
}
