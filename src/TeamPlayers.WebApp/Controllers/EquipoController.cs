using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamPlayers.Core.Entities;
using TeamPlayers.Core.Models;
using TeamPlayers.Core.Services;

namespace TeamPlayers.WebApp.Controllers
{
    public class EquipoController : Controller
    {
        private readonly IEquipoServices _equipoServices;
        public EquipoController(IEquipoServices equipoServices)
        {
            _equipoServices = equipoServices;
        }

        [Route("equipo")]
        public ActionResult Mantenimiento()
        {
            var equipo = new Equipo();
            return View(equipo);
        }

        #region APIs
        [HttpGet("/api/equipo/{id:int}", Name = "GetEquipoById")]
        public async Task<ActionResult<ResponseModel<Equipo>>> GetEquipoById(int id)
        {
            var response = new ResponseModel<Equipo>();
            try
            {
                response.Result = await _equipoServices.GetEquipoById(id);

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

        [HttpGet("/api/equipo")]
        public async Task<ActionResult<ResponseModel<IEnumerable<Equipo>>>> GetAllEquipos()
        {
            var response = new ResponseModel<IEnumerable<Equipo>>();
            try
            {
                response.Result = await _equipoServices.GetAllEquipos();

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

        [HttpPost("/api/equipo")]
        public async Task<ActionResult<ResponseModel<Equipo>>> AddEquipo([FromBody] Equipo equipo)
        {
            var response = new ResponseModel<Equipo>();
            try
            {
                response.Result = await _equipoServices.AddEquipo(equipo);

                if (response.Result == null)
                    return NotFound();

                return CreatedAtRoute("GetEquipoById", new { id = response.Result.Id }, response.Result);
            }
            catch (Exception ex)
            {
                response.SetErrorMessage((ex.InnerException ?? ex).Message);
                return BadRequest(response);
            }
        }

        [HttpPut("/api/equipo")]
        public async Task<ActionResult<ResponseModel<Equipo>>> UpdateEquipo([FromBody] Equipo equipo)
        {
            var response = new ResponseModel<Equipo>();
            try
            {
                response.Result = await _equipoServices.UpdateEquipo(equipo);

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

        [HttpPut("/api/equipo/status")]
        public async Task<ActionResult<ResponseModel<Equipo>>> ToggleEquipoById([FromBody] ToggleDTO toggleDTO)
        {
            var response = new ResponseModel<Equipo>();
            try
            {
                response.Result = await _equipoServices.ToggleEquipoById(toggleDTO.Id);

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

        [HttpDelete("/api/equipo/{id:int}")]
        public async Task<ActionResult<ResponseModel<Equipo>>> DeleteEquipoById(int id)
        {
            var response = new ResponseModel<Equipo>();
            try
            {
                response.Result = await _equipoServices.DeleteEquipoById(id);

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
