using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TruePassarosPasseando.Content
{
    internal class Ducks
    {
        #region VARIABLES
        Texture2D sprite;
        Vector2 position, speed;
        Point size;
        Color color;
        #endregion

        public Ducks(Texture2D sprite, Vector2 position, Point size, Color color)
        {
            this.sprite = sprite;
            this.position = position;
            this.size = size;
            this.color = color;
        }
    }
}
