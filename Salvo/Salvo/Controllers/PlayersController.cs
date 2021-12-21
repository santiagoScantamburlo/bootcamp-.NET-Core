using Microsoft.AspNetCore.Mvc;
using Salvo.Models;
using Salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Salvo.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IPlayerRepository _repository;

        public PlayersController(IPlayerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlayerDTO player)
        {
            try
            {
                Regex regexEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                Regex regexPassword = new Regex("^.*(?=.{10,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$");
                //Verificar si Email y Password estan vacios
                if (String.IsNullOrEmpty(player.Email) || String.IsNullOrEmpty(player.Password))
                {
                    return StatusCode(403, "Datos inválidos");
                }

                if (!regexEmail.IsMatch(player.Email)){
                    return StatusCode(403, "Email inválido");
                }

                if(!regexPassword.IsMatch(player.Password)) {
                    return StatusCode(403, "Contraseña inválida");
                }

                Player dbPlayer = _repository.FindByEmail(player.Email);
                if(dbPlayer != null)
                {
                    return StatusCode(403, "Email en uso");
                }

                Player newPlayer = new Player
                {
                    Email = player.Email,
                    Password = player.Password,
                    Name = player.Name
                };

                _repository.Save(newPlayer);

                return StatusCode(201, newPlayer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
