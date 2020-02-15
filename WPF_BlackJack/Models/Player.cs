using WPF_BlackJack.Presentation;
namespace WPF_BlackJack.Models
{
    public class Player : ObservableObject
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _bankRoll;

        public int BankRoll
        {
            get => _bankRoll;
            set
            {
                _bankRoll = value;
                OnPropertyChanged(nameof(BankRoll));
            }
        }

        private int _totalBet;
        
        public int TotalBet
        {
            get => _totalBet;
            set
            {
                _totalBet = value;
                OnPropertyChanged(nameof(TotalBet));
            }
        }

        private int _totalWinnings;

        public int TotalWinnings
        {
            get => _totalWinnings;
            set
            {
                _totalWinnings = value;
                OnPropertyChanged(nameof(TotalWinnings));
            }
        }

        private int? _cardTotal;

        public int? CardTotal
        {
            get => _cardTotal;
            set
            {
                _cardTotal = value;
                OnPropertyChanged(nameof(CardTotal));
            }
        }





        public Player(string name, int bankroll, int totalbet, int totalwin)
        {
            _name = name;
            _bankRoll = bankroll;
            _totalBet = totalbet;
            _totalWinnings = totalwin;
        }
    }
}
