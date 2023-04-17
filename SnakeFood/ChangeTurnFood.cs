using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_game
{
    public class ChangeTurnFood : Food
    {

        public int Duration;

        public ChangeTurnFood(int x, int y, int height, int width) : base(x, y, height, width, 0, 0)
        {
            Duration = 30;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Brown), Position.X, Position.Y, Height, Width);
        }


    }
}