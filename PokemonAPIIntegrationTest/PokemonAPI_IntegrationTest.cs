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
        [Fact]
        public async Task ProbarÌnfoPokemon()
        {
            Client = new HttpClient();
            // Arrange
            var request = "http://pokemonapi-dev.eba-sqwi5tuh.us-east-2.elasticbeanstalk.com/pokemon/info";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task ProbarSeteoPokemon()
        {
            Client = new HttpClient();
            // Arrange
            var request = new
            {
                Url = "http://pokemonapi-dev.eba-sqwi5tuh.us-east-2.elasticbeanstalk.com/pokemon/iniciar",
                Body = new Pokemon
                {
                    Id = 50,
                    Name = "Togepi",
                    Life = 8000,
                    Type = "Agua",
                    Attacks = new PokemonAttack[]
                    {
                       new PokemonAttack("Ternura",800),
                       new PokemonAttack("Canto",1000)
                        
                    },
                    Status = EnumHelper.GetDescription(PokemonStatus.EN_BATALLA)
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