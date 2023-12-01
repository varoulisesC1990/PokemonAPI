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
        Water,

        [Description("fuego")]
        Fuego,
        [Description("planta")]
        Planta,
        [Description("agua")]
        Agua
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
        public Pokemon() { }

        public Pokemon(int id, string name, string type,int life, PokemonAttack[] attacks, string status) 
        {
        
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Attacks = attacks;
            this.Status = status;
            this.Life=life;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Life { get; set; }
        public PokemonAttack[] Attacks { get; set; }

        public string Status { get; set; }

    }
}
