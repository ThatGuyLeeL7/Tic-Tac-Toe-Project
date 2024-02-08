using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tic_Tac_Toe_Project
{
    public partial class TicTacToeGame : Form
    {
        int PlayerTurn = 0;
        int CPUturn = 0;

        //this is what happens when the 7 button is clicked
        private void numbbtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Text = "x";
            PlayerTurn++;
        }
         void computerMove() {
            while (CPUturn < PlayerTurn)
            {
                Random rand = new Random();

                // Generate and print a random number between 1 and 9
                int randomNumber = rand.Next(1, 10); // Generates a number between 1 and 9 (inclusive)

                CPUturn++;
            }


        }

    }

}
