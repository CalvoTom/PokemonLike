using PokemonLike.Models;

namespace PokemonLike.ViewModels
{
    public class MonsterViewModel : BaseViewModel
    {
        public Monster Monster { get; set; }

        public MonsterViewModel(Monster monster)
        {
            Monster = monster;
        }

        public void Attack(Monster target, Spell spell)
        {
            target.Health -= spell.Damage;
            if (target.Health < 0) target.Health = 0;
        }
    }
}
