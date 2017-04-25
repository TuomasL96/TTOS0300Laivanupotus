using System;
using System.Collections.Generic;
using Battleship.Model;
using System.Windows;


namespace Battleship.ViewModel
{
    class ComputerGridVM : GridVMBase
    {
        MySounds snd = new MySounds();
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
            if (automated) // pelaajalla mahdollisuus antaa tietokoneen ampua
                humanPlayer.TakeTurnAutomated(computerPlayer);
            else
            {
                if (square.Type != SquareType.Unknown)
                {
                    return false;
                }

             humanPlayer.TakeTurn(square.Row, square.Col, computerPlayer);
            }

            if (computerPlayer.AllShipsGone())
            {
                MessageBox.Show("Congratulations! You sank the entire enemy fleet!");
                snd.PlaySound(3);
                humanPlayer.Reset();
                computerPlayer.Reset();
                return true;
            }
            else
            {
                computerPlayer.TakeTurn(humanPlayer);
                if (humanPlayer.AllShipsGone())
                {
                    MessageBox.Show("Your lost the game!");
                    snd.PlaySound(2);
                    humanPlayer.Reset();
                    computerPlayer.Reset();
                    return true;
                }
            }
            return false;
        }
    }
}
