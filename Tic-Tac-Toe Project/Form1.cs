using System;
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
using System.IO;
using static System.Windows.Forms.LinkLabel;

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// TINFO 200 A, Winter 2024
// UWTacoma SET, Joel Abraham, Lee Nguyen, Paul Sok
// 2024-02-10 - Cs1tic - C# Programming Project - Tic Tac Toe Game
// This program creates a Tic-Tac-Toe game for users to interact with.
// The program displays a textbox that the user can enter their name in.
// The program displays a Start Game button that begins a new game and displays their name.
// There is a label that displays the user's input in the textbox after the Start Game button is clicked on.
// There are games score that displays wins for the player and cpu and ties.
// The game score keeps track of Player wins, Computer wins, and Cat's games since the last time the app was executed.
// The user can press any one of the nine buttons and it will set the button accordingly to the player's assign value "X" or "O"
// Whether the player plays as "X" or "O" is random.
// Getting three buttons in a row results in a win for either the player or CPU if not then draw
// The label that displays Tic-Tac-Toe will change to show the result of the game
// A new game will immediately begin with one of the score tracker increasing by one
// The user can click the stop button to stop the game
// The user can click the exit button to exit the game

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Change History
//  Date ------- Developer --- Description
//  2024-02-08 - lee5457 - Initial creation of file to represent this tic tac toe game
//  2024-02-08 - lee5457 - Created TableLayoutPanel1
//  2024-02-08 - lee5457 - Created buttons to function as game squares
//  2024-02-08 - lee5457 - Created Score trackers for player wins, computer wins and ties
//  2024-02-08 - lee5457 - Edited the window to allow buttons and text to scale with the window size
//  2024-02-08 - lee5457 - Created PlayerClickButton() method for user to interact with buttons
//  2024-02-08 - lee5457 - Created buttons to function as game squares
//  2024-02-08 - jabrah2 - Created StartGame() method for the Start button
//  2024-02-08 - jabrah2 - Created StopGame() method for the stop button
//  2024-02-08 - jabrah2 - Created ExitGame() methods for the exit button
//  2024-02-08 - lee5457 - Created CPUMove() method to allow CPU to make an action
//  2024-02-08 - lee5457 - Created CheckGame() method to check the game for a win or loss 
//
//  2024-02-09 - lee5457 - Created RandomizePlayers() method to randomize if player gets "X" or "O"
//  2024-02-09 - pauls752 - Edited the CheckGame() method to include a tie
//  2024-02-09 - pauls752 - Edited the CheckGame() method to increase scores for player or CPU when win or lose or ties
//
//  2024-02-10 - lee5457 - Added user name input, and display message label on the GUI
//  2024-02-10 - lee5457 - Added StreamReader that reads and writes Wins.txt, Losses.txt, and Draws.txt files
//  2024-02-10 - lee5457 - Edited StartGame() method to the app to keep track of Player wins, Computer wins, and Cat's games since the last time the app was executed.


namespace Tic_Tac_Toe_Project
{
    public partial class TicTacToe : Form
    {
        public enum Player
        {
            X, O
        }
        string filePathWins = "Wins.txt"; // References the path to the txt file
        string filePathLosses = "Losses.txt"; // References the path to the txt file
        string filePathTies = "Draws.txt"; // References the path to the txt file

        Random random = new Random();
        int playerWinCount = 0; // player win count
        int CPUWinCount = 0; // computer win count
        int TieCount = 0; // tie count

        List<Button> buttons;
        Player playerA;
        Player playerB;
        Random rand = new Random();

        public TicTacToe()
        {
            InitializeComponent();
            StartGame();
            StopGame();
            ExitGame();
        }


        private void RandomizePlayers() 
        {
            if (rand.Next(2) == 0) 
            {
                playerA = Player.X; // player will play as X
                playerB = Player.O; // Computer will player as O
            }
            else
            {
                playerA = Player.O; // player will play as O
                playerB = Player.X; // Computer will play as X
            }
        }

        private void CPUmove(object sender, EventArgs e) //this method allows for the CPU to make their turn
        {
            if (buttons.Count > 0) //will activate if there are buttons available to click
            {
                int index = random.Next(buttons.Count); //This makes it so that the Computer selects a button at random
                buttons[index].Enabled = false;
                buttons[index].Text = playerB.ToString(); //Sets the text of the button depending on the CPU
                buttons[index].BackColor = Color.Red; //Sets background color of player selected cells to red
                buttons.RemoveAt(index); //Removes the selected button from list of available buttons
                CheckGame();
                CPUTimer.Stop();
            } 
        }

