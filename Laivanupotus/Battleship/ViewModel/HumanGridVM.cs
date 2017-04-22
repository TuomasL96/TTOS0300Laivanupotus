﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleship.Model;
using System.Windows.Data;
using System.ComponentModel;

namespace Battleship.ViewModel
{
    class HumanGridVM: GridVMBase
    {
        public HumanGridVM(HumanPlayer humanPlayer, ComputerPlayer computerPlayer)
            : base(humanPlayer, computerPlayer)
        {
        }

        //että saa suunniteltuu ulkonäön
        public HumanGridVM()
            : base(null, null)
        {
            humanPlayer = new HumanPlayer();
        }

        public override List<List<SeaSquare>> MyGrid
        {
            get
            {
                return humanPlayer.MyGrid;
            }
        }

        public override bool Clicked(SeaSquare content, bool automated)
        {
            return false;
        }
    }
}
