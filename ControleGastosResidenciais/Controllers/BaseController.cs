using ControleGastosResidenciais.Services.Base;
using FinanceiroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastosResidenciais.Controllers
{
    // Controller genérico que expõe os endpoints CRUD padrão para qualquer entidade.
    // Controllers concretos herdam esta classe e herdam automaticamente todos os endpoints.
    // Aqui fiz todas as operações base de um CRUD, por mais que nem todas vão ser usadas na aplicação, só para exemplo de como exemplo de como criar um controller genérico.

    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase where T : class, IEntidade
    {
        protected readonly IBaseService<T> _service;

        protected BaseController(IBaseService<T> service)
        {
            _service = service;
        }

        // GET api/{entidade} — lista todos os registros
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resultado = await _service.GetAll();
            return Ok(resultado);
        }

        // GET api/{entidade}/{id} — busca um registro pelo Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var resultado = await _service.GetById(id);
            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        // POST api/{entidade} — cria um novo registro
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T entidade)
        {
            try
            {
                var resultado = await _service.Criar(entidade);
                return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/{entidade}/{id} — atualiza um registro existente
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] T entidade)
        {
            try
            {
                var resultado = await _service.Atualizar(id, entidade);
                if (resultado == null)
                    return NotFound();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/{entidade}/{id} — remove um registro pelo Id
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletado = await _service.Deletar(id);
            if (!deletado)
                return NotFound();

            return NoContent();
        }
    }
}
