using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using PokemonLike.Models;
using PokemonLike.Services;

namespace PokemonLike.Views
{
    public partial class SpellsManagementControl : UserControl
    {
        private List<Monster> monsters;
        private List<Spell> allSpells;

        public SpellsManagementControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            monsters = JsonService.LoadMonsters();
            allSpells = monsters.SelectMany(m => m.Spells).ToList();

            var allCategory = new Monster { Name = "All" };
            MonsterFilterComboBox.ItemsSource = new List<Monster> { allCategory }.Concat(monsters);

            SpellsListBox.ItemsSource = allSpells;
        }

        private void MonsterFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedMonster = MonsterFilterComboBox.SelectedItem as Monster;
            if (selectedMonster != null && selectedMonster.Name != "All")
            {
                SpellsListBox.ItemsSource = selectedMonster.Spells;
            }
            else
            {
                SpellsListBox.ItemsSource = allSpells;
            }
        }
        private void SpellListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SpellsListBox.SelectedItem is Spell selectedSpell)
            {
                var spellView = new SpellView(selectedSpell); 
                spellView.Show();
            }
        }

    }
}