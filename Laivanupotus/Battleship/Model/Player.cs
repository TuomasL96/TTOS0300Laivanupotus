using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Model
{
    class Player
    {
        const int GRID_SIZE = 10;
        static Random rnd = new Random();

        public List<List<SeaSquare>> MyGrid { get; set; }
        public List<List<SeaSquare>> EnemyGrid { get; set; }

        private List<Ship> myShips = new List<Ship>();
        private List<Ship> enemyShips = new List<Ship>();

        public Player()
        {
            MyGrid = new List<List<SeaSquare>>();
            EnemyGrid = new List<List<SeaSquare>>();

            for (int i = 0; i != GRID_SIZE; ++i)
            {
                MyGrid.Add(new List<SeaSquare>());
                EnemyGrid.Add(new List<SeaSquare>());

                for (int j = 0; j != GRID_SIZE; ++j)
                {
                    MyGrid[i].Add(new SeaSquare(i, j));
                    EnemyGrid[i].Add(new SeaSquare(i, j));
                }
            }

            foreach (ShipType type in Enum.GetValues(typeof(ShipType)))
            {
                myShips.Add(new Ship(type));
                enemyShips.Add(new Ship(type));
            }

            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i != GRID_SIZE; ++i)
            {
                for (int j = 0; j != GRID_SIZE; ++j)
                {
                    MyGrid[i][j].Reset(SquareType.Water);
                    EnemyGrid[i][j].Reset(SquareType.Unknown);
                }
            }

            myShips.ForEach(s => s.Repair());
            enemyShips.ForEach(s => s.Repair());
            PlaceShips();
        }
        // Tästä alkaa laivojen random sijoittelujutut
        private bool SquareFree(int row, int col)
        {
            return (MyGrid[row][col].ShipIndex == -1) ? true : false;
        }

        private bool PlaceVertical(int shipIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE - remainingLength);
            int startPosCol = rnd.Next(GRID_SIZE);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int row = startPosRow; tmp != 0; ++row)
                {
                    if (!SquareFree(row, startPosCol))
                        return false;
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int row = startPosRow; remainingLength != 0; ++row)
                {
                    MyGrid[row][startPosCol].Type = SquareType.Undamaged;
                    MyGrid[row][startPosCol].ShipIndex = shipIndex;
                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private bool PlaceHorizontal(int shipIndex, int remainingLength)
        {
            int startPosRow = rnd.Next(GRID_SIZE);
            int startPosCol = rnd.Next(GRID_SIZE - remainingLength);

            Func<bool> PlacementPossible = () =>
            {
                int tmp = remainingLength;
                for (int col = startPosCol; tmp != 0; ++col)
                {
                    if (!SquareFree(startPosRow, col))
                        return false;
                    --tmp;
                }
                return true;
            };

            if (PlacementPossible())
            {
                for (int col = startPosCol; remainingLength != 0; ++col)
                {
                    MyGrid[startPosRow][col].Type = SquareType.Undamaged;
                    MyGrid[startPosRow][col].ShipIndex = shipIndex;
                    --remainingLength;
                }
                return true;
            }

            return false;
        }

        private void PlaceShips()
        {
            bool startAgain = false;

            for (int i = 0; i != myShips.Count && !startAgain; ++i)
            {
                bool vertical = Convert.ToBoolean(rnd.Next(2));
                bool placed = false;

                int loopCounter = 0;
                for (; !placed && loopCounter != 10000; ++loopCounter)
                {
                    int remainingLength = myShips[i].Length;

                    if (vertical)
                        placed = PlaceVertical(i, remainingLength);
                    else
                        placed = PlaceHorizontal(i, remainingLength);
                }

                if (loopCounter == 10000)
                    startAgain = true;
            }

            if (startAgain)
                PlaceShips();
        }
        // pelin muu toiminta
        private void SinkShip(int i, List<List<SeaSquare>> grid)
        {
            foreach (var row in grid)
            {
                foreach (var square in row)
                {
                    if (square.ShipIndex == i)
                        square.Type = SquareType.Sunk;
                }
            }
        }

        private void MineSunk(int i)
        {
            SinkShip(i, MyGrid);
        }

        public void EnemySunk(int i)
        {
            SinkShip(i, EnemyGrid);
        }


        protected void Fire(int row, int col, Player otherPlayer)
        {
            int damagedIndex;
            bool isSunk;
            SquareType newType = otherPlayer.FiredAt(row, col, out damagedIndex, out isSunk);
            EnemyGrid[row][col].ShipIndex = damagedIndex;

            if (isSunk)
                EnemySunk(damagedIndex);
            else
                EnemyGrid[row][col].Type = newType;
        }

        public SquareType FiredAt(int row, int col, out int damagedIndex, out bool isSunk)
        {
            isSunk = false;
            damagedIndex = -1;

            switch (MyGrid[row][col].Type)
            {
                case SquareType.Water:
                    return SquareType.Water;
                case SquareType.Undamaged:
                    var square = MyGrid[row][col];
                    damagedIndex = square.ShipIndex;
                    if (myShips[damagedIndex].FiredAt())
                    {
                        MineSunk(square.ShipIndex);
                        isSunk = true;
                    } else {
                        square.Type = SquareType.Damaged;
                    }
                    return square.Type;
                case SquareType.Damaged:
                    goto default;
                case SquareType.Unknown:
                    goto default;
                case SquareType.Sunk:
                    goto default;
                default:
                    throw new Exception("fail");
            }
        }

        public bool GameEnd()
        {
            return myShips.All(ship => ship.IsSunk);
        }

        public void TakeTurnAutomated(Player otherPlayer) // tietokoneen ampuminen randomilla
        {
            bool takenShot = false;
            while (!takenShot)
            {
                int row = rnd.Next(GRID_SIZE);
                int col = rnd.Next(GRID_SIZE);

                if (EnemyGrid[row][col].Type == SquareType.Unknown)
                {
                    Fire(row, col, otherPlayer);
                    takenShot = true;
                }
            }
        }
    }
}
