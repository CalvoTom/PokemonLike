using System.Windows;
using PokemonLike.Services;
using PokemonLike.Properties;
using PokemonLike.Models;
using pokemon_like;

namespace PokemonLike.Views
{
    public partial class SignupView : Window
    {
        public SignupView()
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
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;
            var passwordHash = PasswordHasher.HashPassword(password);

            var existingUser = DatabaseService.GetUser(username);
            if (existingUser != null)
            {
                MessageBox.Show("Username is already taken. Please choose a different one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DatabaseService.AddUser(username, passwordHash);
            MessageBox.Show("Account created successfully! Logging in...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            Properties.Settings.Default.Username = username;
            Properties.Settings.Default.Save();

            var monsterManagementView = new MonsterManagementView();
            monsterManagementView.Show();
            this.Close();
        }
    }
}