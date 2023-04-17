using snake_game.SnakeFood;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_game
{
    public class Snake : ICollidable
    {
        public Position Position;
        public int Height, Width;
        public Vector Speed { get; set; }
        public Position position { get; set; }
        MainForm Form = new MainForm();
        public List<Rectangle> BodyParts;
        public Color Color;
        public int normalSpeedX = 10, normalSpeedY = 10;

        public Snake(int x, int y, int height, int width, Color color)
        {
            Speed= new Vector(10,0);
            Position = new Position(x, y);
            Height = height;
            Width = width;
            Color = color;
            BodyParts = new List<Rectangle>();
            BodyParts.Add(new Rectangle(x, y, width, height));
        }

        public void Draw(Graphics g)
        {
            if (BodyParts.Count > 0)
                g.FillRectangles(new SolidBrush(Color), BodyParts.ToArray());
        }

        public void Die()
        {
            BodyParts.Clear();
        }

        public void ChangeDirection(Engine.Direction d)
        {
            if (d == Engine.Direction.Left)
                Speed = new Vector(-normalSpeedX, 0);
            else if (d == Engine.Direction.Right)
                Speed = new Vector(normalSpeedX, 0);
            else if (d == Engine.Direction.Up)
                Speed = new Vector(0, -normalSpeedY);
            else if (d == Engine.Direction.Down)
                Speed = new Vector(0, normalSpeedY);


        }

        public void RandomDirection()
        {
            Random random = new Random();
            var chosenNumber = random.Next(0, 4);

            if (chosenNumber == 0)
                Speed = new Vector(-normalSpeedX, 0);
            else if (chosenNumber == 2)
                Speed = new Vector(normalSpeedX, 0);
            else if (chosenNumber == 3)
                Speed = new Vector(0, -normalSpeedY);
            else if (chosenNumber == 1)
                Speed = new Vector(0, normalSpeedY);
        }


        public void MultiplySpeed(double factor)
        {
            if (Speed.X != 0)
            {
                normalSpeedX = (int)(Speed.X * factor);
                normalSpeedY = (int)(Speed.X * factor);
            }
                
            else if (Speed.Y != 0)
            {
                normalSpeedY = (int)(Speed.Y * factor);
                normalSpeedX = (int)(Speed.Y * factor);
            }
                
        }




        internal void Move()
        {
            for (int i = BodyParts.Count - 1; i > 0; i--)
            {
                BodyParts[i] = BodyParts[i - 1];
            }

            var newPosition = new Position(BodyParts[0].X + Speed.X, BodyParts[0].Y + Speed.Y);
            Position = newPosition;
            BodyParts[0] = new Rectangle(newPosition.X, newPosition.Y, BodyParts[0].Width, BodyParts[0].Height);


            if (BodyParts[0].X > Form.Width)
            {
                BodyParts[0] = new Rectangle(-10, BodyParts[0].Y, BodyParts[0].Width, BodyParts[0].Height);
            }

            else if (BodyParts[0].X < 0)
            {
                BodyParts[0] = new Rectangle(Form.Width, BodyParts[0].Y, BodyParts[0].Width, BodyParts[0].Height);
            }

            if (BodyParts[0].Y > Form.Height)
            {
                BodyParts[0] = new Rectangle(BodyParts[0].X, 0, BodyParts[0].Width, BodyParts[0].Height);
            }

            else if (BodyParts[0].Y < 0)
            {
                BodyParts[0] = new Rectangle(BodyParts[0].X, Form.Height, BodyParts[0].Width, BodyParts[0].Height);
            }
        }

        public void ChangeSize(int value)
        {
            if (value < 0)
            {
                value = Math.Abs(value);
                for (int i = 0; i < value; i++)
                {
                    if (BodyParts.Count > 1)
                    {
                        BodyParts.RemoveAt(BodyParts.Count - 1);
                    }
                }
            }

            else
            {
                for (int i = 0; i < value; i++)
                {
                    BodyParts.Add(new Rectangle(BodyParts[BodyParts.Count - 1].X, BodyParts[BodyParts.Count - 1].Y, Width, Height));
                }
            }

        }

        public Rectangle CheckCollision(Rectangle rectangle)
        {
            foreach (var BodyPart in BodyParts)
            {
                if (BodyPart.IntersectsWith(rectangle))
                    return BodyPart;
               
                    
            }
            return Rectangle.Empty;
        }

        public bool CheckCollisionWithSelf()
        {
            var snakeHead = BodyParts[0];
            var bodyPartsWithoutHead = new List<Rectangle>(BodyParts);
            bodyPartsWithoutHead.RemoveAt(0); 
            foreach (var BodyPart in bodyPartsWithoutHead)
            {
                if (snakeHead.IntersectsWith(BodyPart))
                    return true;
            }
            return false;
        }
    }
}

