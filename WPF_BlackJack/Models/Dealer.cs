﻿using System.Collections.Generic;
using WPF_BlackJack.Presentation;
namespace WPF_BlackJack.Models
{
    public class Dealer : ObservableObject
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

        private List<string> _card;

        public List<string> Card
        {
            get => _card;
            set 
            {
                _card = value;
                OnPropertyChanged(nameof(Card));
            }
        }

        public Dealer(string name)
        {
            _name = name;
            this.Card = new List<string>();
        }


    }
}
