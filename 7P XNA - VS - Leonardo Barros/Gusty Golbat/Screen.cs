using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gusty_Golbat
{
    public class Screen
    {
        private int WIDTH;
        private int HEIGHT;
        private static Screen instance;

        public static Screen GetInstance()
        {
            if (instance == null)
            {
                instance = new Screen();
            }

            return instance;
        }

        public void SetWidth(int width)
        {
            WIDTH = width;
        }

        public int GetWidth()
        {
            return WIDTH;
        }

        public void SetHeight(int height)
        {
            HEIGHT = height;
        }

        public int GetHeight()
        {
            return HEIGHT;
        }
    }
}
