using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_game
{
    public abstract class Food : ICollidable
    {
        public Position Position;
        public int Height, Width;
        public int PointValue;
        public int GrowValue;

        public Food(int x, int y, int height, int width, int pointValue, int growValue)
        {
            Position = new Position(x, y);
            Height = height;
            Width = width;
            PointValue = pointValue;
            GrowValue = growValue;
        }

        public abstract void Draw(Graphics g);

        public Rectangle CheckCollision(Rectangle rectangle)
        {
            Rectangle FoodCollisionBox = new Rectangle(Position.X, Position.Y, Width, Height);

            if (Rectangle.Intersect(FoodCollisionBox, rectangle).IsEmpty)  
                return Rectangle.Empty;
            else
                return FoodCollisionBox;
        }
    }
}
