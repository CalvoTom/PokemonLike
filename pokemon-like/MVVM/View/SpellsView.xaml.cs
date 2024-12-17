using System.Windows;
using PokemonLike.Models;

namespace PokemonLike.Views
{
    public partial class SpellView : Window
    {
        public SpellView(Spell spell)
        {
            InitializeComponent();

            SpellName.Text = spell.Name;
            SpellDamage.Text = spell.Damage.ToString(); ;
            SpellDescription.Text = spell.Description;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();  
        }
    }
}
