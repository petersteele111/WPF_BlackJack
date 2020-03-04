using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using WPF_BlackJack.Business;
using WPF_BlackJack.Models;

namespace WPF_BlackJack.Presentation
{
    public class RegisterViewModel : ObservableObject
    {

        #region Fields

        private GameBusiness _gameBusiness;
        private Player _currentPlayer;
        private string _messages;
        private List<Player> _player = new List<Player>();

        #endregion

        #region Properties

        public List<Player> Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        public string Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }


        #endregion

        #region Methods

        public void Register(string userName)
        {
            Regex rx = new Regex(@"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$");
            bool isValidUserName = rx.IsMatch(userName);
            if (!isValidUserName)
            {
                _messages = "Please enter a Valid Username! No Spaces or Special Characters";
            }
            else
            {
                _currentPlayer = new Player(userName.ToLower(), 1000);
                _player.Add(_currentPlayer);
                _gameBusiness = new GameBusiness();
                _gameBusiness.SavePlayer(_player, userName);
                InitializeGame();

            }
            OnPropertyChanged(nameof(Messages));
        }

        public void Login(string userName)
        {
            Regex rx = new Regex(@"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$");
            bool isValidUserName = rx.IsMatch(userName.ToLower());
            if (!isValidUserName)
            {
                _messages = "Please enter a Valid Username! No Spaces or Special Characters";
                OnPropertyChanged(nameof(Messages));
            }
            else
            {
                _messages = null;
                _gameBusiness = new GameBusiness();
                _currentPlayer = _gameBusiness.GetPlayer(userName.ToLower());
                if (_currentPlayer == null)
                {
                    _messages = "Could Not Find Player. Have You Registered?";
                }
                else
                {
                    InitializeGame();
                }
                OnPropertyChanged(nameof(Messages));
            }
        }

        private void InitializeGame()
        {
            GameViewModel gameViewModel = new GameViewModel(_gameBusiness);
            GameView gameView = new GameView(gameViewModel);
            gameView.DataContext = gameViewModel;
            gameView.Show();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }

        #endregion

        #region Constructor

        public RegisterViewModel()
        {

        }

        #endregion
       
    }
}
