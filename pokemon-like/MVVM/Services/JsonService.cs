using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PokemonLike.Models;

namespace PokemonLike.Services
{
    public static class JsonService
    {
        private const string JsonFilePath = "../../../MVVM/Resources/pokemon.json";

        public static List<Monster> LoadMonsters()
        {
            if (!File.Exists(JsonFilePath))
            {
                System.Windows.MessageBox.Show($"File not found: {Path.GetFullPath(JsonFilePath)}", "Error");
                return new List<Monster>();
            }

            var json = File.ReadAllText(JsonFilePath);
            return JsonConvert.DeserializeObject<List<Monster>>(json);
        }


        public static void SaveMonsters(List<Monster> monsters)
        {
            var json = JsonConvert.SerializeObject(monsters, Formatting.Indented);
            File.WriteAllText(JsonFilePath, json);
        }
    }
}
