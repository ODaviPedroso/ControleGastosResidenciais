using ControleGastosResidenciais.Services.Pessoas;
using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Controllers
{
    public class PessoasController : BaseController<Pessoa>
    {
        public PessoasController(IPessoaService service) : base(service)
        {
        }
    }
}
