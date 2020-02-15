using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_BlackJack.Presentation;

namespace WPF_BlackJack
{
    public partial class App : Application 
    {
        public GameViewModel GameViewModel { get; private set; }
        
        private void Application_Startup(object sender, StartupEventArgs e) 
        {
            GameViewModel gameViewModel = new GameViewModel();
            GameView gameView = new GameView(gameViewModel);
            gameView.DataContext = gameViewModel;
            gameView.Show();
        }
    }
}
