using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokemonAPI.Controllers;
using PokemonAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers.Tests
{
    [TestClass()]
    public class PokemonControllerTests
    {
        //[TestMethod(), TestCategory("Functional")]
        //public void PokemonEstadoCorrecto()
        //{
        //    Pokemon pokemonCreado = new Pokemon();
        //    pokemonCreado.Name = "Juan";
        //    pokemonCreado.Type = "Agua";
        //    pokemonCreado.Life = 100;

        //    PokemonController controller = new PokemonController();
        //    controller.Iniciar(pokemonCreado);

        //    bool esCorrectoEstado = pokemonCreado.Status != EnumHelper.GetDescription(PokemonStatus.DISPONIBLE);

        //    Assert.IsTrue(esCorrectoEstado);
        //}


        //[TestMethod(), TestCategory("Functional")]
        //public void PokemonEstadoVivo()
        //{
        //    Pokemon pokemonCreado = new Pokemon();
        //    pokemonCreado.Name = "Juan";
        //    pokemonCreado.Type = "Agua";
        //    pokemonCreado.Life = -2000;

        //    PokemonController controller = new PokemonController();
        //    controller.Iniciar(pokemonCreado);

        //    bool esCorrectoEstado = pokemonCreado.Status == EnumHelper.GetDescription(PokemonStatus.EN_BATALLA);

        //    Assert.IsTrue(esCorrectoEstado);
        //}


        //[TestMethod(), TestCategory("Functional")]
        //public void PokemonCreadoRespuestaCorrecta()
        //{
        //    Pokemon pokemonCreado = new Pokemon();
        //    pokemonCreado.Name = "Juan";
        //    pokemonCreado.Type = "Agua";
        //    pokemonCreado.Life = 200;

        //    PokemonController controller = new PokemonController();
        //    var result = controller.Iniciar(pokemonCreado) as ObjectResult;

        //    Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        //}


        //[TestMethod(), TestCategory("Functional")]
        //public void PokemonCreadoTipoIncorrecto()
        //{
        //    Pokemon pokemonCreado = new Pokemon();
        //    pokemonCreado.Name = "Kadabara";
        //    pokemonCreado.Type = "Lana";
        //    pokemonCreado.Life = 200;

        //    PokemonController controller = new PokemonController();
        //    var result = controller.Iniciar(pokemonCreado) as ObjectResult;

        //    Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        //}

        [TestMethod(), TestCategory("Functional")]
        public void UnirseAPartida_WhenPokemonIsNull_ReturnsBadRequest()
        {
            // Arrange
            var controller = new PokemonController(); // Create an instance of your controller

            // Act
            var result = controller.IniciarGimnasio() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);

           
        }

        [TestMethod(), TestCategory("Functional")]
        public void UnirseAPartida_WhenPokemonIsNotNull_ReturnsOk()
        {
            // Arrange
            var controller = new PokemonController(); // Create an instance of your controller
                                                      // Set up the controller with a non-null value for PokemonBatalla (e.g., using a mock)
            // Act
            var result = controller.IniciarGimnasio() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod(), TestCategory("Functional")]
        public void Pokemon_Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            string name = "Charmander";
            double life = 100.05;
            PokemonAttack[] attacks = new PokemonAttack[] { new PokemonAttack(PokemonType.normal, 200) };
            string status = EnumHelper.GetDescription(PokemonStatus.DISPONIBLE); // Using extension method to get enum description

            // Act
            Pokemon pokemon = new Pokemon(name, PokemonType.fuego, life, attacks);

            // Assert
            Assert.AreEqual(name, pokemon.name);
            Assert.AreEqual(PokemonType.normal, pokemon.type);
            Assert.AreEqual(life, pokemon.life);
            Assert.AreSame(attacks, pokemon.attacks);
        }


        [TestMethod(), TestCategory("Functional")]
        public void Atacar_WhenPokemonIsNotNullAndAttackIsValid_ReturnsOk()
        {
            // Arrange
            var controller = new PokemonController(); // Create an instance of your controller
                                                      // Set up the controller with a non-null value for PokemonBatalla (e.g., using a mock)
                                                      //controller.PokemonBatalla = new Pokemon(); // Set up PokemonBatalla with a valid Pokemon
            var validAttackId = "1"; // Set up a valid attack ID

            var body = new
            {
                AttackId = validAttackId // Ensure the body contains a valid attack ID
                                         // Add other properties as needed based on your PokemonAttackGym class
            };
            var jsonBody = JsonSerializer.Serialize(body);
            var jsonElement = JsonDocument.Parse(jsonBody).RootElement;

            // Act
            var result = controller.Atacar(jsonElement) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);

          
        }

        [TestMethod(), TestCategory("Functional")]
        public void Atacar_WhenPokemonIsNull_ReturnsBadRequest()
        {
            // Arrange
            var controller = new PokemonController(); // Create an instance of your controller
                                                      // Set up the controller with a null value for PokemonBatalla

            var body = new
            {
                AttackId = 5// Use a valid attack ID
                            // Add other properties as needed based on your PokemonAttackGym class
            };
            var jsonBody = JsonSerializer.Serialize(body);
            var jsonElement = JsonDocument.Parse(jsonBody).RootElement;

            // Act
            var result = controller.Atacar(jsonElement) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status403Forbidden, result.StatusCode);

            //dynamic data = result.Value;
            //Assert.IsNotNull(data);
            //Assert.AreEqual("error", result.StatusCode);
            //// Add more assertions based on the expected output for an error
        }


        [TestMethod(), TestCategory("Functional")]
        public void PokemonAttack_Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            int power = 3000;

            // Act
            PokemonAttack pokemonAttack = new PokemonAttack(PokemonType.fuego, power);

            // Assert
            Assert.AreEqual(power, pokemonAttack.power);
        }


        [TestMethod(), TestCategory("Functional")]
        public void PokemonAttackGym_Properties_ShouldBeSetCorrectly()
        {
            // Arrange
            int attackId = 1;
            int sourcePlayerName = 42;
            int targetPlayerName = 42;

            // Act
            PokemonAttackGym pokemonAttackGym = new PokemonAttackGym
            {
                sourcePlayerName="Ulises",
                targetPlayerName="Renato",
                attackId = attackId
            };

            // Assert
            Assert.AreEqual(attackId, pokemonAttackGym.attackId);
            Assert.AreEqual(sourcePlayerName, pokemonAttackGym.sourcePlayerName);
        }

        [TestMethod(), TestCategory("Functional")]
        public void Status_VictoryProperty_ShouldBeSetCorrectly()
        {
            // Arrange
            bool expectedVictory = true;

            // Act
            Status status = new Status
            {
                Victory = expectedVictory
            };

            // Assert
            Assert.AreEqual(expectedVictory, status.Victory);
        }

        [TestMethod]
        public void PlayerInformation_DefaultConstructor_ShouldInitializeProperties()
        {
            // Arrange & Act
            var playerInfo = new PlayerInformation();

            // Assert
            Assert.IsNotNull(playerInfo);
            Assert.IsNull(playerInfo.PlayerName);
            Assert.IsNull(playerInfo.State);
            Assert.IsNull(playerInfo.Pokemon);
        }

        [TestMethod]
        public void PlayerInformation_ParameterizedConstructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            string playerName = "Ash Ketchum";
            string state = "Active";
            var pokemon = new Pokemon("Pikachu", PokemonType.fuego, 100, new PokemonAttack[] { new PokemonAttack(PokemonType.fuego, 30) });

            // Act
            var playerInfo = new PlayerInformation(playerName, state, pokemon);

            // Assert
            Assert.IsNotNull(playerInfo);
            Assert.AreEqual(playerName, playerInfo.PlayerName);
            Assert.AreEqual(state, playerInfo.State);
            Assert.AreSame(pokemon, playerInfo.Pokemon);
        }

        [TestMethod]
        public void PlayerInformation_SetProperties_ShouldUpdateValues()
        {
            // Arrange
            var playerInfo = new PlayerInformation();

            // Act
            playerInfo.PlayerName = "Misty";
            playerInfo.State = "Inactive";
            playerInfo.Pokemon = new Pokemon("Starmie", PokemonType.agua, 90, new PokemonAttack[] { new PokemonAttack(PokemonType.agua, 25) });

            // Assert
            Assert.AreEqual("Misty", playerInfo.PlayerName);
            Assert.AreEqual("Inactive", playerInfo.State);
            Assert.IsNotNull(playerInfo.Pokemon);
        }

        [TestMethod]
        public void UnirseABatalla_WhenPlayerBatallaIsNull_ReturnsSuccess()
        {
            // Arrange
            var controller = new PokemonController();
            var playerInfo = new PlayerInformation
            {
                Pokemon = new Pokemon("Charizard", PokemonType.fuego, 100, new PokemonAttack[] { new PokemonAttack(PokemonType.fuego, 50) })
            };
            var json = JsonSerializer.Serialize(playerInfo);
            var body = new JsonElement(); // You may need to mock this depending on your actual usage

            var clientMock = new Mock<HttpClient>();
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            clientMock.Setup(c => c.Send(It.IsAny<HttpRequestMessage>())).Returns(responseMessage);

            

            // Act
            IActionResult result = controller.UnirseABatalla(body);

            // Assert
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var responseData = okResult.Value as dynamic;
            Assert.AreEqual("success", responseData.statusResponse);
            Assert.AreEqual("Pokemon attributes set successfully", responseData.message);
        }

        [TestMethod]
        public void UnirseABatalla_WhenPlayerBatallaIsNotNull_ReturnsError()
        {
            // Arrange
            var controller = new PokemonController();
            //controller.PlayerBatalla = new PlayerInformation(); // Assuming PlayerBatalla is a property in your controller
            var playerInfo = new PlayerInformation
            {
                Pokemon = new Pokemon("Blastoise", PokemonType.agua, 90, new PokemonAttack[] { new PokemonAttack(PokemonType.agua, 40) })
            };
            var json = JsonSerializer.Serialize(playerInfo);
            var body = new JsonElement(); // You may need to mock this depending on your actual usage

            // Act
            IActionResult result = controller.UnirseABatalla(body);

            
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var responseData = badRequestResult.Value as dynamic;
            Assert.AreEqual("error", responseData.statusResponse);
            Assert.AreEqual("The pokemon attributtes cannot be set because it is in battle", responseData.message);
        }

 
    }
}
