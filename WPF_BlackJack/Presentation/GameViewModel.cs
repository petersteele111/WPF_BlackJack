using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_BlackJack.Models;

namespace WPF_BlackJack.Presentation
{
    public class GameViewModel : ObservableObject
    {
        private Dealer _dealer;
        private Player _player;


        public Dealer Dealer
        {
            get => _dealer;
            set
            {
                _dealer = value;
                OnPropertyChanged(nameof(Dealer));
            }
        }

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged(nameof(Player));
            }
        }
        public GameViewModel() 
        {
            _dealer = new Dealer("Mark");
            _player = new Player("Peter", 100000, 0, 0);
        }
    }

}
