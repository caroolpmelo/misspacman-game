using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MissPacMan
{
    public partial class GameForm : Form
    {
        bool goup,
            godown,
            goleft,
            goright;
        int speed = 5, // player speed
            ghost1 = 8,
            ghost2 = 8,
            ghost3x = 8, // horizontal speed
            ghost3y = 8, // vertical speed
            score = 0;
        

        public GameForm()
        {
            InitializeComponent();
            label2.Visible = false;
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                pacman.Image = Properties.Resources.Left;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                pacman.Image = Properties.Resources.Right;
            }

            if (e.KeyCode == Keys.Up)
            {
                goup = true;
                pacman.Image = Properties.Resources.Up;
            }

            if (e.KeyCode == Keys.Down)
            {
                godown = true;
                pacman.Image = Properties.Resources.down;
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

            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Score: " + score;

            if (goleft)
            {
                pacman.Left -= speed; // .Left is the pixel position on screen
            }

            if (goright)
            {
                pacman.Left += speed;
            }

            if (goup)
            {
                pacman.Top -= speed;
            }

            if (godown)
            {
                pacman.Top += speed;
            }

            // moving red and yellow ghosts
            redGhost.Left += ghost1;
            yellowGhost.Left += ghost2;

            // redGhost wall collision
            if (redGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                ghost1 = -ghost1; // inverts velocity
            }
            else if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                ghost1 = -ghost1;
            }

            // yellowGhost wall collision
            if (yellowGhost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                ghost2 = -ghost2;
            }
            else if (yellowGhost.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                ghost2 = -ghost2;
            }

            // checks walls, ghosts and points
            foreach (Control item in this.Controls)
            {
                if (item is PictureBox && // item is wall or ghost
                    (string)item.Tag == "wall" || (string)item.Tag == "ghost")
                {
                    if (((PictureBox)item).Bounds.IntersectsWith(pacman.Bounds) ||
                        score == 30) // collides or score=30
                    {
                        pacman.Left = 0;
                        pacman.Top = 25;
                        label2.Text = "GAME OVER";
                        label2.Visible = true;
                        timer1.Stop();
                    }
                }

                if (item is PictureBox && (string)item.Tag == "coin") // catch coin
                {
                    if (((PictureBox)item).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        this.Controls.Remove(item); // remove coin
                        score++;
                    }
                }
            }

            // moving pinkGhost
            pinkGhost.Left += ghost3x;
            pinkGhost.Top += ghost3y;

            // pinkGhost collision (edges of screen or walls)
            if (pinkGhost.Left < 1 ||
                pinkGhost.Left + pinkGhost.Width > ClientSize.Width - 2 ||
                (pinkGhost.Bounds.IntersectsWith(pictureBox1.Bounds)) ||
                (pinkGhost.Bounds.IntersectsWith(pictureBox2.Bounds)) ||
                (pinkGhost.Bounds.IntersectsWith(pictureBox3.Bounds)) ||
                (pinkGhost.Bounds.IntersectsWith(pictureBox4.Bounds)))
            {
                ghost3x = -ghost3x;
            }

            if (pinkGhost.Top < 1 ||
                pinkGhost.Top + pinkGhost.Height > ClientSize.Height - 2)
            {
                ghost3y = -ghost3y;
            }
        }
    }
}
