using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_BlackJack.Models
{
    public class GameBoard
    {
        public enum GameState
        {
            PlayerTurn,
            DealerTurn,
            PlayerDeal,
            DealerDeal,
            PlayerHit,
            DealerHit,
            PlayerStand,
            DealerStand,
            PlayerSplit,
            PlayerInsurance,
            PlayWon,
            DealerWon
        }



    }
}
