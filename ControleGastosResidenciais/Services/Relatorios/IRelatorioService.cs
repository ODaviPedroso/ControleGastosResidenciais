using ControleGastosResidenciais.Dtos;

namespace ControleGastosResidenciais.Services.Relatorios
{
    public interface IRelatorioService
    {
        // Retorna o resumo financeiro agrupado por pessoa + totais gerais
        Task<TotaisResponse> GetTotaisPorPessoa();

        // Retorna o resumo financeiro agrupado por categoria + totais gerais
        Task<TotaisResponse> GetTotaisPorCategoria();
    }
}
