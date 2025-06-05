using Azure;
using Azure.AI.ContentSafety;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.event_.Contexts;
using webapi.event_.Domains;
using webapi.event_.Interfaces;

namespace webapi.event_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ComentariosEventosController : ControllerBase
    {
        private readonly IComentariosEventosRepository _comentariosEventosRepository;

        private readonly ContentSafetyClient _contentSafetyClient;
        private readonly Context _contexto;
        public ComentariosEventosController(ContentSafetyClient contentSafetyClient, IComentariosEventosRepository comentariosEventosRepository, Context contexto)
        {
            _comentariosEventosRepository = comentariosEventosRepository;
            _contentSafetyClient = contentSafetyClient;
            _contexto = contexto;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ComentariosEventos comentarios)
        {
            try
            {
                Eventos eventoBuscado = _contexto.Eventos.FirstOrDefault(e => e.IdEvento == comentarios.IdEvento);

                if (eventoBuscado == null ) {
                    return NotFound("Evento nao encontrado");
                }
                if (eventoBuscado.DataEvento >= DateTime.UtcNow)
                {
                    return BadRequest("Nao e possivel comentar um evento que ainda nao aconteceu");
                }

                if (string.IsNullOrEmpty(comentarios.Descricao))
                {
                    return BadRequest("O texto a ser moderado nao pode estar vazio!");
                }
                //criacao de objeto
                var request = new AnalyzeTextOptions(comentarios.Descricao);

                //chamar api do content safety
                Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

                //verificar se o texto tem alguma severidade
                bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(c => c.Severity > 0);

                //se o conteudo for impróprio, não exibe o comentário, caso contrario exibe 
                comentarios.Exibe = !temConteudoImproprio;

                //cadastra de fato o comentário
                _comentariosEventosRepository.Cadastrar(comentarios);

                return Ok();
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
                _comentariosEventosRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ListarSomenteExibe")]
        public IActionResult GetExibe(Guid id)
        {
            try
            {
                return Ok(_comentariosEventosRepository.ListarSomenteExibe(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_comentariosEventosRepository.Listar(id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("BuscarPorIdUsuario")]
        public IActionResult GetByIdUser(Guid idUsuario, Guid idEvento)
        {
            try
            {
                return Ok(_comentariosEventosRepository.BuscarPorIdUsuario(idUsuario, idEvento));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
