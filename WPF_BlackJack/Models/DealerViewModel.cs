using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_BlackJack.Business;

namespace WPF_BlackJack.Models
{
    class DealerViewModel : ObservableObject
    {
        private Dealer _dealer;


        public Dealer Dealer
        {
            get => _dealer;
            set
            {
                _dealer = value;
                OnPropertyChanged(nameof(Dealer));
            }
        }

        public DealerViewModel()
        {
            _dealer = new Dealer("Mike");
        }

    }
}
