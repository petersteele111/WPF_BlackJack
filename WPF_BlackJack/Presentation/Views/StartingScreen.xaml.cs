using System.Windows;

namespace WPF_BlackJack.Presentation
{
    /// <summary>
    /// Interaction logic for StartingScreen.xaml
    /// </summary>
    public partial class StartingScreen : Window
    {
        public StartingScreen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            RegisterPlayer registerPlayer = new RegisterPlayer(registerViewModel);
            registerPlayer.DataContext = registerViewModel;
            registerPlayer.Show();
            this.Close();
        }
    }
}
