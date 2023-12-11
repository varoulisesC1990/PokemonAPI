using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokemonAPI.Controllers;
using PokemonAPI.Model;
using Xunit;


namespace PokemonAPIIntegrationTest
{
    public class PokemonAPI_IntegrationTest
    {
        private HttpClient Client;

        [Fact, TestPriority(0)]
        public async Task ProbarSeteoPokemon()
        {
            Client = new HttpClient();

            Pokemon pokemon = new Pokemon
            {
                name = "Togepi",
                life = 8000,
                type = PokemonType.agua,
                attacks = new PokemonAttack[]
                    {
                       new PokemonAttack(PokemonType.normal,800),
                       new PokemonAttack(PokemonType.fuego,1000)

                    },
            };

            // Arrange
            var request = new
            {
                Url = "http://ec2-3-139-191-226.us-east-2.compute.amazonaws.com/pokemon/iniciar",

                Body = new PlayerInformation
                {
                    PlayerName = "Ulises",
                    Pokemon = pokemon,
                    State = EnumHelper.GetDescription(PokemonStatus.EN_BATALLA)
                }
            };

            // Act
            var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }



        [Fact, TestPriority(1)]
        public async Task ProbarÌnfoPokemon()
        {
            Client = new HttpClient();
            // Arrange
            var request = "http://ec2-3-139-191-226.us-east-2.compute.amazonaws.com/pokemon/info";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact, TestPriority(2)]
        public async Task ProbarUnirsePartida()
        {
            Client = new HttpClient();
            // Arrange
            var request = new
            {
                Url = "http://ec2-3-139-191-226.us-east-2.compute.amazonaws.com/pokemon/unirse-a-partida"
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(string.Empty));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact, TestPriority(3)]
        public async Task ProbarTerminarPartida()
        {
            Client = new HttpClient();
            // Arrange
            var request = "http://ec2-3-139-191-226.us-east-2.compute.amazonaws.com/pokemon/info";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }



        [Fact, TestPriority(3)]
        public async Task ProbarConexionGimnasioPokemon()
        {
            Client = new HttpClient();

            Pokemon pokemon = new Pokemon
            {
                name = "Togepi",
                life = 8000,
                type = PokemonType.normal,
                attacks = new PokemonAttack[]
                  {
                       new PokemonAttack(PokemonType.normal,800),
                       new PokemonAttack(PokemonType.fuego,1000)

                  },
            };

            // Arrange
            var request = new
            {
                Url = "http://ec2-3-18-23-121.us-east-2.compute.amazonaws.com:8080/pokemon/iniciar",
                Body = new PlayerInformation
                {
                    PlayerName = "Ulises",
                    Pokemon = pokemon,
                    State = EnumHelper.GetDescription(PokemonStatus.EN_BATALLA)
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}