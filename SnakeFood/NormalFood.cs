using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_game
{
    public class NormalFood : Food
    {
        public NormalFood(int x, int y, int height, int width) : base(x, y, height, width, 1, 1)
        {

        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Orange), Position.X, Position.Y, Height, Width);
        }
    }
}
