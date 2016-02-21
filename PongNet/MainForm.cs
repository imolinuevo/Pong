using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
            int style = NativeWinAPI.GetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE);
            style |= NativeWinAPI.WS_EX_COMPOSITED;
            NativeWinAPI.SetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE, style);
            platform = new Rectangle(182, 400, 120, 40);
            ball = new Rectangle(new Random().Next(50, 450), 20, 25, 25);
            SetSpeed(10);
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
            graphics.FillRectangle(new SolidBrush(Color.Black), platform);
            graphics.FillEllipse(new SolidBrush(Color.Black), ball);
        }

        private void MovePlatform(object sender, MouseEventArgs e)
        {
            platform.X = e.X - 60;
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

                DialogResult result = MessageBox.Show("Your score: " + points, "Game Over", MessageBoxButtons.OK);
                if (result == System.Windows.Forms.DialogResult.OK)
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
            SetSpeed(10);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
            SetSpeed(20);
        }

        private void fastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
            SetSpeed(30);
        }

        private void hardcoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame();
            SetSpeed(40);
        }

        private void aboutTheDeveloperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Iñigo Molinuevo in 2016, find this proyect in: https://github.com/imolinuevo/Pong", "About the developer", MessageBoxButtons.OK);
        }

        private void top10ScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopScores topScores = new TopScores();
            topScores.Show();
        }
    }

    internal static class NativeWinAPI
    {
        internal static readonly int GWL_EXSTYLE = -20;
        internal static readonly int WS_EX_COMPOSITED = 0x02000000;

        [DllImport("user32")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    }
}
