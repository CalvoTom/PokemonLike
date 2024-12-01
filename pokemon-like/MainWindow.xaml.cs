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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Ouvre la fenêtre LoginView
            var loginView = new LoginView();
            loginView.Show(); // Affiche la fenêtre LoginView
            this.Close(); // Ferme la fenêtre principale
        }
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // Ouvre la fenêtre LoginView
            var connectView = new SettingsView();
            connectView.Show(); // Affiche la fenêtre LoginView
        }
    }
}