using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using PokemonAPI.Model;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace PokemonAPI.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    public class PokemonController : ControllerBase
    {
        static Pokemon pokemonBatalla;


        /// <summary>
        /// Begin Pokemon.
        /// </summary>
        /// <returns>Begin pokemon info</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns success state</response>
        /// <response code="400">Error</response>
        [HttpPut]
        [Route("pokemon/iniciar")]
        public IActionResult Iniciar(Pokemon pokemonNuevo)
        {
            string statusResponse = String.Empty;
            string message = String.Empty;
            string data = String.Empty;
            string jsonString = string.Empty;
            string tipoFinal = string.Empty;
            string statusFinal = string.Empty;

            if (Enum.IsDefined(typeof(PokemonType),pokemonNuevo.Type))
                tipoFinal = pokemonNuevo.Type;
            else
            {
                var myJson = new
                {
                    statusResponse = "error",
                    message = "The type not exist",
                };

                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }


            if (pokemonNuevo.Life<=0)
            { 
                var myJson = new
                {
                    statusResponse = "error",
                    message = "The life of pokemon must be greater to 0",
                };

                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }

            if (pokemonBatalla == null)
            {
                //if (pokemonNuevo.Name == String.Empty)
                //    CargarArchivoLocalPokemon(out pokemonBatalla);

                
                    pokemonBatalla = new Pokemon
                    {
                        Name = pokemonNuevo.Name,
                        Life = pokemonNuevo.Life,
                        Type = tipoFinal,
                        Attacks = pokemonNuevo.Attacks,
                        Status= EnumHelper.GetDescription(PokemonStatus.EN_BATALLA)
                    };


                var myJson = new
                {
                    statusResponse = "success",
                    message = "Pokemon attributes set successfully",
                };

                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status200OK, jsonString);
                
            }
            else
            {
                var myJson = new
                {
                    statusResponse = "error",
                    message = "The pokemon attributtes cannot be set because it is in battle",
                };

                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }
        }

        /// <summary>
        /// Pokemon send attack.
        /// </summary>
        /// <returns>Pokemon attack</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns success state</response>
        /// <response code="400">Error</response>
        [Route("pokemon/atacar")]
        [HttpPost]
        public IActionResult Atacar([FromBody] JsonElement body)
        {
            var json = body.GetRawText();
            
            var model = JsonConvert.DeserializeObject<PokemonAttackGym>(json);

            string status = String.Empty;
            string message = String.Empty;
            string data = String.Empty;
            string jsonString = string.Empty;


            if (pokemonBatalla != null)
            {
                Pokemon pokemonInfoAttack = new Pokemon();
               // pokemonInfoAttack.Id = pokemonBatalla.Id;
                pokemonInfoAttack.Name = pokemonBatalla.Name;
                pokemonInfoAttack.Type = pokemonBatalla.Type;
                pokemonInfoAttack.Life = pokemonBatalla.Life;

                try
                {
                    var attackPokemon = pokemonBatalla.Attacks[model.AttackId];

                    var pokemonInfoCorta = new
                    {
                        //Id = pokemonInfoAttack.Id,
                        Name = pokemonInfoAttack.Name,
                        Type = pokemonInfoAttack.Type,
                        Life = pokemonInfoAttack.Life
                    };

                    var infoPokemon = new
                    {
                        attack = attackPokemon,
                        pokemon = pokemonInfoCorta,
                    };


                    var myJson = new
                    {
                        status = "success",
                        data = infoPokemon,
                        message = "Pokemon attack send successfully"
                    };
                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status200OK, jsonString);
                }

                catch (Exception ex)
                {
                    var myJson = new
                    {
                        status = "error",
                        message = "Pokemon or attack not valid"
                    };
                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status400BadRequest, jsonString);
                }
            }

            else
            {
                var myJson = new
                {
                    status = "error",
                    message = "Pokemon or attack not valid"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }

        
        }

        /// <summary>
        /// Change Pokemon life.
        /// </summary>
        /// <returns>Get pokemon new life</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns success state</response>
        /// <response code="400">Error</response>
        [Route("pokemon/editar-vida")]
        [HttpPut]
        public IActionResult EditarVida(Pokemon pokeLife)
        {
            string status = String.Empty;
            string message = String.Empty;
            string data = String.Empty;
            string jsonString = string.Empty;

            if (pokemonBatalla != null)
            {

                if (pokeLife.Life < 0)
                {
                    var myJson = new
                    {
                        status = "error",
                        message = "Error modifying Pokemon life"
                    };

                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status400BadRequest, jsonString);
                }

                else
                {
                    pokemonBatalla.Life = pokeLife.Life;

                    var myJson = new
                    {
                        status = "success",
                        message = "Pokemon life has been modified"
                    };
                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status200OK, jsonString);
                }

               
            }

            else
            {
                var myJson = new
                {
                    status = "error",
                    message = "Error modifying Pokemon life"
                };

                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }

        
        }


        /// <summary>
        /// Get pokemon info.
        /// </summary>
        /// <returns>Get pokemon info</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [Route("pokemon/info")]
        [HttpGet]
        public IActionResult InfoPokemon()
        {
            string jsonString = string.Empty;
            if (pokemonBatalla == null)
            {
                var myJson = new
                {
                    status = "error",
                    data = "Error getting pokemon info",
                };

                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }

            else
            {
                var myJson = new
                {
                    status = "success",
                    data = pokemonBatalla
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status200OK, jsonString);

            }
        }

        /// <summary>
        /// Start a new game.
        /// </summary>
        /// <returns>Get new game</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns success state</response>
        /// <response code="400">Error</response>
        [Route("pokemon/iniciar-turno")]
        [HttpPost]
        public IActionResult IniciarTurno()
        {
            string jsonString = string.Empty;
            string message = String.Empty;
            if (pokemonBatalla == null)
            {
                var myJson = new
                {
                    status = "error",
                    message = "Error initializing turn"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }

            else
            {
                var myJson = new
                {
                    status = "success",
                    message = "Turn initialized successfully"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status200OK, jsonString);
            }

        }


        /// <summary>
        /// Start a new game.
        /// </summary>
        /// <returns>Get new game</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns success state</response>
        /// <response code="400">Error</response>
        [Route("pokemon/unirse-a-partida")]
        [HttpPost]
        public IActionResult UnirseAPartida()
        {
            string jsonString = string.Empty;
            string message = String.Empty;
            if (pokemonBatalla == null)
            {
                var myJson = new
                {
                    status = "error",
                    message = "Error adding pokemon to the battle"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }

            else
            {
                var myJson = new
                {
                    status = "success",
                    message = "Pokemon ready to fight"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status200OK, jsonString);
            }

        }


        /// <summary>
        /// Finish a  game.
        /// </summary>
        /// <returns>Finish game</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns success state</response>
        /// <response code="400">Error</response>
        [Route("pokemon/terminar-partida")]
        [HttpPost]
        public IActionResult TerminarPartida(Status status)
        {
            string jsonString = string.Empty;
            string message = String.Empty;
            if (pokemonBatalla == null)
            {
                var myJson = new
                {
                    status = "error",
                    message = "Error reseting pokemon"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }

            else
            {
                pokemonBatalla.Status = status.Victory==false && pokemonBatalla.Life==0? EnumHelper.GetDescription(PokemonStatus.DERROTADO) 
                    : status.Victory == false && pokemonBatalla.Life == 0 ? EnumHelper.GetDescription(PokemonStatus.DISPONIBLE) 
                    : EnumHelper.GetDescription(PokemonStatus.GANADOR);

                var myJson = new
                {
                    status = "success",
                    message = "Pokemon has been reset"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status200OK, jsonString);
            }
        }

    }
}
