using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Battleship.ViewModel;
using Battleship.Model;
using Battleship.Properties;


namespace Battleship
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HumanPlayer humanPlayer;
        ComputerPlayer computerPlayer;

        HumanGridVM _humanGrid;
        ComputerGridVM _computerGrid;


        public MainWindow()
        {

            humanPlayer = new HumanPlayer();
            computerPlayer = new ComputerPlayer();

            _humanGrid = new HumanGridVM(humanPlayer, computerPlayer);
            _computerGrid = new ComputerGridVM(humanPlayer, computerPlayer);

            InitializeComponent();
            humanGrid.DataContext = _humanGrid;
            computerGrid.DataContext = _computerGrid;
        }

        private void playNewGame(object sender, ExecutedRoutedEventArgs e)
        {
            humanPlayer.Reset();
            computerPlayer.Reset();
            CustomSoundPlayer.PlayRestart();
        }

        private void exitGame(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void autoGame(object sender, ExecutedRoutedEventArgs e)
        {
            bool temp = CustomSoundPlayer.GetSoundStatus();
            CustomSoundPlayer.SetSound(false);
            playNewGame(sender, e); while (!_computerGrid.Clicked(null, true)) { }
            CustomSoundPlayer.SetSound(temp);
        }
           
        private void toggleSounds(object sender, ExecutedRoutedEventArgs e)
        {
            bool temp = CustomSoundPlayer.GetSoundStatus();
            if (temp == true)
                CustomSoundPlayer.SetSound(false);
            else { CustomSoundPlayer.SetSound(true); } 
        }
    }
}
