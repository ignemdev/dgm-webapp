using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;
using TeamPlayers.Core.Models;
using TeamPlayers.Core.Services;

namespace TeamPlayers.WebApp.Controllers
{
    public class JugadorController : Controller
    {
        private readonly IJugadorServices _jugadorServices;
        public JugadorController(IJugadorServices jugadorServices)
        {
            _jugadorServices = jugadorServices;
        }

        [Route("jugador")]
        public ActionResult Mantenimiento()
        {
            var jugador = new Jugador();
            return View(jugador);
        }

        [Route("jugador/activos")]
        public ActionResult Activos() => View();

        [Route("jugador/inactivos")]
        public ActionResult Inactivos() => View();

        [Route("jugador/libres")]
        public ActionResult Libres() => View();

        #region APIs
        [HttpGet("/api/jugador/{id:int}", Name = "GetJugadorById")]
        public async Task<ActionResult<ResponseModel<Jugador>>> GetJugadorById(int id)
        {
            var response = new ResponseModel<Jugador>();
            try
            {
                response.Result = await _jugadorServices.GetJugadorById(id);

                if (response.Result == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }

        [HttpGet("/api/jugador")]
        public async Task<ActionResult<ResponseModel<IEnumerable<Jugador>>>> GetAllJugadores([FromQuery] Jugador props)
        {
            var response = new ResponseModel<IEnumerable<Jugador>>();
            try
            {
                response.Result = await _jugadorServices.GetAllJugadoresByProps(props);

                if (response.Result == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }

        [HttpPost("/api/jugador")]
        public async Task<ActionResult<ResponseModel<Jugador>>> AddJugador([FromBody] Jugador jugador)
        {
            var response = new ResponseModel<Jugador>();
            try
            {
                response.Result = await _jugadorServices.AddJugador(jugador);

                if (response.Result == null)
                    return NotFound();

                return CreatedAtRoute("GetJugadorById", new { id = response.Result.Id }, response.Result);
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }

        [HttpPut("/api/jugador")]
        public async Task<ActionResult<ResponseModel<Jugador>>> UpdateJugador([FromBody] Jugador jugador)
        {
            var response = new ResponseModel<Jugador>();
            try
            {
                response.Result = await _jugadorServices.UpdateJugador(jugador);

                if (response.Result == null)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }

        [HttpPut("/api/jugador/toggle")]
        public async Task<ActionResult<ResponseModel<Jugador>>> ToggleJugadorById([FromBody] ToggleDTO toggleDTO)
        {
            var response = new ResponseModel<Jugador>();
            try
            {
                response.Result = await _jugadorServices.ToggleJugadorById(toggleDTO.Id);

                if (response.Result == null)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }

        [HttpPut("/api/jugador/equipo")]
        public async Task<ActionResult<ResponseModel<Jugador>>> ChangeJugadorTeam([FromBody] Jugador jugador)
        {
            var response = new ResponseModel<Jugador>();
            try
            {
                response.Result = await _jugadorServices.ChangeJugadorTeam(jugador);

                if (response.Result == null)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }

        [HttpPut("/api/jugador/liberar")]
        public async Task<ActionResult<ResponseModel<Jugador>>> HacerJugadorAgenteLibre([FromBody] Jugador jugador)
        {
            var response = new ResponseModel<Jugador>();
            try
            {
                response.Result = await _jugadorServices.HacerJugadorAgenteLibre(jugador);

                if (response.Result == null)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }

        [HttpDelete("/api/jugador/{id:int}")]
        public async Task<ActionResult<ResponseModel<Jugador>>> DeleteJugadorById(int id)
        {
            var response = new ResponseModel<Jugador>();
            try
            {
                response.Result = await _jugadorServices.DeleteJugadorById(id);

                if (response.Result == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }
        #endregion
    }

}
