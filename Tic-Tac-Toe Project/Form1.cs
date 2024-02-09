using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Project
{
    public partial class TicTacToeGame : Form
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

        private int moves = 0;

        int GameStatus = 0;

        List<Button> buttons;

        public TicTacToeGame()
        {
            InitializeComponent();
            RestartGame();
        }

        private void CPUmove(object sender, EventArgs e)
        {
            if (buttons.Count > 0)
            {
                int index = random.Next(buttons.Count);
                buttons[index].Enabled = false;
                currentPlayer = Player.O;
                buttons[index].Text = currentPlayer.ToString();
                buttons[index].BackColor = Color.Red;

                moves++;

                buttons.RemoveAt(index);
                CheckGame();
                CPUTimer.Stop();
            }
        }

        private void PlayerClickButton(object sender, EventArgs e)
        {
            var button = (Button)sender;

            currentPlayer = Player.X;
            button.Text = currentPlayer.ToString();
            button.Enabled = false;
            button.BackColor = Color.Cyan;

            moves++;

            buttons.Remove(button);
            CheckGame();
            CPUTimer.Start();
        }

        private void RestartGame(object sender, EventArgs e)
        {
            RestartGame();
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
                CPUTimer.Stop(); // Stops the CPUTimer
                MessageBox.Show("Player Wins"); // displays message
                playerWinCount++; // increase the player win count
                PlayerWinsText.Text = "Player Wins " + playerWinCount; // update player win count
                RestartGame(); // Restarts the game               

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
                    CPUTimer.Stop(); // stop the timer
                    MessageBox.Show("Computer Wins"); // displays message
                    CPUWinCount++; // increase the CPU win count
                    CPUWinsText.Text = "CPU Wins " + CPUWinCount; // update CPU win count
                    RestartGame(); ; // Restarts the game
            }
            else if (buttons.Count == 0)
            {
                CPUTimer.Stop();
                MessageBox.Show("DRAW");
                TieCount++; // increase the CPU win count
                DrawScoreText.Text = "Ties " + TieCount; // update CPU win count
                RestartGame(); ; // Restarts the game
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
        }

        private bool CheckDraw()
        {
            return moves == 9;
        }


        private void CheckDraw(object sender, EventArgs e)
        {
            if (moves == 9) 
            {
                CPUTimer.Stop(); // stop the timer
                MessageBox.Show("DRAW"); // displays message
                TieCount++; // increase the CPU win count
                DrawScoreText.Text = "Ties " + TieCount; // update CPU win count
                RestartGame(); ; // Restarts the game
            }
        }

    }
}
