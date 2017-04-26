using System;
using System.Windows.Input;

namespace Battleship
{
    class Commands //näppäinkomentoja peliä varten
    {
        public static readonly RoutedUICommand ToggleSounds = new RoutedUICommand("Toggle sounds", "Toggle Sounds", typeof(Commands), new InputGestureCollection { new KeyGesture(Key.F1) });

        public static readonly RoutedUICommand NewGame = new RoutedUICommand("New Game", "New Game", typeof(Commands),new InputGestureCollection { new KeyGesture(Key.F2) });

        public static readonly RoutedUICommand AutomatedGame = new RoutedUICommand("Automated Game", "Automated Game", typeof(Commands),new InputGestureCollection { new KeyGesture(Key.F3) });

        public static readonly RoutedUICommand Exit = new RoutedUICommand("Exit", "Exit", typeof(Commands),new InputGestureCollection { new KeyGesture(Key.F4) });
    }
}