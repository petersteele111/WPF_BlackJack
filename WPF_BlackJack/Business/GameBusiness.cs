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
        
        List<Player> _allPlayers;

        Player _player;

        IDataService _dataService;

        public GameBusiness()
        {
            InitializeDataService();
            InitializeGame();
        }

        private void InitializeDataService()
        {
            _dataService = new DataServiceJson();
        }

        private void InitializeGame() 
        {
            _allPlayers = _dataService.ReadAll();

            _player = _allPlayers.FirstOrDefault(p => p.Name == "Peter");
        }

        public List<Player> GetAllPlayers() 
        {
            return _dataService.ReadAll();
        }

        public void SaveAllPlayers() 
        {
            _dataService.WriteAll(_allPlayers);
        }

        public Player GetCurrentPlayers() 
        {
            _player = _allPlayers.FirstOrDefault(p => p.Name == "Peter");
            Player currentPlayers = _player;


            return currentPlayers;
        }
    }

}
