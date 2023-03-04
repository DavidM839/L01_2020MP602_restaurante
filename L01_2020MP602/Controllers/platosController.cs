using L01_2020MP602.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020MP602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public platosController(restauranteContext restauranteContexto)
        {
            _restauranteContext = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Getplatos()
        { //VER TODOS LOS platos
            List<platos> mi_lista = (from e in _restauranteContext.platos select e).ToList();
            if (mi_lista.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(mi_lista);
            }
        }
        // localhost:4455/api/equipos/getbyid?id=23&nombre=pwa
        [HttpGet]
        [Route("getbyid/{id}")]

        public IActionResult getplat(int id)
        {// VER DATOS BUSCADOS POR ID 
            platos? unplatos = (from e in _restauranteContext.platos
                                        where e.platoId == id
                                        select e).FirstOrDefault();
            if (unplatos == null)
                return NotFound();

            return Ok(unplatos);

        }

        [HttpGet]
        [Route("find")]
        public IActionResult buscarplato(decimal filtro)
        { //por precio
            List<platos> lista = (from e in _restauranteContext.platos
                                      where e.precio <= filtro
                                      select e).ToList();

            if (lista.Any())
            {
                return Ok(lista);
            }

            return NotFound();

        }

        [HttpPost]
        [Route("add")]

        public IActionResult Crearplato([FromBody] platos platonuevo)
        {
            try
            {

                _restauranteContext.platos.Add(platonuevo);
                _restauranteContext.SaveChanges();

                return Ok(platonuevo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar")]
        public IActionResult actualizarplato(int id, [FromBody] platos platomodificar)
        {
            platos? platoexiste = (from e in _restauranteContext.platos
                                      where e.platoId == id
                                      select e).FirstOrDefault();
            if (platoexiste == null)
                return NotFound();

            platoexiste.nombrePlato = platomodificar.nombrePlato;
            platoexiste.precio = platomodificar.precio;


            _restauranteContext.Entry(platoexiste).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(platoexiste);
        }
        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarplat(int id)
        {
            platos? platoExiste = (from e in _restauranteContext.platos
                                      where e.platoId == id
                                      select e).FirstOrDefault();
            if (platoExiste == null)
                return NotFound();


            _restauranteContext.Entry(platoExiste).State = EntityState.Deleted;
            _restauranteContext.SaveChanges();

            return Ok(platoExiste);
        }
    }
}