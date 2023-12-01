namespace PokemonAPI.Model
{
    public class PokemonAttack
    {
        public PokemonAttack() { }

        public PokemonAttack(string type,int power) 
        {
            this.Type = type;
            this.Power = power;
        }

        public string Type { get; set; }
        public int Power { get; set; }
    }
}
