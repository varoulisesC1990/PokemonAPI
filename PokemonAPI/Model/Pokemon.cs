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
        DISPONIBLE,
        [Description("en-batalla")]
        EN_BATALLA,
        [Description("atacando")]
        ATACANDO,
        [Description("derrotado")]
        DERROTADO,
        [Description("ganador")]
        GANADOR
    }


    public class Pokemon
    {
        public Pokemon() { }

        public Pokemon(string name, string type,int life, PokemonAttack[] attacks, string status) 
        {
            this.Name = name;
            this.Type = type;
            this.Attacks = attacks;
            this.Status = status;
            this.Life=life;
        }

       // public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Life { get; set; }
        public PokemonAttack[] Attacks { get; set; }

        public string Status { get; set; }

    }
}
