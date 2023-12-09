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
            var request = "http://ec2-3-139-191-226.us-east-2.compute.amazonaws.com/pokemon/info";

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
                Url = "http://ec2-3-139-191-226.us-east-2.compute.amazonaws.com/pokemon/iniciar",
                Body = new Pokemon
                {
                    //Id = 50,
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
            var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }



        [Fact]
        public async Task ProbarConexionGimnasioPokemon()
        {
            Client = new HttpClient();
            // Arrange
            var request = new
            {
                Url = "http://ec2-3-18-23-121.us-east-2.compute.amazonaws.com:8080/pokemon/iniciar",
                Body = new Pokemon
                {
                    //Id = 50,
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