using System;
using System.Threading;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        Random random;
        bool gameStart = false;
        int count = 0;
        int gravity = -3;
        Thread tube1;
        Thread tube2;
        Thread grav;
        public Form1()
        {
            InitializeComponent();
            random = new Random();
        }
        void MoveTube1()
        {
            while (gameStart)
            {
                pictureBox1.Invoke(new Action(() => pictureBox1.Left -= 3));

                if (pictureBox1.Left < -pictureBox1.Width)
                {
                    count++;
                    pictureBox1.Invoke(new Action(()
                        =>
                    {
                        pictureBox1.Left = Width +
                        random.Next(pictureBox1.Width, pictureBox1.Width + 100);
                        label1.Text = "Score: " + count.ToString();
                        pictureBox1.Top =
                        random.Next(Height / 2, Height / 2 + pictureBox1.Height / 2);
                    }));
                }
                Thread.Sleep(5);
            }
        }

        void MoveTube2()
        {
            while(gameStart)
            {
                pictureBox2.Invoke(new Action(() => pictureBox2.Left -= 3));

                if (pictureBox2.Left < -pictureBox2.Width)
                    pictureBox2.Invoke(new Action(()
                       => {
                            pictureBox2.Left = Width + 
                            random.Next(pictureBox2.Width, pictureBox2.Width + 100);

                            pictureBox2.Top =
                            random.Next(-pictureBox2.Height /2, -10);
                        }));
                Thread.Sleep(5);
            }
        }

        void Gravity()
        {
            while(gameStart)
            {
                if(pictureBox3.Top <= 0 || pictureBox3.Top >= Height-100)
                {
                    pictureBox3.Invoke(
                    new Action(() => pictureBox3.Top = Height / 2));
                    gameStart = false;
                }
                else if(pictureBox3.Left + pictureBox3.Width - 5 > pictureBox1.Left //Если наша птичка между трубами
                    && pictureBox3.Left + 5 < pictureBox1.Left + pictureBox1.Width)
                {
                    if (pictureBox3.Top + 5 < pictureBox2.Top + pictureBox2.Height ||
                        pictureBox3.Top + pictureBox3.Height - 5 > pictureBox1.Top)
                    {
                        pictureBox3.Invoke(
                            new Action(() =>
                            {
                                pictureBox3.Top = Height / 2;
                                pictureBox1.Left = Width;
                                pictureBox2.Left = Width;
                            }));
                        gameStart = false;
                    }
                }

                if (gravity < 3) gravity++;
                pictureBox3.Invoke(
                    new Action(() =>
                    {
                        pictureBox3.Top += gravity;
                    }));
                Thread.Sleep(3);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameStart = true;
            tube1 = new Thread(MoveTube1);
            tube2 = new Thread(MoveTube2);
            grav = new Thread(Gravity);
            tube1.Start();
            tube2.Start();
            grav.Start();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            gravity = -8;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameStart = false;
            
            //Thread.Sleep(1000);
        }
    }
}
