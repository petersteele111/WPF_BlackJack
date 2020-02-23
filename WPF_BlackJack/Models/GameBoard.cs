using System;
using System.Collections.Generic;
using WPF_BlackJack.Presentation;

namespace WPF_BlackJack.Models
{
    public class GameBoard : ObservableObject
    {
        #region Enums
        public enum GameState
        {
            PlayerBet,
            PlayerHit,
            PlayerStand,
            DealerStand,
            DealerDraw,
            PlayerBust,
            DealerBust,
            PlayerBlackJack,
            DealerBlackJack,
            Draw,
            PlayerWon,
            DealerWon,
            RoundOver,
            NewRound
        };

        #endregion

        #region CardLogic

        List<string> cards = new List<string>()
        {
             "c01", "c02", "c03", "c04", "c05", "c06", "c07", "c08", "c09", "c10", "c11", "c12", "d13", "d01", "d02", "d03", "d04", "d05", "d06", "d07", "d08", "d09", "d10", "d11", "d12", "d13", "h01", "h02", "h03", "h04", "h05", "h06", "h07", "h08", "h09", "h10", "h11", "h12", "h13", "s01", "s02", "s03", "s04", "s05", "s06", "s07", "s08", "s09", "s10", "s11", "s12", "s13"
        };

        List<string> selectedCards = new List<string>();
        private const int INITIALDEAL = 2;


        public (List<string> dCards, List<string> pCards) DealInitalCards()
        {
            List<string> dealerCards = new List<string>();
            List<string> playerCards = new List<string>();

            Random random = new Random();

            reshuffleCards();

            for (int i = 0; i < INITIALDEAL; i++)
            {
                int r1 = random.Next(1, cards.Count);
                dealerCards.Add(cards[r1]);
                selectedCards.Add(dealerCards[i]);
                cards.Remove(dealerCards[i]);
            }

            for (int i = 0; i < INITIALDEAL; i++)
            {
                int r1 = random.Next(1, cards.Count);
                playerCards.Add(cards[r1]);
                selectedCards.Add(playerCards[i]);
                cards.Remove(playerCards[i]);
            }

            return (playerCards, dealerCards);
        }

        public string DealCard()
        {
            reshuffleCards();
            Random random = new Random();
            int r1 = random.Next(1, cards.Count);
            string dealtCard = cards[r1];
            selectedCards.Add(cards[r1]);
            cards.Remove(cards[r1]);
            return dealtCard;
        }

        private void reshuffleCards()
        {
            if (cards.Count <= 4)
            {
                cards.Clear();
                foreach (var card in selectedCards)
                {
                    cards.Add(card);
                }
                selectedCards.Clear();
            }
        }

        #endregion

        #region GameStateLogic

        public GameState currentGameState { get; set; }
        public bool Clickable()
        {
            if (currentGameState == GameState.PlayerStand || currentGameState == GameState.PlayerBust || currentGameState == GameState.PlayerBlackJack)
            {
                return false;
            }
            else if (currentGameState == GameState.PlayerBet)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public bool Visible()
        {
            if (currentGameState == GameState.RoundOver || currentGameState == GameState.PlayerBlackJack || currentGameState == GameState.PlayerBust)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    #endregion
    }
}
