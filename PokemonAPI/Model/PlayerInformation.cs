namespace PokemonAPI.Model
{
    public class PlayerInformation
    {
        public PlayerInformation() { }

        public PlayerInformation(string playerName, string state, Pokemon pokemon)
        {
            this.PlayerName = playerName;
            this.State = state;
            this.Pokemon = pokemon;
        }


        public string PlayerName { get; set; }

        public string State { get; set; }

        public Pokemon Pokemon { get; set; }
    }
}
