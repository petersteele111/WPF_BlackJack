using System.Windows;


namespace WPF_BlackJack.Presentation
{
    /// <summary>
    /// Interaction logic for RegisterPlayer.xaml
    /// </summary>
    public partial class RegisterPlayer : Window
    {
        RegisterViewModel _registerViewModel;
        public RegisterPlayer(RegisterViewModel registerViewModel)
        {
            _registerViewModel = registerViewModel;
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _registerViewModel.Register(UserName.Text);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) 
        {
            _registerViewModel.Login(UserName.Text);
        }
    }
}
