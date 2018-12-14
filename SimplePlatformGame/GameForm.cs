using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePlatformGame
{
    public partial class GameForm : Form
    {
        bool goleft = false,
            goright = false,
            jumping = false;
        int jumpSpeed = 10,
            force = 8,
            score = 0;

        public GameForm()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }

            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }

            if (jumping)
            {
                jumping = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            player.Top += jumpSpeed;

            if (jumping && force < 0)
            {
                jumping = false;
            }

            if (goleft)
            {
                player.Left -= 5;
            }

            if (goright)
            {
                player.Left += 5;
            }

            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }

            if (!jumping)
            {
                jumpSpeed = 12;
            }

            // floor collision
            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && (string)item.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(item.Bounds) &&
                        !jumping)
                    {
                        force = 8;
                        player.Top = item.Top - player.Height;
                    }
                }

                if (item is PictureBox && (string)item.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(item.Bounds) &&
                        !jumping)
                    {
                        this.Controls.Remove(item);
                        score++;
                        scoreLabel.Text = "Score: " + score;
                    }
                }
            }

            if (player.Bounds.IntersectsWith(door.Bounds))
            {
                timer1.Stop();
                MessageBox.Show("You WIN");
            }
        }
    }
}
