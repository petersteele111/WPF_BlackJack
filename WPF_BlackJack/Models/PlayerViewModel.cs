using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_BlackJack.Business;

namespace WPF_BlackJack.Models
{
    class PlayerViewModel : ObservableObject
    {
        private Player _player;

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        public PlayerViewModel()
        {
            _player = new Player("Peter", 10000, 0,0);
        }
    }
}
