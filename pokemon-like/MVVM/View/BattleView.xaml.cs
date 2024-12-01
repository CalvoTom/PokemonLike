using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PokemonLike.Models;
using PokemonLike.Services;

namespace PokemonLike.Views
{
    public partial class BattleView : Window
    {
        private Monster playerMonster;
        private Monster enemyMonster;
        private int playerPoints = 0;  

        public BattleView(Monster selectedMonster)
        {
            InitializeComponent();
            playerMonster = selectedMonster;
            InitializeBattle();
        }

        private void InitializeBattle()
        {
            PlayerMonsterName.Text = playerMonster.Name;
            PlayerHpBar.Maximum = playerMonster.Health;
            PlayerHpBar.Value = playerMonster.Health;
            SpellListBox.ItemsSource = playerMonster.Spells.Select(spell => spell.Name).ToList();

            try
            {
                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new System.Uri(playerMonster.ImageUrl, System.UriKind.Absolute);
                bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                PlayerMonsterImage.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Image Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            NewEnemyButton.IsEnabled = false;
            GenerateEnemyMonster();
            UpdatePointsCounter();
        }


        private void GenerateEnemyMonster()
        {
            var allMonsters = JsonService.LoadMonsters();
            var random = new Random();
            enemyMonster = allMonsters[random.Next(allMonsters.Count)];
            enemyMonster.Health = (int)(enemyMonster.Health * 1.1); 

            EnemyMonsterName.Text = enemyMonster.Name;
            EnemyHpBar.Maximum = enemyMonster.Health;
            EnemyHpBar.Value = enemyMonster.Health;

            try
            {
                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new System.Uri(enemyMonster.ImageUrl, System.UriKind.Absolute);
                bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                EnemyMonsterImage.Source = bitmap; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Image Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SpellListBox.IsEnabled = true;
            UseSpellButton.IsEnabled = true;
        }

        private async void UseSpell_Click(object sender, RoutedEventArgs e)
        {
            SpellListBox.IsEnabled = false;
            UseSpellButton.IsEnabled = false;

            if (SpellListBox.SelectedItem is string selectedSpellString)
            {
                var selectedSpell = playerMonster.Spells.FirstOrDefault(spell => spell.Name == selectedSpellString);

                if (selectedSpell != null)
                {
                    enemyMonster.Health -= selectedSpell.Damage;
                    if (enemyMonster.Health < 0) enemyMonster.Health = 0;
                    EnemyHpBar.Value = enemyMonster.Health;

                    var playerPopup = new SpellPopupView($"-{selectedSpell.Damage} HP", $"{playerMonster.Name} uses {selectedSpell.Name}");
                    playerPopup.Show();
                    await playerPopup.ShowAndCloseAsync(this);

                    if (enemyMonster.Health == 0)
                    {
                        MessageBox.Show("Enemy defeated!", "Victory", MessageBoxButton.OK, MessageBoxImage.Information);
                        playerPoints++;  
                        UpdatePointsCounter();

                        SpellListBox.IsEnabled = true;
                        UseSpellButton.IsEnabled = true;
                        NewEnemyButton.IsEnabled = true;

                        return;  
                    }

                    await Task.Delay(1500); 

                    var random = new Random();
                    var enemySpell = enemyMonster.Spells[random.Next(enemyMonster.Spells.Count)];
                    playerMonster.Health -= enemySpell.Damage;
                    if (playerMonster.Health < 0) playerMonster.Health = 0;
                    PlayerHpBar.Value = playerMonster.Health;

                    var enemyPopup = new SpellPopupView($"-{enemySpell.Damage} HP", $"{enemyMonster.Name} uses {enemySpell.Name}");
                    enemyPopup.Show();
                    await enemyPopup.ShowAndCloseAsync(this);

                    if (playerMonster.Health == 0)
                    {
                        MessageBox.Show("You have been defeated!", "Defeat", MessageBoxButton.OK, MessageBoxImage.Error);

                        var monsterManagementView = new MonsterManagementView();
                        monsterManagementView.Show();
                        this.Close();
                    }
                }
            }

            SpellListBox.IsEnabled = true;
            UseSpellButton.IsEnabled = true;
        }


        private void NewEnemy_Click(object sender, RoutedEventArgs e)
        {
            GenerateEnemyMonster();
            NewEnemyButton.IsEnabled = false;
        }
        private void UpdatePointsCounter()
        {
            PointsCounter.Text = $"Points: {playerPoints}";
        }
    }
}
