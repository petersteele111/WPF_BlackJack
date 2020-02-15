using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_BlackJack.Models;
namespace WPF_BlackJack.Data
{
    class DataService : IDataService
    {
        public List<Player> ReadAll() 
        {
            return new List<Player>()
            {
                new Player(name:"Peter", bankroll:100000, totalbet:0, totalwin:0)
            };
        }

        public void WriteAll(List<Player> players) 
        {

        }
    }
}
