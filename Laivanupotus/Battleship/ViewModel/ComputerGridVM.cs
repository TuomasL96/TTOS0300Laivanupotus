using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleship.Model;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace Battleship.ViewModel
{
    class ComputerGridVM : GridVMBase
    {
        public ComputerGridVM(HumanPlayer humanPlayer, ComputerPlayer computerPlayer)
            : base(humanPlayer, computerPlayer)
        {
        }

        public override List<List<SeaSquare>> MyGrid
        {
            get
            {
                return humanPlayer.EnemyGrid;
            }
        }


        public override bool Clicked(SeaSquare square, bool automated) // palauttaa true kun peli loppuu
        {
            if (automated)
                humanPlayer.TakeTurnAutomated(computerPlayer);
            else
            {
                if (square.Type != SquareType.Unknown)
                {
                    MessageBox.Show("You have already fired here");
                    return false;
                }

             humanPlayer.TakeTurn(square.Row, square.Col, computerPlayer);
            }

            if (computerPlayer.GameEnd())
            {
                MessageBox.Show("Congratulations! You sank the entire enemy fleet!");
                return true;
            }
            else
            {
                computerPlayer.TakeTurn(humanPlayer);
                if (humanPlayer.GameEnd())
                {
                    MessageBox.Show("Your fleet got sunk, better luck next time");
                    return true;
                }
            }

            return false;
        }
    }
}
