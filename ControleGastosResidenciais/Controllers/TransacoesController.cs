using ControleGastosResidenciais.Services.Transasoes;
using FinanceiroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastosResidenciais.Controllers
{
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacoesController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Transacao transacao)
        {
            try
            {
                var resultado = await _transacaoService.Criar(transacao);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
