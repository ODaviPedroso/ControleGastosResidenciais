using ControleGastosResidenciais.Services.Relatorios;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastosResidenciais.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        // Retorna o total de receitas, despesas e saldo de cada pessoa, mais o total geral
        [HttpGet("totais-por-pessoa")]
        public async Task<IActionResult> TotaisPorPessoa()
        {
            var resultado = await _relatorioService.GetTotaisPorPessoa();
            return Ok(resultado);
        }

        // Retorna o total de receitas, despesas e saldo de cada categoria, mais o total geral
        [HttpGet("totais-por-categoria")]
        public async Task<IActionResult> TotaisPorCategoria()
        {
            var resultado = await _relatorioService.GetTotaisPorCategoria();
            return Ok(resultado);
        }
    }
}
