namespace PokemonAPI.Model
{
    public class PokemonAttack
    {
        public PokemonAttack() { }

        public PokemonAttack(PokemonType type,int power) 
        {
            this.type = type;
            this.power = power;
        }

        public PokemonType type { get; set; }
        public int power { get; set; }
    }
}
