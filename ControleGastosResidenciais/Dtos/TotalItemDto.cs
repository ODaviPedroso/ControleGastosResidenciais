namespace ControleGastosResidenciais.Dtos
{
    // Representa o resumo financeiro de um item (pessoa ou categoria)
    public class TotalItemDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }

        // Saldo = receitas - despesas
        public decimal Saldo { get; set; }
    }
}
