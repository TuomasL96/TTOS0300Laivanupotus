using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Model
{
    enum ShipType {Carrier, Battleship, Destroyer, Submarine, PatrolBoat};

    class Ship
    {
        private int health;
        private readonly ShipType type; 

        private static Dictionary<ShipType, int> shipLengths = new Dictionary<ShipType, int>()
        {
            {ShipType.Carrier, 5},
            {ShipType.Battleship, 4},
            {ShipType.Destroyer, 3},
            {ShipType.Submarine, 3},
            {ShipType.PatrolBoat, 2}
        };

        public Ship(ShipType type)
        {
            this.type = type;
            Repair();
        }

        public void Repair()
        {
            health = shipLengths[type];
        }

        public int Length
        {
            get
            {
                return shipLengths[type];
            }
        }
        
        public bool IsSunk
        {
            get
            {
                if (health == 0)
                {
                    return true;
                }
                else { return false; }
            }
        }

        public bool FiredAt()
        {
            health--;
            return IsSunk;
        }
    }
}
