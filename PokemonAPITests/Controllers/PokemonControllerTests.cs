using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonAPI.Controllers;
using PokemonAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers.Tests
{
    [TestClass()]
    public class PokemonControllerTests
    {
        [TestMethod(), TestCategory("Functional")]
        public void PokemonEstadoCorrecto()
        {
            Pokemon pokemonCreado = new Pokemon();
            pokemonCreado.Name = "Juan";
            pokemonCreado.Type = "Agua";
            pokemonCreado.Life = 100;

            PokemonController controller = new PokemonController();
            controller.Iniciar(pokemonCreado);

            bool esCorrectoEstado = pokemonCreado.Status != EnumHelper.GetDescription(PokemonStatus.DISPONIBLE);

            Assert.IsTrue(esCorrectoEstado);
        }


        [TestMethod(), TestCategory("Functional")]
        public void PokemonEstadoVivo()
        {
            Pokemon pokemonCreado = new Pokemon();
            pokemonCreado.Name = "Juan";
            pokemonCreado.Type = "Agua";
            pokemonCreado.Life = -2000;

            PokemonController controller = new PokemonController();
            controller.Iniciar(pokemonCreado);

            bool esCorrectoEstado = pokemonCreado.Status == EnumHelper.GetDescription(PokemonStatus.DISPONIBLE);

            Assert.IsTrue(esCorrectoEstado);
        }


        [TestMethod(), TestCategory("Functional")]
        public void PokemonCreadoRespuestaCorrecta()
        {
            Pokemon pokemonCreado = new Pokemon();
            pokemonCreado.Name = "Juan";
            pokemonCreado.Type = "Agua";
            pokemonCreado.Life = 200;

            PokemonController controller = new PokemonController();
            var result=controller.Iniciar(pokemonCreado) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }


        [TestMethod(), TestCategory("Functional")]
        public void PokemonCreadoTipoIncorrecto()
        {
            Pokemon pokemonCreado = new Pokemon();
            pokemonCreado.Name = "Kadabara";
            pokemonCreado.Type = "Lana";
            pokemonCreado.Life = 200;

            PokemonController controller = new PokemonController();
            var result = controller.Iniciar(pokemonCreado) as ObjectResult;

            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod(), TestCategory("Functional")]
        public void UnirseAPartida_WhenPokemonIsNull_ReturnsBadRequest()
        {
            // Arrange
            var controller = new PokemonController(); // Create an instance of your controller
         
            // Act
            var result = controller.UnirseAPartida() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);

            dynamic data = result.Value;
            Assert.IsNotNull(data);
            Assert.AreEqual("error", data.status);
            Assert.AreEqual("Error adding pokemon to the battle", data.message);
        }

        [TestMethod(), TestCategory("Functional")]
        public void UnirseAPartida_WhenPokemonIsNotNull_ReturnsOk()
        {
            // Arrange
            var controller = new PokemonController(); // Create an instance of your controller
                                                   // Set up the controller with a non-null value for PokemonBatalla (e.g., using a mock)

            // Act
            var result = controller.UnirseAPartida() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            dynamic data = result.Value;
            Assert.IsNotNull(data);
            Assert.AreEqual("success", data.status);
            Assert.AreEqual("Pokemon ready to fight", data.message);
        }

        [TestMethod]
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
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            dynamic data = result.Value;
            Assert.IsNotNull(data);
            Assert.AreEqual("success", data.status);
            // Add more assertions based on the expected output for a successful attack
        }

        [TestMethod]
        public void Atacar_WhenPokemonIsNull_ReturnsBadRequest()
        {
            // Arrange
            var controller = new PokemonController(); // Create an instance of your controller
                                                   // Set up the controller with a null value for PokemonBatalla

            var body = new
            {
                AttackId =5// Use a valid attack ID
                                           // Add other properties as needed based on your PokemonAttackGym class
            };
            var jsonBody = JsonSerializer.Serialize(body);
            var jsonElement = JsonDocument.Parse(jsonBody).RootElement;

            // Act
            var result = controller.Atacar(jsonElement) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);

            dynamic data = result.Value;
            Assert.IsNotNull(data);
            Assert.AreEqual("error", data.status);
            // Add more assertions based on the expected output for an error
        }


    }
}