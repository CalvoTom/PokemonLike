using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokemonLike.Views;

namespace pokemon_like
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show(); 
            this.Close(); 
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            var signupView = new SignupView();
            signupView.Show(); 
            this.Close(); 
        }
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            var connectView = new SettingsView();
            connectView.Show(); 
        }
    }
}