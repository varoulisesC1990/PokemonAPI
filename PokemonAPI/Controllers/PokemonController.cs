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
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Numerics;
using System.Text;

namespace PokemonAPI.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    public class PokemonController : ControllerBase
    {
        static PlayerInformation playerBatalla;

         private static string URLGIMNASIO = "http://ec2-3-18-23-121.us-east-2.compute.amazonaws.com:8080/api/gimnasio/";
        //private static string URLGIMNASIO = "http://localhost:8080/api/gimnasio/";


        /// <summary>
        /// Begin Pokemon.
        /// </summary>
        /// <returns>Begin pokemon info</returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Returns success state</response>
        /// <response code="400">Error</response>
        [HttpPost]
        [Route("pokemon/unirse")]
        public IActionResult UnirseABatalla([FromBody] JsonElement body)
        {

            var json = body.GetRawText();
            var model = JsonConvert.DeserializeObject<PlayerInformation>(json);
            PlayerInformation player = model;

            string urlFinal = URLGIMNASIO + "unirse";
            HttpClient client = new HttpClient();
            string statusResponse = String.Empty;
            string message = String.Empty;
            string data = String.Empty;
            string jsonString = string.Empty;
            string tipoFinal = string.Empty;
            string statusFinal = string.Empty;

            try
            {
                if (!Enum.IsDefined(typeof(PokemonType), player.Pokemon.type))
                {
                    var myJson = new
                    {
                        statusResponse = "error",
                        message = "The type not exist",
                    };

                    jsonString = JsonSerializer.Serialize(myJson);

                    return StatusCode(StatusCodes.Status400BadRequest, jsonString);
                }


                if (player.Pokemon.life <= 0)
                {
                    var myJson = new
                    {
                        statusResponse = "error",
                        message = "The life of pokemon must be greater to 0",
                    };

                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status400BadRequest, jsonString);
                }

                if (playerBatalla == null)
                {
                    playerBatalla = player;
                    // Act
                    //var response = Client.PutAsync(urlFinal, ContentHelper.GetStringContent(player));
                    var webRequest = new HttpRequestMessage(HttpMethod.Post, urlFinal)
                    {
                        Content = new StringContent(json, Encoding.UTF8, "application/json")
                    };

                    var response = client.Send(webRequest);

                    if (response.IsSuccessStatusCode)
                    {
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

                else
                {
                    //playerBatalla = null;
                    var myJson = new
                    {
                        statusResponse = "error",
                        message = "The pokemon attributtes cannot be set because it is in battle",
                    };

                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status400BadRequest, jsonString);
                }

            }
            catch (Exception ex)
            {
                playerBatalla = null;
                var myJson = new
                {
                    statusResponse = "error",
                    message = "The pokemon can not be enter to the battle",
                };

                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
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
        [Route("pokemon/iniciar")]
        [HttpPost]
        public IActionResult IniciarGimnasio()
        {
            string urlFinal = URLGIMNASIO + "iniciar";
            HttpClient Client = new HttpClient();

            string jsonString = string.Empty;
            string message = String.Empty;
            if (playerBatalla == null)
            {
                var myJson = new
                {
                    status = "error",
                    message = "Error adding pokemon to the battle"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status400BadRequest, jsonString);
            }

            try
            {
                var response = Client.PostAsync(urlFinal, null);
                var myJson = new
                {
                    status = "success",
                    message = "Pokemon ready to fight"
                };
                jsonString = JsonSerializer.Serialize(myJson);
                return StatusCode(StatusCodes.Status200OK, jsonString);
            }

            catch (Exception ex)
            {
                var myJson = new
                {
                    status = "error",
                    message = "Error adding pokemon to the battle"
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
            string urlFinal = URLGIMNASIO + "atacar";
            HttpClient client = new HttpClient();
            var json = body.GetRawText();

            var model = JsonConvert.DeserializeObject<PokemonAttackGym>(json);

            string status = String.Empty;
            string message = String.Empty;
            string data = String.Empty;
            string jsonString = string.Empty;


            var attackPokemon = playerBatalla.Pokemon.attacks[model.attackId - 1];
            try
            {
                var infoPokemon = new
                {
                    attack = model.attackId,
                    sourcePlayerName = model.sourcePlayerName,
                    targetPlayerName = model.targetPlayerName
                };

                var webRequest = new HttpRequestMessage(HttpMethod.Post, urlFinal)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = client.Send(webRequest);

                if (response.IsSuccessStatusCode)
                {
                    var myJson = new
                    {
                        status = "success",
                        data = infoPokemon,
                        message = "Pokemon attack send successfully"
                    };
                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status200OK, jsonString);
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

            try
            {

                if (pokeLife.life < 0)
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
                    playerBatalla.Pokemon.life = pokeLife.life;

                    var myJson = new
                    {
                        status = "success",
                        message = "Pokemon life has been modified"
                    };
                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status200OK, jsonString);
                }


            }

            catch (Exception ex)
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
            string urlFinal = URLGIMNASIO + "info";
            HttpClient client = new HttpClient();
            string jsonString = string.Empty;
            if (playerBatalla == null)
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

                var webRequest = new HttpRequestMessage(HttpMethod.Get, urlFinal)
                {
                    Content = null
                };

                var response = client.Send(webRequest);

                if (response.IsSuccessStatusCode)
                {

                    var myJson = new
                    {
                        status = "success",
                        data = playerBatalla
                    };
                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status200OK, jsonString);
                }

                else
                {
                    var myJson = new
                    {
                        status = "error",
                        data = "Error getting pokemon info",
                    };

                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status400BadRequest, jsonString);
                }

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
            string urlFinal = URLGIMNASIO + "iniciar";
            HttpClient client = new HttpClient();

            string jsonString = string.Empty;
            string message = String.Empty;
            if (playerBatalla == null)
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
                var webRequest = new HttpRequestMessage(HttpMethod.Post, urlFinal)
                {
                    Content = null
                };

                var response = client.Send(webRequest);


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
            if (playerBatalla == null)
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
                playerBatalla.State = status.Victory == false && playerBatalla.Pokemon.life == 0 ? EnumHelper.GetDescription(PokemonStatus.DERROTADO)
                    : status.Victory == false && playerBatalla.Pokemon.life == 0 ? EnumHelper.GetDescription(PokemonStatus.DISPONIBLE)
                    : EnumHelper.GetDescription(PokemonStatus.GANADOR);

                try
                {
                    var myJson = new
                    {
                        status = "success",
                        message = "Pokemon has been reset"
                    };
                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status200OK, jsonString);
                }

                catch
                {
                    var myJson = new
                    {
                        status = "error",
                        message = "Error reseting pokemon"
                    };
                    jsonString = JsonSerializer.Serialize(myJson);
                    return StatusCode(StatusCodes.Status400BadRequest, jsonString);
                }


            }
        }

    }
}
