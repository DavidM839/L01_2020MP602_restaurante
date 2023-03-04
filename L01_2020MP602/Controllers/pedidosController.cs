using L01_2020MP602.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace L01_2020MP602.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class pedidosController : ControllerBase
        {
            private readonly restauranteContext _restauranteContext;

            public pedidosController(restauranteContext restauranteContexto)
            {
                _restauranteContext = restauranteContexto;
            }

            [HttpGet]
            [Route("GetAll")]
            public IActionResult Get()
            { //VER TODOS LOS PEDIDOS
                List<pedidos> mi_lista = (from e in _restauranteContext.pedidos select e).ToList();
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

            public IActionResult get(int id)
            {// VER DATOS BUSCADOS POR ID 
                pedidos? unpedido = (from e in _restauranteContext.pedidos
                                     where e.pedidoId == id 
                                     select e).FirstOrDefault();
                if (unpedido == null)
                    return NotFound();

                return Ok(unpedido);

            }

            [HttpGet]
            [Route("encontrar")]
            public IActionResult buscar(int filtro)
            { //id del cliente
                List<pedidos> lista = (from e in _restauranteContext.pedidos
                                             where e.clienteId == filtro
                                             select e).ToList();

                if (lista.Any()) 
                {
                    return Ok(lista);
                }

                return NotFound();

            }
        [HttpGet]
        [Route("bymotorista")]
        public IActionResult buscarmotorista(int filtro)
        { //id del motorista
            List<pedidos> lista = (from e in _restauranteContext.pedidos
                                   where e.motoristaId == filtro
                                   select e).ToList();

            if (lista.Any())
            {
                return Ok(lista);
            }

            return NotFound();

        }
        [HttpPost]
            [Route("add")]

            public IActionResult Crearped([FromBody] pedidos pedidonuevo)
            {
                try
                {

                _restauranteContext.pedidos.Add(pedidonuevo);
                _restauranteContext.SaveChanges();

                    return Ok(pedidonuevo);

                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
            [HttpPut]
            [Route("actualizar")]
            public IActionResult actualizarped(int id, [FromBody] pedidos pedidomodificar)
            {
            pedidos? pedidoexiste = (from e in _restauranteContext.pedidos
                                         where e.pedidoId == id
                                         select e).FirstOrDefault();
                if (pedidoexiste == null)
                    return NotFound();

            pedidoexiste.motoristaId = pedidomodificar.motoristaId;
            pedidoexiste.clienteId = pedidomodificar.clienteId;
            pedidoexiste.platoId = pedidomodificar.platoId;
            pedidoexiste.cantidad = pedidomodificar.cantidad;
            pedidoexiste.precio = pedidomodificar.precio;

            _restauranteContext.Entry(pedidoexiste).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

                return Ok(pedidoexiste);
            }
            [HttpDelete]
            [Route("delete/{id}")]

            public IActionResult eliminarped(int id)
            {
                pedidos? pedidoExis = (from e in _restauranteContext.pedidos
                                         where e.pedidoId == id
                                         select e).FirstOrDefault();
                if (pedidoExis == null)
                    return NotFound();


            _restauranteContext.Entry(pedidoExis).State = EntityState.Deleted;
            _restauranteContext.SaveChanges();

                return Ok(pedidoExis);
            }
        }
    }
