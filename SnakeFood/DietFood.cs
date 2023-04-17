using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_game
{
    public class DietFood : Food
    {
        public DietFood (int x, int y, int height, int width) : base(x, y, height, width, -1, -1)
        {

        }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Pink), Position.X, Position.Y, Height, Width);
        }
    }
}
