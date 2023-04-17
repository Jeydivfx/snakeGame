using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_game
{
    public class Engine
    {
        public enum Direction { Left, Right, Up, Down }
        public Keys inputKey;

        private MainForm Form;
        public Game Game; 
        Random Random = new Random();
        Timer Timer = new Timer();

        public Engine()
        {
            Form = new MainForm();
            Form.label1.Text = "Player 1: 0";
            Form.label2.Text = "Player 2: 0";
            Form.label3.Text = "Player 3: 0";
            Game = new Game(Form);
        }


        public void Run()
        {
            Timer.Tick += TimerEventHandler;
            Timer.Interval = 1000 / 10;
            Timer.Start();
            Form.Paint += Draw;
            Form.KeyDown += new KeyEventHandler(Form_KeyDown);
            Application.Run(Form);
        }

        

        private void TimerEventHandler(object sender, EventArgs e)
        {
            Game.Update();
            Form.Refresh();
        }


        private void Draw(object obj, PaintEventArgs args)
        {
            Game.Draw(args);
        }
        private void Form_KeyDown(Object sender, KeyEventArgs e)
        {
            Game.SetInputKey(e.KeyCode);
        }
    }
}
