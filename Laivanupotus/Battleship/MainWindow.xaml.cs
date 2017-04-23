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
using System.Media;

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
            SoundPlayer snd = new SoundPlayer(Battleship.Properties.Resources.menu_button);
            snd.Play();
            humanPlayer.Reset();
            computerPlayer.Reset();            
        }

        private void exitGame(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
