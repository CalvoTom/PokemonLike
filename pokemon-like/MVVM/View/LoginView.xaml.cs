using System.Windows;
using PokemonLike.Services;
using PokemonLike.Properties;
using pokemon_like;
using PokemonLike.Models;

namespace PokemonLike.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            var signupView = new SignupView();
            signupView.Show(); 
            this.Close(); 
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;
            var passwordHash = PasswordHasher.HashPassword(password);

            var user = DatabaseService.GetUser(username);

            if (user != null && user.PasswordHash == passwordHash)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Properties.Settings.Default.Username = username;
                Properties.Settings.Default.Save();

                var monsterManagementView = new MonsterManagementView();
                monsterManagementView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
