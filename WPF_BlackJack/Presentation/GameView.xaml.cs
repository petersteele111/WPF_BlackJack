using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_BlackJack.Presentation;

namespace WPF_BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameView: Window
    {
        GameViewModel _gameViewModel;
        public GameView(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
            InitializeComponent();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            BlackJackRules blackJackRules = new BlackJackRules();
            blackJackRules.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) 
        {
            _gameViewModel.GameCommand(Quit.Name);
            Application.Current.Shutdown();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            _gameViewModel.GameCommand(NewGame.Name);
        }

        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
            _gameViewModel.PlayerHit();
        }

        private void StandButton_Click(object sender, RoutedEventArgs e)
        {
            _gameViewModel.PlayerStand();
        }

        private void BetButton_Click(object sender, RoutedEventArgs e) 
        {
            Button BetButton = sender as Button;
            switch (BetButton.Name)
            {
                case "PlayerBet1":
                    _gameViewModel.BetButtonCommand(BetButton.Name);
                    break;
                case "PlayerBet5":
                    _gameViewModel.BetButtonCommand(BetButton.Name);
                    break;
                case "PlayerBet10":
                    _gameViewModel.BetButtonCommand(BetButton.Name);
                    break;
                case "PlayerBet25":
                    _gameViewModel.BetButtonCommand(BetButton.Name);
                    break;
                case "PlayerBet50":
                    _gameViewModel.BetButtonCommand(BetButton.Name);
                    break;
                case "PlayerBet100":
                    _gameViewModel.BetButtonCommand(BetButton.Name);
                    break;
                default:
                    break;
            }
        }

        private void BetModifier_Click(object sender, RoutedEventArgs e)
        {
            Button BetModifier = sender as Button;
            switch (BetModifier.Name)
            {
                case "BetDecrease":
                    _gameViewModel.BetModifierCommand(BetModifier.Name);
                    break;
                case "BetIncrease":
                    _gameViewModel.BetModifierCommand(BetModifier.Name);
                    break;
                default:
                    break;
            }
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            Button ActionButton = sender as Button;
            _gameViewModel.ActionButtonCommand(ActionButton.Name);
        }
    }
}