        private void PlayerClickButton(object sender, EventArgs e)
        {
            var button = (Button)sender; //allows the player to interact with the button
            button.Text = playerA.ToString();
            button.Enabled = false;
            button.BackColor = Color.LightGreen; //Sets background color of player selected cells to light green
            buttons.Remove(button); //Removes the selected button from list of available buttons
            CheckGame();
            CPUTimer.Start();
        }

        private void StartGame(object sender, EventArgs e)
        {
            NameLabel.Text = textBox1.Text; //Sets the Notification label to display what is inputed in the text box
            textBox1.Text = "Welcome!"; //Displays a welcome message after input in text box

            StreamWriter WinsFile = new StreamWriter(filePathWins); //references Wins.txt file
            
            WinsFile.WriteLine(NameLabel.Text + " Wins: 0"); //Writes contents of files and resets wins

            WinsFile.Close(); //Close the file

            StreamWriter LossesFile = new StreamWriter(filePathLosses); //references Losses.txt file
            
            LossesFile.WriteLine("CPU Wins: 0"); //Resets CPU wins label

            LossesFile.Close(); //Close the file

            StreamWriter TiesFile = new StreamWriter(filePathTies); //references Draws.txt file
            //Write a line of text
            TiesFile.WriteLine("Ties: 0"); //Resets ties label

            TiesFile.Close(); //Close the file

            StartGame(); //starts the game
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
                    PlayerWinsText.Text = NameLabel.Text + " Wins: " + playerWinCount; // update player win count

                    StreamWriter sw = new StreamWriter(filePathWins); 
  
                    sw.WriteLine(NameLabel.Text + " Wins: " + playerWinCount); //rewrites a new score for the player win count in file
                 
                    sw.Close(); //Close the file
                }
                else
                {
                    NotificationBox.Text = "CPU WINS";
                    CPUWinCount++; //increase the CPU win count
                    CPUWinsText.Text = "CPU Wins " + CPUWinCount; //update CPU win count

                    StreamWriter sw = new StreamWriter(filePathLosses);
   
                    sw.WriteLine("CPU Wins: " + CPUWinCount); //rewrites a new score for the CPU win count in file

                    sw.Close(); //Close the file
                }
                StartGame(); // Starts the game
            }
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
                    NotificationBox.Text = NameLabel.Text + " " + "Wins"; //Displays text in label box
                    PlayerWinsText.Text = NameLabel.Text + " Wins: " + playerWinCount; // update player win count

                    StreamWriter sw = new StreamWriter(filePathWins);

                    sw.WriteLine(NameLabel.Text + " Wins: " + playerWinCount); //rewrites a new score for the player win count in file

                    sw.Close(); //Close the file
                }
                else
                {
                    NotificationBox.Text = "CPU WINS"; //Displays text in label box
                    CPUWinCount++; //increase the CPU win count
                    CPUWinsText.Text = "CPU Wins " + CPUWinCount; // update CPU win count

                    StreamWriter sw = new StreamWriter(filePathLosses);  
     
                    sw.WriteLine("CPU Wins: " + CPUWinCount); //rewrites a new score for the CPU win count in file

                    sw.Close(); //Close the file
                }
                StartGame(); // Starts the game
            }
            else if (buttons.Count == 0)
            {
                CPUTimer.Stop();
                NotificationBox.Text = "Draw"; //Displays text in label box
                TieCount++; // increase the CPU win count
                DrawScoreText.Text = "Ties: " + TieCount; // update CPU win count

                StreamWriter sw = new StreamWriter(filePathTies);
        
                sw.WriteLine("Ties: " + TieCount); //rewrites a new score for tie count in file

                sw.Close(); //Close the file

                StartGame(); // Starts the game
            }
        }

        private void StartGame()
        {
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9};

            //Pass the file path and file name to the StreamReader constructor
            StreamReader WinsText = new StreamReader(filePathWins); //Reads the contents of Wins.txt
            StreamReader LossText = new StreamReader(filePathLosses); //Reads the contents of Losses.txt
            StreamReader TieText = new StreamReader(filePathTies); //Reads the contents of Ties.txt

            PlayerWinsText.Text = WinsText.ReadLine(); //Sets the contents of Wins.txt as PlayerWinsText
            CPUWinsText.Text = LossText.ReadLine(); //Sets the contents of Losses.txt as CPUWinsText
            DrawScoreText.Text = TieText.ReadLine(); //Sets the contents of Ties.txt as DrawScoreText
            WinsText.Close(); //closes Wins.txt
            LossText.Close(); //closes Losses.txt
            TieText.Close(); //closes Ties.txt

            foreach (Button x in buttons)
            {
                x.Enabled = true; //Enables all the buttons to interact with
                x.Text = " "; //Sets the text of the buttons to be blank
                x.BackColor = DefaultBackColor; //Sets the background color of the buttons to default
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
    }
}
