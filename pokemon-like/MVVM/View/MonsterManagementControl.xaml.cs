using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PokemonLike.Models;
using PokemonLike.Services;

namespace PokemonLike.Views
{
    public partial class MonsterManagementControl : UserControl
    {
        private Monster selectedMonster;

        public MonsterManagementControl()
        {
            InitializeComponent();
            LoadMonsters();
        }

        private void LoadMonsters()
        {
            var monsters = JsonService.LoadMonsters();
            MonsterListBox.ItemsSource = monsters;

            if (monsters.Any())
            {
                selectedMonster = monsters.First();
                MonsterListBox.SelectedItem = selectedMonster;

                MonsterName.Text = selectedMonster.Name;
                MonsterHealth.Text = selectedMonster.Health.ToString();

                try
                {
                    var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(selectedMonster.ImageUrl, UriKind.Absolute);
                    bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    MonsterImage.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Image Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                SpellListBox.ItemsSource = selectedMonster.Spells;
            }
        }

        private void MonsterListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedMonster = MonsterListBox.SelectedItem as Monster;
            if (selectedMonster != null)
            {
                MonsterName.Text = selectedMonster.Name;
                MonsterHealth.Text = selectedMonster.Health.ToString();

                try
                {
                    var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new System.Uri(selectedMonster.ImageUrl, System.UriKind.Absolute);
                    bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    MonsterImage.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Image Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                SpellListBox.ItemsSource = selectedMonster.Spells;
            }
        }


        private void ChooseMonster_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMonster != null)
            {
                MessageBox.Show($"You have chosen {selectedMonster.Name}!", "Monster Selected", MessageBoxButton.OK, MessageBoxImage.Information);

                var battleView = new BattleView(selectedMonster);
                battleView.Show();
                var parentWindow = Window.GetWindow(this);
                parentWindow.Close();
            }
            else
            {
                MessageBox.Show("Please select a monster first.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SpellListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SpellListBox.SelectedItem is Spell selectedSpell)
            {
                var spellView = new SpellView(selectedSpell); 
                spellView.Show();
            }
        }

    }
}
