using System.Collections.Generic;
using System.ComponentModel;

namespace PokemonAPI.Model
{

    public enum PokemonType
    {
        [Description("normal")]
        normal,
        [Description("fuego")]
        fuego,
        [Description("planta")]
        planta,
        [Description("agua")]
        agua
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

        public Pokemon(string name, PokemonType type,double life, PokemonAttack[] attacks) 
        {
            this.name = name;
            this.type = type;
            this.life=life;
            this.attacks = attacks;
        }


        // public int Id { get; set; }
        public string name { get; set; }
        public PokemonType type { get; set; }
        public double life { get; set; }
        public PokemonAttack[] attacks { get; set; }

    }
}
