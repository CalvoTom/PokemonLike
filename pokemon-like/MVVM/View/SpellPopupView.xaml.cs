using System;
using System.Threading.Tasks;
using System.Windows;

namespace PokemonLike.Views
{
    public partial class SpellPopupView : Window
    {
        public SpellPopupView(string damage, string spellInfo)
        {
            InitializeComponent();
            DamageText.Text = damage;
            SpellText.Text = spellInfo;
        }

        public async Task ShowAndCloseAsync(Window parent, int duration = 1000)
        {
            CenterPopup(parent); 
            await Task.Delay(duration);
            this.Close();
        }

        private void CenterPopup(Window parent)
        {
            if (parent != null)
            {
                this.Left = parent.Left + (parent.Width - this.Width) / 2;
                this.Top = parent.Top + (parent.Height - this.Height) / 2;
            }
        }
    }
}
