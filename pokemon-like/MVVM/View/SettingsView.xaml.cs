using System.Windows;
using PokemonLike.Services;
using PokemonLike.Properties;

namespace PokemonLike.Views
{
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConnectionStringTextBox.Text;

            try
            {
                Console.WriteLine(connectionString);
                DatabaseService.Initialize(connectionString);
                MessageBox.Show("Connection successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Failed to connect to the database. Please check the connection string.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            var connectionString = ConnectionStringTextBox.Text;
            Properties.Settings.Default.ConnectionString = connectionString;
            Properties.Settings.Default.Save();
            DatabaseService.Initialize(connectionString);

            MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
