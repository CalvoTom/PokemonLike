using System.Windows;
using System.Windows.Controls;
using pokemon_like;
using PokemonLike.Models;
using PokemonLike.Services;

namespace PokemonLike.Views
{
    public partial class AccountManagementControl : UserControl
    {
        private Login currentUser;

        public AccountManagementControl()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            var username = Properties.Settings.Default.Username;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("No user logged in!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            currentUser = DatabaseService.GetUser(username);

            if (currentUser != null)
            {
                UsernameTextBlock.Text = $"Username: {currentUser.Username}";
            }
            else
            {
                MessageBox.Show("User not found in database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Username = string.Empty;
            Properties.Settings.Default.Save();
            MessageBox.Show("You have been logged out.", "Log Out", MessageBoxButton.OK, MessageBoxImage.Information);

            var loginPage = new LoginView();
            loginPage.Show();
            var parentWindow = Window.GetWindow(this);
            parentWindow.Close();
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the account {currentUser.Username}?",
                    "Delete Account", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DatabaseService.DeleteUser(currentUser.Username);
                    Properties.Settings.Default.Username = string.Empty;
                    Properties.Settings.Default.Save();

                    MessageBox.Show("Your account has been deleted.", "Account Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    var loginPage = new LoginView();
                    loginPage.Show();
                    var parentWindow = Window.GetWindow(this);
                    parentWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("No user logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
