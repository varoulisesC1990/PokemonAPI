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




    }
}