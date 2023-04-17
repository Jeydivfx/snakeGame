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
    public class Game
    {
        public List<Food> FoodList;
        public List<Player> Players;
        private MainForm Form;
        private Stopwatch Stopwatch;
        private Keys InputKey;
     
        private bool SpelSlut = false;

        public Game(MainForm Form)
        {
            this.Form = Form;
            FoodList = new List<Food>();
            Players = new List<Player>();
            Stopwatch = new Stopwatch();
            Initialize();
        }

        public void SpawnFood(Food foodToSpawn)
        {
            int maxXPos = 50;
            int maxYPos = 40;

            Random random = new Random();

            if (foodToSpawn is NormalFood)
                FoodList.Add(new NormalFood(random.Next(0, maxXPos) * 10, random.Next(0, maxYPos) * 10, 10, 10));
            else if (foodToSpawn is DietFood)
                FoodList.Add(new DietFood(random.Next(0, maxXPos) * 10, random.Next(0, maxYPos) * 10, 10, 10));
            else if (foodToSpawn is SuperFood)
                FoodList.Add(new SuperFood(random.Next(0, maxXPos) * 10, random.Next(0, maxYPos) * 10, 10, 10));
            else
                FoodList.Add(new ChangeTurnFood(random.Next(0, maxXPos) * 10, random.Next(0, maxYPos) * 10, 10, 10));

        }
        public bool GameOver()
        {
            if (Players.All(x => x.IsDead))
                return true;
            else
                return false;
        }
        public void PrintScores()
        {
            Form.label1.Text = "Player 1: " + Players[0].Score.ToString() + (Players[0].IsDead ? " (DEAD)" : "");
            Form.label2.Text = "Player 2: " + Players[1].Score.ToString() + (Players[1].IsDead ? " (DEAD)" : "");
            Form.label3.Text = "Player 3: " + Players[2].Score.ToString() + (Players[2].IsDead ? " (DEAD)" : "");
        }
        public void PrintTime()
        {
            TimeSpan timeSpan = Stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
                timeSpan.Milliseconds / 10);
            Form.label6.Text = "Time: " + elapsedTime;
        }
        public void PrintLabels()
        {
            PrintScores();
            PrintTime();
        }

        public void Restart()
        {
            FoodList.Clear();
            Players.Clear();
            Initialize();
        }

        public void Update()
        {
            PrintLabels();

            if (GameOver())
            {
                if (InputKey == Keys.Enter)
                {
                    Restart();
                    PrintScores();
                    Form.label4.Visible = false;
                    Form.label5.Visible = false;
                }
            }
            foreach (var player in Players)
            {
                if (player.IsDead)
                    continue;

                foreach (var otherPlayer in Players)
                {
                    if (otherPlayer.IsDead)
                        continue;

                    if (otherPlayer == player)
                    {
                        if (player.Snake.CheckCollisionWithSelf())
                        {
                            otherPlayer.Die();
                        }
                    }
                    else
                    {
                        var otherPlayerSnakeHead = new Rectangle();
                        otherPlayerSnakeHead = otherPlayer.Snake.BodyParts[0];

                        var collidedBodyPart = player.Snake.CheckCollision(otherPlayerSnakeHead);
                        if (collidedBodyPart != Rectangle.Empty)
                        {
                            if (collidedBodyPart == player.Snake.BodyParts[0])
                            {
                                otherPlayer.Die();
                                player.Die();
                            }
                            else
                            {
                                otherPlayer.Die();
                                if (otherPlayer != player)
                                    player.Score += 5;
                            }

                        }
                    }
                    if (GameOver())
                    {
                        Form.label4.Visible = true;
                        Form.label5.Visible = true;
                    }
                }
                List<Food> foodToRemove = new List<Food>();
                foreach (var food in FoodList)
                {
                    var foodRectangle = new Rectangle(food.Position.X, food.Position.Y, food.Width, food.Height);
                    if (player.Snake.CheckCollision(foodRectangle) != Rectangle.Empty)
                    {
                        if (food is ChangeTurnFood)
                            player.SetRandomizedTurns(true, GetTimeInSeconds() + ((ChangeTurnFood)food).Duration);
                        else
                        {
                            player.ChangeSizeFood(food.PointValue, food.GrowValue);
                        }

                        foodToRemove.Add(food);
                    }
                }

                foreach (var food in foodToRemove)
                {
                    FoodList.Remove(food);
                    SpawnFood(food);
                }

                if (player.hasRandomizedTurns && GetTimeInSeconds() > player.randomizedTurnsTime)
                    player.SetRandomizedTurns(false, 0);

 
                player.Move(InputKey);
            }
        }
        public int GetTimeInSeconds()
        {
            return (int)(Stopwatch.ElapsedMilliseconds / 1000);
        }
        public void Draw(PaintEventArgs args)
        {
            foreach (var player in Players)
            {
                player.Snake.Draw(args.Graphics);
            }

            foreach (var food in FoodList)
            {
                food.Draw(args.Graphics);
            }

        }
        public void SetInputKey(Keys inputKey)
        {
            InputKey = inputKey;
        }

        public void Initialize()
        {
            Stopwatch.Stop();
            Stopwatch.Start();

            Dictionary<Engine.Direction, Keys> player1Keybinds = new Dictionary<Engine.Direction, Keys>();
            player1Keybinds.Add(Engine.Direction.Left, Keys.Left);
            player1Keybinds.Add(Engine.Direction.Right, Keys.Right);
            player1Keybinds.Add(Engine.Direction.Up, Keys.Up);
            player1Keybinds.Add(Engine.Direction.Down, Keys.Down);
            Players.Add(new Player(600, 50, Color.Red, player1Keybinds));

            Dictionary<Engine.Direction, Keys> player2Keybinds = new Dictionary<Engine.Direction, Keys>();
            player2Keybinds.Add(Engine.Direction.Left, Keys.A);
            player2Keybinds.Add(Engine.Direction.Right, Keys.D);
            player2Keybinds.Add(Engine.Direction.Up, Keys.W);
            player2Keybinds.Add(Engine.Direction.Down, Keys.S);

            Players.Add(new Player(20, 20, Color.Blue, player2Keybinds));

            Dictionary<Engine.Direction, Keys> player3Keybinds = new Dictionary<Engine.Direction, Keys>();
            player3Keybinds.Add(Engine.Direction.Left, Keys.J);
            player3Keybinds.Add(Engine.Direction.Right, Keys.L);
            player3Keybinds.Add(Engine.Direction.Up, Keys.I);
            player3Keybinds.Add(Engine.Direction.Down, Keys.K);

            Players.Add(new Player(200, 200, Color.Green, player3Keybinds));
            int maxXPos = 50;
            int maxYPos = 40;

            Random random = new Random();
            FoodList.Add(new NormalFood(random.Next(0, maxXPos) * 10, random.Next(0, maxYPos) * 10, 10, 10));
            FoodList.Add(new DietFood(random.Next(0, maxXPos) * 10, random.Next(0, maxYPos) * 10, 10, 10));
            FoodList.Add(new SuperFood(random.Next(0, maxXPos) * 10, random.Next(0, maxYPos) * 10, 10, 10));
            FoodList.Add(new ChangeTurnFood(random.Next(0, maxXPos) * 10, random.Next(0, maxYPos) * 10, 10, 10));
        }
    }
}
