using System.Windows;

namespace WPF_BlackJack.Presentation
{
    /// <summary>
    /// Interaction logic for BlackJackRules.xaml
    /// </summary>
    public partial class BlackJackRules : Window
    {
        public BlackJackRules()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
