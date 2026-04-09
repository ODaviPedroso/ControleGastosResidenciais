using ControleGastosResidenciais.Services.Base;
using FinanceiroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastosResidenciais.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<T> : ControllerBase where T : class, IEntidade
    {
        protected readonly IBaseService<T> _service;

        protected BaseController(IBaseService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resultado = await _service.GetAll();
            return Ok(resultado);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var resultado = await _service.GetById(id);
            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

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
