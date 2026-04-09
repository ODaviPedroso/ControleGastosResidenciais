using ControleGastosResidenciais.Services.Transasoes;
using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Controllers
{
    public class TransacoesController : BaseController<Transacao>
    {
        public TransacoesController(ITransacaoService service) : base(service)
        {
        }
    }
}
