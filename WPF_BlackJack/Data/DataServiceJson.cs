using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_BlackJack.Models;

namespace WPF_BlackJack.Data
{
    public class DataServiceJson : IDataService
    {
        private string _dataFilePath;

        public List<Player> ReadAll()
        {
            List<Player> players;
            {
                try
                {
                    using (StreamReader sr = new StreamReader(_dataFilePath))
                    {
                        string jsonString = sr.ReadToEnd();
                        players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return players;
            }
        }

        public void WriteAll(List<Player> players)
        {
            string jsonString = JsonConvert.SerializeObject(players, Formatting.Indented);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataServiceJson() 
        {
            _dataFilePath = @"Data\Data.json";
        }
    }
}
