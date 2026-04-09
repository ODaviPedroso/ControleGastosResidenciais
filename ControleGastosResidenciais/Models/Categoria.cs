namespace FinanceiroApi.Models
{
    public class Categoria : IEntidade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Descricao { get; set; } = string.Empty;
        public Finalidade Finalidade { get; set; }
    }

    public enum Finalidade
    {
        Despesa,
        Receita,
        Ambas
    }
}
