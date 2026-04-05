using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Services.Transasoes
{
    public interface ITransacaoService
    {
        Task<Transacao> Criar(Transacao transacao);
    }
}
