using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using Battleship.Properties;

namespace Battleship.Model
{
    class MySounds
    {
        public MySounds() { }
        public void PlaySound(int soundnumber)
        {
            switch (soundnumber)
            {
                case 0:
                    SoundPlayer explosion = new SoundPlayer(Resources.explosion);
                    explosion.Play();
                    break;
                case 1:
                    SoundPlayer miss = new SoundPlayer(Resources.menu_button2);
                    miss.Play();
                    break;
                case 2:
                    SoundPlayer lose = new SoundPlayer(Resources.lose);
                    lose.Play();
                    break;
                case 3:
                    SoundPlayer win = new SoundPlayer(Resources.menu);
                    win.Play();
                    break;
                case 4:
                    SoundPlayer Reset = new SoundPlayer(Resources.menu_button);
                    Reset.Play();
                    break;
                default:
                    break;
            }
        }
    }
}
