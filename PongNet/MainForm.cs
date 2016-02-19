using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongNet
{
    public partial class MainForm : Form
    {

        private Timer timer = new Timer();
        private Graphics graphics;
        private Rectangle platform;
        private Rectangle ball;
        private int axisXSpeed;
        private int axisYSpeed;
        private int axisInvert;
        private int points;

        public MainForm()
        {
            InitializeComponent();
            platform = new Rectangle(182, 400, 120, 40);
            ball = new Rectangle(new Random().Next(50, 450), 20, 25, 25);
            SetSpeed(5);
            points = 0;
        }

        private void SetSpeed(int speed)
        {
            axisXSpeed = speed;
            axisYSpeed = speed;
            axisInvert = speed * 2;
        }

        public void ResetGame()
        {
            ball.Location = new Point(new Random().Next(50, 450), 20);
            points = 0;
            CountLabel.Text = points.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            graphics = canvasPanel.CreateGraphics();
            timer.Interval = 60;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void PaintCanvas(object sender, PaintEventArgs e)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.FillRectangle(new SolidBrush(Color.RoyalBlue), platform);
            graphics.FillEllipse(new SolidBrush(Color.Red), ball);
        }

        private void MovePlatform(object sender, MouseEventArgs e)
        {
            platform.X = e.X - 60;
            Refresh();
        }

        private void MoveBall()
        {
            if (ball.X + axisXSpeed < 0)
            {
                axisXSpeed += axisInvert;
            }
            if (ball.X + axisXSpeed > platformBox.Width - ball.Width)
            {
                axisXSpeed -= axisInvert;
            }
            if (ball.Y + axisYSpeed < 0)
            {
                axisYSpeed += axisInvert;
                if (axisYSpeed == 0)
                {
                    axisYSpeed += axisInvert;
                }
            }
            if (ball.Y  >= platform.Y)
            {
                timer.Stop();

                var gameOver = MessageBox.Show("Your score: " + points, "Game Over", MessageBoxButtons.OK);
                if (gameOver == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Restart();
                }
            }
            if (ball.IntersectsWith(platform))
            {
                axisYSpeed -= axisInvert;
                points++;
                CountLabel.Text = points.ToString();

            }
            ball.X += axisXSpeed;
            ball.Y += axisYSpeed;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            MoveBall();
            Refresh();
            Console.WriteLine("X speed: " + axisXSpeed + " Y speed: " + axisYSpeed);
        }

        private void Play_Click(object sender, EventArgs e)
        {
            timer.Start();
            Play.Enabled = false;
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
            Play.Enabled = true;
        }

        private void slowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
            SetSpeed(5);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
            SetSpeed(10);
        }

        private void fastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
            SetSpeed(15);
        }

        private void hardcoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
            SetSpeed(20);
        }
    }
}
