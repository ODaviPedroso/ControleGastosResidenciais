namespace FinanceiroApi.Models
{
    public class Pessoa : IEntidade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public List<Transacao> Transacoes { get; set; } = new();
    }
}
