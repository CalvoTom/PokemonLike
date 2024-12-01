using System.Collections.ObjectModel;
using PokemonLike.Models;
using PokemonLike.Services;

namespace PokemonLike.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Monster> Monsters { get; set; }
        public Monster SelectedMonster { get; set; }

        public MainViewModel()
        {
            LoadMonsters();
        }

        private void LoadMonsters()
        {
            // Charger les monstres à partir du fichier JSON
            Monsters = new ObservableCollection<Monster>(JsonService.LoadMonsters());
        }
    }
}
