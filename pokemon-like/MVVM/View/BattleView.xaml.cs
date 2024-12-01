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
        private int playerPoints = 0;  // Compteur de points

        public BattleView(Monster selectedMonster)
        {
            InitializeComponent();
            playerMonster = selectedMonster;
            InitializeBattle();
        }

        private void InitializeBattle()
        {
            // Initialisation du monstre du joueur
            PlayerMonsterName.Text = playerMonster.Name;
            PlayerHpBar.Maximum = playerMonster.Health;
            PlayerHpBar.Value = playerMonster.Health;

            // Mettre les noms des sorts dans la SpellListBox
            SpellListBox.ItemsSource = playerMonster.Spells.Select(spell => spell.Name).ToList();

            // Charger et afficher l'image du Pok�mon du joueur
            try
            {
                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new System.Uri(playerMonster.ImageUrl, System.UriKind.Absolute);
                bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                PlayerMonsterImage.Source = bitmap; // Afficher l'image du Pok�mon
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Image Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // D�sactiver le bouton "New Enemy" jusqu'� ce que l'ennemi soit vaincu
            NewEnemyButton.IsEnabled = false;


            // G�n�rer un monstre ennemi
            GenerateEnemyMonster();

            // Mettre � jour le compteur de points
            UpdatePointsCounter();
        }


        private void GenerateEnemyMonster()
        {
            var allMonsters = JsonService.LoadMonsters();
            var random = new Random();
            enemyMonster = allMonsters[random.Next(allMonsters.Count)];
            enemyMonster.Health = (int)(enemyMonster.Health * 1.1); // Ennemi l�g�rement plus fort

            // R�initialiser les informations de l'ennemi
            EnemyMonsterName.Text = enemyMonster.Name;
            EnemyHpBar.Maximum = enemyMonster.Health;
            EnemyHpBar.Value = enemyMonster.Health;

            // Charger et afficher l'image du monstre ennemi
            try
            {
                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new System.Uri(enemyMonster.ImageUrl, System.UriKind.Absolute);
                bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                EnemyMonsterImage.Source = bitmap; // Afficher l'image du monstre
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Image Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // R�activer les boutons apr�s la r�initialisation de l'ennemi
            SpellListBox.IsEnabled = true;
            UseSpellButton.IsEnabled = true;
        }



        private async void UseSpell_Click(object sender, RoutedEventArgs e)
        {
            // D�sactiver les options pendant l'attaque du joueur
            SpellListBox.IsEnabled = false;
            UseSpellButton.IsEnabled = false;

            if (SpellListBox.SelectedItem is string selectedSpellString)
            {
                // Trouver le sort correspondant � partir du nom
                var selectedSpell = playerMonster.Spells.FirstOrDefault(spell => spell.Name == selectedSpellString);

                if (selectedSpell != null)
                {
                    // Player attaque l'ennemi
                    enemyMonster.Health -= selectedSpell.Damage;
                    if (enemyMonster.Health < 0) enemyMonster.Health = 0;
                    EnemyHpBar.Value = enemyMonster.Health;

                    // V�rifier si l'ennemi est vaincu
                    if (enemyMonster.Health == 0)
                    {
                        MessageBox.Show("Enemy defeated!", "Victory", MessageBoxButton.OK, MessageBoxImage.Information);
                        playerPoints++;  // Incr�menter les points
                        UpdatePointsCounter();

                        // R�activer les boutons imm�diatement apr�s la victoire
                        SpellListBox.IsEnabled = true;
                        UseSpellButton.IsEnabled = true;
                        NewEnemyButton.IsEnabled = true;

                        return;  // Sortir de la m�thode, aucune attaque de l'ennemi
                    }

                    // Attendre que l'ennemi r�agisse
                    await Task.Delay(1500); // Temps de latence

                    // Ennemi contre-attaque
                    var random = new Random();
                    var enemySpell = enemyMonster.Spells[random.Next(enemyMonster.Spells.Count)];
                    playerMonster.Health -= enemySpell.Damage;
                    if (playerMonster.Health < 0) playerMonster.Health = 0;
                    PlayerHpBar.Value = playerMonster.Health;  // Mise � jour de la barre de vie du joueur

                    // V�rifier si le joueur est vaincu
                    if (playerMonster.Health == 0)
                    {
                        MessageBox.Show("You have been defeated!", "Defeat", MessageBoxButton.OK, MessageBoxImage.Error);

                        var monsterManagementView = new MonsterManagementView();
                        monsterManagementView.Show();
                        this.Close();
                    }
                }
            }

            // R�activer les options apr�s la contre-attaque de l'ennemi
            SpellListBox.IsEnabled = true;
            UseSpellButton.IsEnabled = true;
        }



        private void NewEnemy_Click(object sender, RoutedEventArgs e)
        {
            GenerateEnemyMonster();
            NewEnemyButton.IsEnabled = false;
        }

        // Mise � jour du compteur de points
        private void UpdatePointsCounter()
        {
            PointsCounter.Text = $"Points: {playerPoints}";
        }
    }
}
