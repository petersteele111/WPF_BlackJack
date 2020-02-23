using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_BlackJack.Data;
using WPF_BlackJack.Models;

namespace WPF_BlackJack.Business
{
    public class GameBusiness
    {
        public enum GameStatus 
        {
            Quit,
            QuitSave
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set 
            {
                _userName = value; 
            }
        }


        List<Player> _allPlayers;

        Player _player;

        IDataService _dataService;

        public GameBusiness()
        {
            InitializeDataService();
            //InitializeGame();
        }

        private void InitializeDataService()
        {
            _dataService = new DataServiceJson();
        }

        public List<Player> GetAllPlayers() 
        {
            return _dataService.ReadAll();
        }

        public void SavePlayer(List<Player> player, string userName) 
        {
            _dataService.WriteAll(player);
            _userName = userName;
        }

        public Player GetPlayer(string userName)
        {
            _allPlayers = GetAllPlayers();
            _player = _allPlayers.FirstOrDefault(p => p.Name == userName);
            _userName = userName;
            return _player;
        }


    }

}
