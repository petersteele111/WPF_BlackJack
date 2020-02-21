using WPF_BlackJack.Business;
using WPF_BlackJack.Models;

namespace WPF_BlackJack.Presentation
{
    public class GameViewModel : ObservableObject
    {
        #region Fields
        private Player _player;
        private Player _currentPlayer;
        private Dealer _dealer;
        private GameBusiness _gameBusiness;
        private GameBoard _gameBoard;
        #endregion

        #region Models
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

        private string _messages;

        public string Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        private bool _canClick;

        public bool CanClick
        {
            get => _canClick;
            set
            {
                _canClick = value;
                OnPropertyChanged(nameof(CanClick));
            }
        }

        private bool _isVisibile;

        public bool IsVisibile
        {
            get => _isVisibile;
            set
            {
                _isVisibile = value;
                OnPropertyChanged(nameof(IsVisibile));
            }
        }

        #endregion

        #region Start Game
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
            _dealer.HiddenCard = ("Images/Cards/" + value.dCards[0] + ".bmp");
            _dealer.Card.Add("Images/Cards/" + value.dCards[1] + ".bmp");
            _player.Card.Add("Images/Cards/" + value.pCards[0] + ".bmp");
            _player.Card.Add("Images/Cards/" + value.pCards[1] + ".bmp");

            int.TryParse(value.dCards[0].Substring(1).TrimStart('0'), out int dCardValue1);
            int.TryParse(value.dCards[1].Substring(1).TrimStart('0'), out int dCardValue2);


            int.TryParse(value.pCards[0].Substring(1).TrimStart('0'), out int cardValue1);
            int.TryParse(value.pCards[1].Substring(1).TrimStart('0'), out int cardValue2);

            if (cardValue1 >= 10)
            {
                cardValue1 = 10;
            }

            if (cardValue2 >= 10)
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


            if (dCardValue1 >= 10)
            {
                dCardValue1 = 10;
            }

            if (dCardValue2 >= 10)
            {
                dCardValue2 = 10;
            }

            if (dCardValue1 == 1 && dCardValue2 <= 10)
            {
                dCardValue1 = 11;
            }

            if (dCardValue2 == 1 && dCardValue1 <= 10)
            {
                dCardValue2 = 11;
            }



            _player.CardTotal = cardValue1 + cardValue2;
            _dealer.HiddenCardTotal = dCardValue1 + dCardValue2;
            _gameBoard.currentGameState = GameBoard.GameState.PlayerBet;
            _canClick = _gameBoard.Clickable();
            _isVisibile = false;
            _messages = "Player Bet";
            OnPropertyChanged(nameof(CanClick));
            OnPropertyChanged(nameof(IsVisibile));


        }
        #endregion

        #region Reset/New/GameBoard

        private void ResetBoard()
        {
            _gameBoard = new GameBoard();
            _player.Card.Clear();
            _messages = "Player Bet";
            InitializeGame();
            OnPropertyChanged(nameof(Messages));
        }

        private void NewGame() 
        {
            ResetBoard();
            _player.BankRoll = 1000;
            _player.TotalBet = 0;
            OnPropertyChanged(nameof(Player));
            OnPropertyChanged(nameof(Dealer));
        }

        private void NewRound()
        {
            _player.TotalBet = 0;
            _player.TotalWinnings = 0;
            ResetBoard();
            OnPropertyChanged(nameof(Player));
            OnPropertyChanged(nameof(Dealer));
        }

        #endregion

