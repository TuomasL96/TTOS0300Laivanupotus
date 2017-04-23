using System;
using System.Windows.Input;

namespace Battleship
{
    class Commands //näppäinkomentoja peliä varten
    {
        public static readonly RoutedUICommand NewGame = new RoutedUICommand("New Game", "New Game", typeof(Commands),new InputGestureCollection { new KeyGesture(Key.F3) });
        public static readonly RoutedUICommand Exit = new RoutedUICommand("Exit", "Exit", typeof(Commands),new InputGestureCollection { new KeyGesture(Key.F4) });
    }
}
