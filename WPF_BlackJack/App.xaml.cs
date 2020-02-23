using System.Windows;
using WPF_BlackJack.Presentation;

namespace WPF_BlackJack
{
    public partial class App : Application 
    {
        public GameViewModel GameViewModel { get; private set; }
        
        private void Application_Startup(object sender, StartupEventArgs e) 
        {
            StartingScreen startingScreen = new StartingScreen();
            startingScreen.Show();
        }
    }
}
