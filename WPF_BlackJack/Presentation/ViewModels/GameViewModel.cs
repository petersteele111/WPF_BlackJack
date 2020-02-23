using System.Collections.Generic;
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
        private List<Player> _playerData = new List<Player>();
        #endregion

        #region Properties
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

        private bool _canBet;

        public bool CanBet
        {
            get => _canBet;
            set
            {
                _canBet = value;
                OnPropertyChanged(nameof(CanBet));
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        private bool _isDealVisible;

        public bool IsDealVisible
        {
            get => _isDealVisible;
            set
            {
                _isDealVisible = value;
                OnPropertyChanged(nameof(IsDealVisible));
            }
        }

        #endregion

        #region Start Game
        public GameViewModel(GameBusiness gameBusiness)
        {
            _gameBusiness = gameBusiness;
            _gameBoard = new GameBoard();

            InitializeGame();
        }

        private void InitializeGame()
        {
            _currentPlayer = _gameBusiness.GetPlayer(_gameBusiness.UserName);

            _player = _currentPlayer;
            _dealer = new Dealer("Mark");

            _gameBoard.currentGameState = GameBoard.GameState.PlayerBet;
            _canBet = true;
            _isDealVisible = true;
            _isVisible = false;
            _messages = "Player Bet";
            OnPropertyChanged(nameof(IsDealVisible));
            OnPropertyChanged(nameof(CanBet));
            OnPropertyChanged(nameof(IsVisible));
        }

        public void Deal()
        {
            _canBet = false;
            OnPropertyChanged(nameof(CanBet));

            _canClick = _gameBoard.Clickable();
            OnPropertyChanged(nameof(CanClick));

            var value = _gameBoard.DealInitalCards();
            _dealer.Card.Add("../Images/Cards/b1fv.bmp");
            _dealer.HiddenCard = ("../Images/Cards/" + value.dCards[0] + ".bmp");
            _dealer.Card.Add("../Images/Cards/" + value.dCards[1] + ".bmp");
            _player.Card.Add("../Images/Cards/" + value.pCards[0] + ".bmp");
            _player.Card.Add("../Images/Cards/" + value.pCards[1] + ".bmp");

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
            IsDealVisible = false;
            OnPropertyChanged(nameof(IsDealVisible));
            OnPropertyChanged(nameof(Dealer));
            OnPropertyChanged(nameof(Player));

            if (_player.CardTotal == 21)
            {
                PlayerBlackJack();
            }
        }

        public void SaveData()
        {
            _playerData.Add(_player);
            _gameBusiness.SavePlayer(_playerData, _player.Name);
            _playerData.Clear();
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
            _player.CardTotal = null;
            ResetBoard();
            OnPropertyChanged(nameof(Player));
            OnPropertyChanged(nameof(Dealer));
        }

        #endregion

        #region Game Logic

        #region BettingLogic
        public void CheckBankRoll(int bet, int bankRoll)
        {
            if (bankRoll > 0)
            {
                _player.BetAmount = bet;
                _player.TotalBet += bet;
                _player.BankRoll -= bet;
            }
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
                    CheckBankRoll(BET1, _player.BankRoll);
                    break;
                case "PlayerBet5":
                    CheckBankRoll(BET5, _player.BankRoll);
                    break;
                case "PlayerBet10":
                    CheckBankRoll(BET10, _player.BankRoll);
                    break;
                case "PlayerBet25":
                    CheckBankRoll(BET25, _player.BankRoll);
                    break;
                case "PlayerBet50":
                    CheckBankRoll(BET50, _player.BankRoll);
                    break;
                case "PlayerBet100":
                    CheckBankRoll(BET100, _player.BankRoll);
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
                    if (_player.TotalBet - _player.BetAmount >= 0)
                    {
                        _player.TotalBet -= _player.BetAmount;
                        _player.BankRoll += _player.BetAmount;
                    }
                    break;
                case "BetIncrease":
                    if (_player.BankRoll > 0)
                    {
                        _player.TotalBet += _player.BetAmount;
                        _player.BankRoll -= _player.BetAmount;
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ActionButtonLogic

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
                _player.Card.Add("../Images/Cards/" + card + ".bmp");
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

        public void PlayerStand()
        {
            if (_player.TotalBet >= 1)
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
                _dealer.Card.Add("../Images/Cards/" + dCard + ".bmp");
                CheckDealerWinCondition();
            }
            CheckGameWinCondition();
            OnPropertyChanged(nameof(Dealer));
            OnPropertyChanged(nameof(Player));

        }

        #endregion

        #region WinConditions

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
                PlayerBust();
            }

            if (_player.CardTotal == 21)
            {
                PlayerBlackJack();
            }
        }

        public void PlayerBlackJack()
        {
            if (_player.CardTotal == 21)
            {
                _gameBoard.currentGameState = GameBoard.GameState.PlayerBlackJack;
                _messages = "Player BlackJack";
                _player.TotalWinnings = _player.TotalBet * 2;
                _player.BankRoll = _player.TotalWinnings;

                _isVisible = _gameBoard.Visible();

                _canClick = _gameBoard.Clickable();
                SaveData();

                OnPropertyChanged(nameof(Messages));
                OnPropertyChanged(nameof(IsVisible));
                OnPropertyChanged(nameof(CanClick));
                OnPropertyChanged(nameof(Player));
            }
        }

        public void PlayerBust()
        {
            _gameBoard.currentGameState = GameBoard.GameState.PlayerBust;
            _messages = "Player Bust";
            _canClick = _gameBoard.Clickable();
            _isVisible = _gameBoard.Visible();
            SaveData();

            OnPropertyChanged(nameof(IsVisible));
            OnPropertyChanged(nameof(Messages));
            OnPropertyChanged(nameof(CanClick));
        }

        public void DealerBust()
        {
            _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
            _messages = "Dealer Bust";
            _player.TotalWinnings = _player.TotalBet * 2;
            _player.BankRoll += _player.TotalWinnings;
            SaveData();
        }

        public void DealerBlackJack()
        {
            _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
            _messages = "Dealer BlackJack";
            _player.TotalWinnings = 0;
            SaveData();
        }

        public void DealerWin()
        {
            _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
            _messages = "Dealer Won";
            _player.TotalWinnings = 0;
            SaveData();
        }

        public void PlayerWin()
        {
            _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
            _messages = "Player Won";
            _player.TotalWinnings = _player.TotalBet * 2;
            _player.BankRoll += _player.TotalWinnings;
            SaveData();
        }

        public void DrawGame()
        {
            _gameBoard.currentGameState = GameBoard.GameState.RoundOver;
            _messages = "Draw";
            _player.TotalWinnings = _player.TotalBet;
            _player.BankRoll += _player.TotalWinnings;
            SaveData();
        }


        public void CheckGameWinCondition()
        {
            if (_dealer.CardTotal > 21)
            {
                DealerBust();
            }
            else if (_dealer.CardTotal == 21)
            {
                DealerBlackJack();
            }
            else if (_player.CardTotal > _dealer.CardTotal)
            {
                PlayerWin();
            }
            else if (_player.CardTotal < _dealer.CardTotal)
            {
                DealerWin();
            }
            else if (_player.CardTotal == _dealer.CardTotal)
            {
                DrawGame();
            }
            else
            {
                _messages = "Something went wrong! You should never see this message";
                OnPropertyChanged(nameof(Messages));
            }

            _isVisible = _gameBoard.Visible();

            OnPropertyChanged(nameof(Dealer));
            OnPropertyChanged(nameof(Player));
            OnPropertyChanged(nameof(Messages));
            OnPropertyChanged(nameof(IsVisible));
        }

        #endregion

        #region ButtonCommands
        internal void GameCommand(string commandName)
        {
            switch (commandName)
            {
                case "NewGame":
                    NewGame();
                    break;
                case "Quit":
                    break;
                default:
                    break;
            }
        }

        internal void ActionButtonCommand(string commandName)
        {
            switch (commandName)
            {
                case "NextRound":
                    _gameBoard.currentGameState = GameBoard.GameState.PlayerBet;
                    _canClick = _gameBoard.Clickable();
                    if (_player.BankRoll == 0)
                    {
                        GameOver();
                    }
                    else
                    {
                        NewRound();
                    }
                    break;
                case "Deal":
                    if (_player.TotalBet < 1)
                    {
                        _messages = "You Must Place A Bet Before Dealing!";
                        OnPropertyChanged(nameof(Messages));
                    }
                    else
                    {
                        Deal();
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Screens

        public void GameOver()
        {
            GameOver gameOver = new GameOver();
            gameOver.Show();
        }

        #endregion

        #endregion
    }
}
