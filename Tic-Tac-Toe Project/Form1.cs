﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static Tic_Tac_Toe_Project.TicTacToe;

namespace Tic_Tac_Toe_Project
{
    public partial class TicTacToe : Form
    {
        public enum Player
        {
            X, O
        }

        Player currentPlayer;
        Random random = new Random();
        int playerWinCount = 0;
        int CPUWinCount = 0;
        int TieCount = 0;

        List<Button> buttons;
        Player playerA;
        Player playerB;
        Random rand = new Random();

        private void RandomizePlayers()
        {
            if (rand.Next(2) == 0)
            {
                playerA = Player.X;
                playerB = Player.O;
            }
            else
            {
                playerA = Player.O;
                playerB = Player.X;
            }
        }

        public TicTacToe()
        {
            InitializeComponent();
            RestartGame();
            StopGame();
            ExitGame();
        }

       
        private void CPUmove(object sender, EventArgs e)
        {

            if (buttons.Count > 0)
            {
                int index = random.Next(buttons.Count);
                buttons[index].Enabled = false;
                buttons[index].Text = playerB.ToString();
                buttons[index].BackColor = Color.Red;
                buttons.RemoveAt(index);
                CheckGame();
                CPUTimer.Stop();
            }
        }

        private void PlayerClickButton(object sender, EventArgs e)
        {
            var button = (Button)sender;

            button.Text = playerA.ToString();
            
            button.Enabled = false;
            button.BackColor = Color.Cyan;
            buttons.Remove(button);
            CheckGame();
            CPUTimer.Start();
        }

        private void RestartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void StopGame(object sender, EventArgs e)
        {
            StopGame();
        }

        private void ExitGame(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckGame()
        {
            if (button1.Text == "X" && button2.Text == "X" && button3.Text == "X"
                    || button4.Text == "X" && button5.Text == "X" && button6.Text == "X"
                    || button7.Text == "X" && button9.Text == "X" && button8.Text == "X"
                    || button1.Text == "X" && button4.Text == "X" && button7.Text == "X"
                    || button2.Text == "X" && button5.Text == "X" && button8.Text == "X"
                    || button3.Text == "X" && button6.Text == "X" && button9.Text == "X"
                    || button1.Text == "X" && button5.Text == "X" && button9.Text == "X"
                    || button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                // If any of the above conditions are met
                CPUTimer.Stop();
                if (playerA == Player.X)
                {
                    playerWinCount++; // increase the player win count
                    NotificationBox.Text = NameLabel.Text + " " + "Wins"; // displays message
                    PlayerWinsText.Text = "Player Wins " + playerWinCount; // update player win count
                }
                else
                {
                    NotificationBox.Text = "CPU WIN";
                }
                RestartGame();
                StopGame(); // Stops the game
                ExitGame(); // Exits the game
            }
            // below if statement is for when the CPU wins the game
            else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "O"
            || button4.Text == "O" && button5.Text == "O" && button6.Text == "O"
            || button7.Text == "O" && button9.Text == "O" && button8.Text == "O"
            || button1.Text == "O" && button4.Text == "O" && button7.Text == "O"
            || button2.Text == "O" && button5.Text == "O" && button8.Text == "O"
            || button3.Text == "O" && button6.Text == "O" && button9.Text == "O"
            || button1.Text == "O" && button5.Text == "O" && button9.Text == "O"
            || button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {
                // If any of the above conditions are met
                CPUTimer.Stop();
                if (playerA == Player.O)
                {
                    playerWinCount++; // increase the player win count
                    NotificationBox.Text = NameLabel.Text + " " + "Wins"; // displays message
                    PlayerWinsText.Text = "Player Wins " + playerWinCount; // update player win count
                }
                else
                {
                    NotificationBox.Text = "CPU WIN";
                }
                RestartGame();
                StopGame(); // Stops the game
                ExitGame(); // Exits the game
            }
            else if (buttons.Count == 0)
            {
                CPUTimer.Stop();
                NotificationBox.Text = "Draw";
                TieCount++; // increase the CPU win count
                DrawScoreText.Text = "Ties " + TieCount; // update CPU win count
                RestartGame(); ; // Restarts the game
                StopGame(); // Stops the game
                ExitGame(); // Exits the game
            }
        }

        private void RestartGame()
        {
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9};

            foreach (Button x in buttons)
            {
                x.Enabled = true;
                x.Text = " ";
                x.BackColor = DefaultBackColor;
            }
            RandomizePlayers();
            if (playerA == Player.O)
            {
                CPUTimer.Start();
            }
        }
        private void StopGame()
        {
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            foreach (Button x in buttons)
            {
                x.Enabled = true;
                x.Text = " ";
                x.BackColor = DefaultBackColor;
            }
        }

        private void ExitGame()
        {
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            foreach (Button x in buttons)
            {
                x.Enabled = true;
                x.Text = " ";
                x.BackColor = DefaultBackColor;
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            NameLabel.Text = textBox1.Text;
            textBox1.Text = "Welcome!";
        }
    }
}
