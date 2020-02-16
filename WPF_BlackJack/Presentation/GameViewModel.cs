using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_BlackJack.Business;
using WPF_BlackJack.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WPF_BlackJack.Presentation
{
    public class GameViewModel : ObservableObject
    {
        private Player _player;
        private Player _currentPlayer;
        private Dealer _dealer;
        private GameBusiness _gameBusiness;
        private GameBoard _gameBoard;

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        public Dealer Dealer 
        {
            get => _dealer;
            set 
            {
                _dealer = value;
                OnPropertyChanged(nameof(Dealer));
            }
        }

        public GameViewModel() 
        {
            _gameBusiness = new GameBusiness();
            _gameBoard = new GameBoard();

            InitializeGame();
        }

        private void InitializeGame()
        {
            _currentPlayer = _gameBusiness.GetCurrentPlayers();

            _player = _currentPlayer;
            _dealer = new Dealer("Mark");

            var value = _gameBoard.DealInitalCards();
            _dealer.Card.Add("Images/Cards/b1fv.bmp");
            _dealer.Card.Add("Images/Cards/" + value.dCards[1] + ".bmp");
            _player.Card.Add("Images/Cards/" + value.pCards[0] + ".bmp");
            _player.Card.Add("Images/Cards/" + value.pCards[1] + ".bmp");

            int.TryParse(value.pCards[0].Substring(1).TrimStart('0'), out int cardValue1);
            int.TryParse(value.pCards[1].Substring(1).TrimStart('0'), out int cardValue2);
            
            if (cardValue1 >=10)
            {
                cardValue1 = 10;
            }

            if (cardValue2 >=10)
            {
                cardValue2 = 10;
            }

            if (cardValue1 == 1 && cardValue2 <= 10)
            {
                cardValue1 = 11;
            }

            if (cardValue2 == 1 && cardValue1 <= 10)
            {
                cardValue2 = 11;
            }



            _player.CardTotal = cardValue1 + cardValue2;


        }

        private const int BET1 = 1;
        private const int BET5 = 5;
        private const int BET10 = 10;
        private const int BET25 = 25;
        private const int BET50 = 50;
        private const int BET100 = 100;

        internal void BetButtonCommand(string betCommand) 
        {
            switch (betCommand)
            {
                case "PlayerBet1":
                    _player.TotalBet += BET1;
                    _player.BankRoll -= BET1;
                    break;
                case "PlayerBet5":
                    _player.TotalBet += BET5;
                    _player.BankRoll -= BET5;
                    break;
                case "PlayerBet10":
                    _player.TotalBet += BET10;
                    _player.BankRoll -= BET10;
                    break;
                case "PlayerBet25":
                    _player.TotalBet += BET25;
                    _player.BankRoll -= BET25;
                    break;
                case "PlayerBet50":
                    _player.TotalBet += BET50;
                    _player.BankRoll -= BET50;
                    break;
                case "PlayerBet100":
                    _player.TotalBet += BET100;
                    _player.BankRoll -= BET100;
                    break;
                default:
                    break;
            }
        }


        private const int ACE_ADJUSTMENT = 10;
        public void PlayerDeal() 
        {
  
            string card = _gameBoard.DealCard();
            int.TryParse(card.Substring(1).TrimStart('0'), out int cardValue1);

            if (_player.CardTotal < 21)
            {
                _player.CardTotal += cardValue1;
            }

            if (_player.CardTotal >= 21)
            {
                foreach (var cards in _player.Card)
                {
                    if (cards.Substring(1).TrimStart('0') == "1")
                    {
                        _player.CardTotal -= ACE_ADJUSTMENT;
                    }
                }
            }

            _player.Card.Add("Images/Cards/" + card + ".bmp");
            OnPropertyChanged(nameof(Player));

        }

        public void DealerDeal()
        {
            while (_dealer.CardTotal < 17)
            {
                string cardValue = _gameBoard.DealCard();
                _dealer.Card.Add("Images/Cards/" + cardValue + ".bmp");
                OnPropertyChanged(nameof(Dealer));
                if (_dealer.CardTotal >= 17)
                {
                    _gameBoard.DealerStand();
                }
            }

        }

        public void Stand() 
        {
            DealerDeal();
        }



        internal void GameCommand(string commandName) 
        {
            switch (commandName)
            {
                case "NewGame":
                    InitializeGame();
                    OnPropertyChanged(nameof(Player));
                    OnPropertyChanged(nameof(Dealer));
                    break;
            }
        }


    } 

}
