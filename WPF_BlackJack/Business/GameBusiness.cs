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

        Player _playerOne;
        Player _playerTwo;

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

            _playerOne = _allPlayers.FirstOrDefault(p => p.Name == "Peter");
            _playerTwo = _allPlayers.FirstOrDefault(p => p.Name == "Arya");
        }

        public List<Player> GetAllPlayers() 
        {
            return _dataService.ReadAll();
        }

        public void SaveAllPlayers() 
        {
            _dataService.WriteAll(_allPlayers);
        }

        public (Player playterOne, Player playerTwo) GetCurrentPlayers() 
        {
            _playerOne = _allPlayers.FirstOrDefault(p => p.Name == "Peter");
            _playerTwo = _allPlayers.FirstOrDefault(p => p.Name == "Arya");
            (Player playerOne, Player playerTwo) currentPlayers = (_playerOne, _playerTwo);

            return currentPlayers;
        }
    }

}
