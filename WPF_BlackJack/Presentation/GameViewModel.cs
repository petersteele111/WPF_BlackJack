using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_BlackJack.Business;
using WPF_BlackJack.Models;

namespace WPF_BlackJack.Presentation
{
    public class GameViewModel : ObservableObject
    {
        private Dealer _dealer;
        private Player _playerOne;
        private Player _playerTwo;
        private (Player playerOne, Player playerTwo) _currentPlayers;
        private GameBusiness _gameBusiness;

        public Dealer Dealer
        {
            get => _dealer;
            set
            {
                _dealer = value;
                OnPropertyChanged(nameof(Dealer));
            }
        }

        public Player PlayerOne
        {
            get => _playerOne;
            set
            {
                _playerOne = value;
                OnPropertyChanged(nameof(PlayerOne));
            }
        }

        public Player PlayerTwo 
        {
            get => _playerTwo;
            set 
            {
                _playerTwo = value;
                OnPropertyChanged(nameof(PlayerTwo));
            }
        }

        public GameViewModel() 
        {
            _gameBusiness = new GameBusiness();

            InitializeGame();
        }

        private void InitializeGame()
        {
            _currentPlayers = _gameBusiness.GetCurrentPlayers();

            _playerOne = _currentPlayers.playerOne;
            _playerTwo = _currentPlayers.playerTwo;
        }
    }

}
