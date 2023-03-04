using L01_2020MP602.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020MP602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristasController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public motoristasController(restauranteContext restauranteContexto)
        {
            _restauranteContext = restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Getmotoristas()
        { //VER TODOS LOS motoristas
            List<motoristas> mi_lista = (from e in _restauranteContext.motoristas select e).ToList();
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

        public IActionResult getmoto(int id)
        {// VER DATOS BUSCADOS POR ID 
            motoristas? unmotoristas = (from e in _restauranteContext.motoristas
                                        where e.motoristaId == id
                                        select e).FirstOrDefault();
            if (unmotoristas == null)
                return NotFound();

            return Ok(unmotoristas);

        }

        [HttpGet]
        [Route("find")]
        public IActionResult buscarMoto(String filtro)
        { //Nombre del motorista
            List<motoristas> lista = (from e in _restauranteContext.motoristas
                                      where e.nombreMotorista == filtro
                                      select e).ToList();

            if (lista.Any())
            {
                return Ok(lista);
            }

            return NotFound();

        }

        [HttpPost]
        [Route("add")]

        public IActionResult Crearmoto([FromBody] motoristas motoristanuevo)
        {
            try
            {

                _restauranteContext.motoristas.Add(motoristanuevo);
                _restauranteContext.SaveChanges();

                return Ok(motoristanuevo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar")]
        public IActionResult actualizarmoto(int id, [FromBody] motoristas motorismodificar)
        {
            motoristas? motoexiste = (from e in _restauranteContext.motoristas
                                      where e.motoristaId == id
                                      select e).FirstOrDefault();
            if (motoexiste == null)
                return NotFound();

            motoexiste.motoristaId = motorismodificar.motoristaId;
            motoexiste.nombreMotorista = motorismodificar.nombreMotorista;


            _restauranteContext.Entry(motoexiste).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(motoexiste);
        }
        [HttpDelete]
        [Route("delete/{id}")]

        public IActionResult eliminarmoto(int id)
        {
            motoristas? motoExiste = (from e in _restauranteContext.motoristas
                                     where e.motoristaId == id
                                     select e).FirstOrDefault();
            if (motoExiste == null)
                return NotFound();


            _restauranteContext.Entry(motoExiste).State = EntityState.Deleted;
            _restauranteContext.SaveChanges();

            return Ok(motoExiste);
        }
    }
}