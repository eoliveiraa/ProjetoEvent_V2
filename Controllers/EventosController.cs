using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.event_.Domains;
using webapi.event_.Interfaces;
using webapi.event_.Repositories;

namespace webapi.event_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EventosController : ControllerBase
    {
        private readonly IEventosRepository _eventosRepository;
        public EventosController(IEventosRepository eventosRepository)
        {
            _eventosRepository = eventosRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_eventosRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ListarPorId")]
        public IActionResult ListById(Guid id)
        {
            try
            {
                return Ok(_eventosRepository.ListarPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ListarProximos")]
        public IActionResult GetNextEvents()
        {
            try
            {
                return Ok(_eventosRepository.ProximosEventos());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_eventosRepository.BuscarPorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Eventos evento)
        {
            try
            {
                _eventosRepository.Cadastrar(evento);


                return StatusCode(201, evento);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Eventos evento)
        {
            try
            {
                _eventosRepository.Atualizar(id, evento);

                return StatusCode(204, evento);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _eventosRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
