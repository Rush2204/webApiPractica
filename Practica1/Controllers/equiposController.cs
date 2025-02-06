using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica1.Models;
using Microsoft.EntityFrameworkCore;

namespace Practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        /// <summary>
        /// EndPoint que retorna el listado de todos los equipos existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos select e).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipo);

        }

        ///<summary>
        /// EndPoint que retorna registros de una tabla filtrados por id
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        [HttpGet]
        [Route("GetById /{id}")]
        public IActionResult Get(int id) 
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();
            if (equipo == null) 
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        ///<summary>
        /// EndPoint que retorna registros de una tabla filtrados por descripcion
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filro) 
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.descripcion.Contains(filro)
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        ///<summary>
        /// metodo para guardar un registro en la BD
        ///</summary>
        ///
        [HttpPost]
        [Route("add")]
        public IActionResult GuardarEquipo([FromBody] equipos equipo) 
        {
            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
