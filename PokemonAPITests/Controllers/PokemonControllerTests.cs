using Microsoft.AspNetCore.Http;
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
            pokemonCreado.Status = "Muerto";
            pokemonCreado.Life = 100;

            PokemonController controller = new PokemonController();
            controller.Iniciar(pokemonCreado);

            bool esCorrectoEstado = pokemonCreado.Status != EnumHelper.GetDescription(PokemonStatus.Available);

            Assert.IsTrue(esCorrectoEstado);
        }


        [TestMethod(), TestCategory("Functional")]
        public void PokemonEstadoVivo()
        {
            Pokemon pokemonCreado = new Pokemon();
            pokemonCreado.Name = "Juan";
            pokemonCreado.Type = "Agua";
            pokemonCreado.Status = "Muerto";
            pokemonCreado.Life = -2000;

            PokemonController controller = new PokemonController();
            controller.Iniciar(pokemonCreado);

            bool esCorrectoEstado = pokemonCreado.Status == EnumHelper.GetDescription(PokemonStatus.Available);

            Assert.IsTrue(esCorrectoEstado);
        }



        //[TestMethod, TestCategory("Functional")]
        //public void RegistraTiqueteEnSei_ServicioOperando_NumeroTiqueteRegistrado()
        //{

        //    IEnumerable<TiqueteSei> valorEsperado()
        //    {
        //        yield return new TiqueteSei() { IdTiquete = ID_TIQUETE_ESPERADO, Mensaje = MENSAJE };
        //    }
        //    Task<IEnumerable<TiqueteSei>> s = Task.FromResult<IEnumerable<TiqueteSei>>(valorEsperado());


        //    elTiquete.RegistreConsultaEnSeiEnAnonimoAsync(ID_PANEL, ID_CONFIGURACION).Returns(s);
        //    TiqueteSeiController elServicioSei = new TiqueteSeiController(elTiquete);
        //    var idTiquete = elServicioSei.RegistreConsultaEnSeiEnAnonimoAsync(ID_PANEL, ID_CONFIGURACION);

        //    bool tiqueteCorrecto = idTiquete.Status != TaskStatus.Faulted;
        //    Assert.IsTrue(tiqueteCorrecto);
        //}
    }
}