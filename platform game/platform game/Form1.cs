using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platform_game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed;
        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;
        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;
        int force;

        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            player.Top += jumpSpeed;

            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;

            }

            if (jumping == true && force <0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;

            }
            else
            {
                jumpSpeed = 10;

            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;

                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                                
                            }






                        }
                        x.BringToFront();
                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score:" + score + Environment.NewLine + "So SAD dude try again!";
                        }
                    }

                }
            }


            horizontalPlatform.Left -= horizontalSpeed;

            if(horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }
            verticalPlatform.Top += verticalSpeed;
            if (verticalPlatform.Top < 20 || verticalPlatform.Top > 700)
            {
                verticalSpeed = -verticalSpeed;
            }

            verticalPlatform2.Top -= verticalSpeed;
            if (verticalPlatform2.Top < 50 || verticalPlatform.Top > 500)
            {
                verticalSpeed = -verticalSpeed;
            }


            enemyOne.Left -= enemyOneSpeed;
            if (enemyOne.Left < pictureBox8.Left ||enemyOne.Left + enemyOne.Width > pictureBox8.Left + pictureBox8.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }
            enemyTwo.Left += enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score:" + score + Environment.NewLine + " way to heaven ";
            }
            if(player.Bounds.IntersectsWith(door.Bounds) )
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score:" + score + Environment.NewLine + " Your journy is Completed";
            }
           
        
        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
               
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;

            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;

            }
            if(jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }
            
            // reset the position of all chaecters
            player.Left = 2;
            player.Top = 658;

            enemyOne.Left = 400;
            enemyTwo.Left = 360;

            horizontalPlatform.Left = 275;
            verticalPlatform.Top = 581;

            gameTimer.Start();

            


        }
    }
}
