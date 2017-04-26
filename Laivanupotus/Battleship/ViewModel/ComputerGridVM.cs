using System;
using System.Collections.Generic;
using Battleship.Model;
using System.Windows;


namespace Battleship.ViewModel
{
    class ComputerGridVM : GridVMBase
    {

        public ComputerGridVM(HumanPlayer humanPlayer, ComputerPlayer computerPlayer)
            : base(humanPlayer, computerPlayer)
        { }

        public override List<List<SeaSquare>> MyGrid
        {
            get { return _humanPlayer.EnemyGrid; }
        }

        public override bool Clicked(SeaSquare square, bool automated) // palauttaa true kun peli loppuu
        {
            if (automated) // pelaajalla mahdollisuus antaa tietokoneen ampua peli läpi
            _humanPlayer.TakeTurnAutomated(_computerPlayer);
            else
            {
                if (square.Type != SquareType.Unknown)
                {
                    return false;
                }
             _humanPlayer.TakeTurn(square.Row, square.Col, _computerPlayer);
            }

            if (_computerPlayer.AllShipsGone())
            {
                MessageBox.Show("Congratulations! You sank the entire enemy fleet!");
                _humanPlayer.Reset();
                _computerPlayer.Reset();
                CustomSoundPlayer.PlayWin();
                return true;
            }
            else
            {
                _computerPlayer.TakeTurn(_humanPlayer);
                if (_humanPlayer.AllShipsGone())
                {
                    MessageBox.Show("You lost the game!");
                    _humanPlayer.Reset();
                    _computerPlayer.Reset();
                    CustomSoundPlayer.PlayLose();
                    return true;
                }
            }
            return false;
        }
    }
}
