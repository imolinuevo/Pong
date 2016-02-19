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

        public MainForm()
        {
            InitializeComponent();
            platform = new Rectangle(182, 400, 120, 40);
            ball = new Rectangle(50, 50, 25, 25);
            axisXSpeed = 5;
            axisYSpeed = 5;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            graphics = canvasPanel.CreateGraphics();
            timer.Interval = 60;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
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
                axisXSpeed += 10;
            }
            if (ball.X + axisXSpeed >= platformBox.Width)
            {
                axisXSpeed -= 10;
            }
            if (ball.Y + axisYSpeed < 0)
            {
                axisYSpeed += 5;
                if (axisYSpeed == 0)
                {
                    axisYSpeed += 5;
                }
            }
            if (ball.Y  >= platform.Y)
            {
                axisXSpeed = 0;
                axisYSpeed = 0;
                timer.Stop();

                var gameOver = MessageBox.Show("GAME OVER", "Message", MessageBoxButtons.OK);
                if (gameOver == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Restart();
                }
            }
            if (ball.IntersectsWith(platform))
            {
                axisYSpeed -= 10;
            }
            ball.X += axisXSpeed;
            ball.Y += axisYSpeed;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            MoveBall();
            Refresh();
        }
    }
}
