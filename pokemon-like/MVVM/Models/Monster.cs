
namespace PokemonLike.Models
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public string ImageUrl { get; set; }
        public List<Spell> Spells { get; set; }
    }
}
