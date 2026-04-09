using ControleGastosResidenciais.Data;
using ControleGastosResidenciais.Services.Base;
using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Services.Pessoas
{
    public class PessoaService : BaseService<Pessoa>, IPessoaService
    {
        public PessoaService(AppDbContext context) : base(context)
        {
        }
    }
}
