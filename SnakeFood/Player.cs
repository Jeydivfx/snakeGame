using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_game.SnakeFood
{

    public class Player
    {
        public Snake Snake;
        public int Score;
        public Color Color;
        public bool IsDead;
        public Dictionary<Engine.Direction, Keys> Keybinds;
        private Color red;
        public Dictionary<Engine.Direction, Keys> RandomizedKeybinds;
        public int randomizedTurnsTime;
        public bool hasRandomizedTurns;
        


        public Player(int x, int y, Color color, Dictionary<Engine.Direction, Keys> keybinds)
        {
            Color = color;
            Snake = new Snake(x, y, 10, 10, color);
            Score = 0;
            IsDead = false;
            Keybinds = keybinds;
        }

        public void Die()
        {
            IsDead = true;
            Snake.Die();
        }

        internal void Move(Keys inputKey)
        {
            if (IsDead)
                return;

            if (Keybinds.Any(x => x.Value == inputKey))
            {
                var selectedDirection = Keybinds.FirstOrDefault(x => x.Value == inputKey).Key;
                Snake.ChangeDirection(selectedDirection);
            }
            

            if (hasRandomizedTurns)
            {   
                Snake.RandomDirection();
            }

            Snake.Move();

        }

        public void SetRandomizedTurns(bool activateRandomizedTurns, int timeStampSeconds)
        {
            hasRandomizedTurns = activateRandomizedTurns;
            randomizedTurnsTime = timeStampSeconds;
            
        }

        public void ChangeSizeFood(int pointValue, int growValue)
        {
            Score += pointValue;
            Snake.ChangeSize(growValue);
        }
    }
}
