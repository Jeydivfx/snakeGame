using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace snake_game
{
    public interface ICollidable
    {
        void Draw(Graphics g);
        Rectangle CheckCollision(Rectangle rectangle);
    }
}
