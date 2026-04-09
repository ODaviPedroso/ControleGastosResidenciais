namespace ControleGastosResidenciais.Dtos
{
    // Resposta completa da consulta de totais, contendo o detalhamento por item e os totais gerais
    public class TotaisResponse
    {
        public List<TotalItemDto> Itens { get; set; } = new();
        public decimal TotalGeralReceitas { get; set; }
        public decimal TotalGeralDespesas { get; set; }

        // Saldo líquido geral = total de receitas - total de despesas de todos os itens
        public decimal SaldoLiquido { get; set; }
    }
}
