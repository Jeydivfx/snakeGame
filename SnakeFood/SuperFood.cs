using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace snake_game
{
    public class SuperFood : Food
    {
        public SuperFood(int x, int y, int height, int width) : base(x, y, height, width, 5, 2)
        {

        }
        public override void Draw(System.Drawing.Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Purple), Position.X, Position.Y, Height, Width);
        }

        
    }
}