        #region Game Logic

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
                    _player.BetAmount = BET1;
                    _player.TotalBet += BET1;
                    _player.BankRoll -= BET1;
                    break;
                case "PlayerBet5":
                    _player.BetAmount = BET5;
                    _player.TotalBet += BET5;
                    _player.BankRoll -= BET5;
                    break;
                case "PlayerBet10":
                    _player.BetAmount = BET10;
                    _player.TotalBet += BET10;
                    _player.BankRoll -= BET10;
                    break;
                case "PlayerBet25":
                    _player.BetAmount = BET25;
                    _player.TotalBet += BET25;
                    _player.BankRoll -= BET25;
                    break;
                case "PlayerBet50":
                    _player.BetAmount = BET50;
                    _player.TotalBet += BET50;
                    _player.BankRoll -= BET50;
                    break;
                case "PlayerBet100":
                    _player.BetAmount = BET100;
                    _player.TotalBet += BET100;
                    _player.BankRoll -= BET100;
                    break;
                default:
                    break;
            }
        }

        internal void BetModifierCommand(string betModifier)
        {
            switch (betModifier)
            {
                case "BetDecrease":
                    _player.TotalBet -= _player.BetAmount;
                    _player.BankRoll += _player.BetAmount;
                    break;
                case "BetIncrease":
                    _player.TotalBet += _player.BetAmount;
                    _player.BankRoll -= _player.BetAmount;
                    break;
                default:
                    break;
            }
        }


        private const int ACE_ADJUSTMENT = 10;
        public void PlayerHit() 
        {
            if (_player.TotalBet >= 1)
            {
                _gameBoard.currentGameState = GameBoard.GameState.PlayerHit;
                _messages = "Player Hit";
                string card = _gameBoard.DealCard();
                int.TryParse(card.Substring(1).TrimStart('0'), out int cardValue1);

                if (cardValue1 >= 10)
                {
                    cardValue1 = 10;
                }

                _player.CardTotal += cardValue1;
                _player.Card.Add("Images/Cards/" + card + ".bmp");
                CheckPlayerWinCondition();
                OnPropertyChanged(nameof(Player));
                OnPropertyChanged(nameof(Messages));
            }
            else
            {
                _messages = "You Must First Place A Bet!";
                OnPropertyChanged(nameof(Messages));
            }


        }

        private void CheckPlayerWinCondition()
        {
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

            if (_player.CardTotal > 21)
            {
                _gameBoard.currentGameState = GameBoard.GameState.PlayerBust;
                _messages = "Player Bust";

                OnPropertyChanged(nameof(Messages));
            }

            if (_player.CardTotal == 21)
            {
                _gameBoard.currentGameState = GameBoard.GameState.PlayerBlackJack;
                _messages = "Player BlackJack";
                _player.TotalWinnings = _player.TotalBet * 2;
                _player.BankRoll = _player.TotalWinnings;
                OnPropertyChanged(nameof(Player));
            }
        }

        public void PlayerStand()
        {
            if (_player.TotalBet >= 1 )
            {
                _gameBoard.currentGameState = GameBoard.GameState.PlayerStand;
                _canClick = _gameBoard.Clickable();
                _messages = "Player Stand";
                OnPropertyChanged(nameof(Messages));
                OnPropertyChanged(nameof(CanClick));
                DealerDeal();
            }
            else
            {
                _messages = "You Cannot Stand Without Betting First! Place A Bet!";
                OnPropertyChanged(nameof(Messages));
            }

        }

        private void CheckDealerWinCondition()
        {
            if (_dealer.CardTotal >= 21)
            {
                foreach (var cards in _player.Card)
                {
                    if (cards.Substring(1).TrimStart('0') == "1")
                    {
                        _dealer.CardTotal -= ACE_ADJUSTMENT;
                    }
                }
            }
        }

        public void DealerDeal()
        {
            _gameBoard.currentGameState = GameBoard.GameState.DealerDraw;
            _messages = "Dealer Draw";
            OnPropertyChanged(nameof(Messages));
            _dealer.CardTotal = _dealer.HiddenCardTotal;
            _dealer.Card[0] = _dealer.HiddenCard;
            while (_dealer.CardTotal < 17)
            {
                string dCard = _gameBoard.DealCard();
                int.TryParse(dCard.Substring(1).TrimStart('0'), out int dCardValue1);

                if (dCardValue1 >= 10)
                {
                    dCardValue1 = 10;
                }

                _dealer.CardTotal += dCardValue1;
                _dealer.Card.Add("Images/Cards/" + dCard + ".bmp");
                CheckDealerWinCondition();
            }
            CheckGameWinCondition();
            OnPropertyChanged(nameof(Dealer));
            OnPropertyChanged(nameof(Player));

        }

        public void CheckGameWinCondition()
        {
            if (_dealer.CardTotal > 21)
            {
                _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
                _messages = "Dealer Bust";
                _player.TotalWinnings = _player.TotalBet * 2;
                _player.BankRoll += _player.TotalWinnings;
            }
            else if (_dealer.CardTotal == 21)
            {
                _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
                _messages = "Dealer BlackJack";
            }
            else if (_player.CardTotal > _dealer.CardTotal)
            {
                _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
                _messages = "Player Won";
                _player.TotalWinnings = _player.TotalBet * 2;
                _player.BankRoll += _player.TotalWinnings;
            }
            else if (_player.CardTotal < _dealer.CardTotal)
            {
                _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
                _messages = "Dealer Won";
            }
            else if (_player.CardTotal == _dealer.CardTotal)
            {
                _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
                _messages = "Draw";
                _player.TotalWinnings = _player.TotalBet;
                _player.BankRoll += _player.TotalWinnings;
            }

           _isVisibile = _gameBoard.Visible();

            OnPropertyChanged(nameof(Dealer));
            OnPropertyChanged(nameof(Player));
            OnPropertyChanged(nameof(Messages));
            OnPropertyChanged(nameof(IsVisibile));
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
                    NewGame();
                    break;
                case "Quit":
                    _player.BankRoll += _player.TotalBet;
                    _player.TotalBet = 0;
                    _gameBusiness.SaveAllPlayers();
                    break;
            }
        }

        internal void NextRoundCommand(string commandName)
        {
            switch (commandName)
            {
                case "NextRound":
                    NewRound();
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
