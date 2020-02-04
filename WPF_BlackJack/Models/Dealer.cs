namespace WPF_BlackJack.Business
{
    class Dealer : ObservableObject
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

        private int _cardTotal;

        public int CardTotal
        {
            get => _cardTotal;
            set
            {
                _cardTotal = value;
                OnPropertyChanged(nameof(CardTotal));
            }
        }

        public Dealer(string name)
        {
            _name = name;
        }


    }
}
