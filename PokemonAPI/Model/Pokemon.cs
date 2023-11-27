using System.Collections.Generic;
using System.ComponentModel;

namespace PokemonAPI.Model
{

    public enum PokemonType
    {
        [Description("fuego")]
        Fire,
        [Description("planta")]
        Grass,
        [Description("normal")]
        Normal,
        [Description("agua")]
        Water
    }


    public enum PokemonStatus
    {
        [Description("disponible")]
        Available,
        [Description("en-batalla")]
        InBattle,
        [Description("atacando")]
        Attacking,
        [Description("derrotado")]
        Defeated
    }


    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Life { get; set; }
        public PokemonAttack[] Attacks { get; set; }

        public string Status { get; set; }

    }
}
