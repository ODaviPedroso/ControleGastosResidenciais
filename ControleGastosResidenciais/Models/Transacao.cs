namespace FinanceiroApi.Models
{
    public class Transacao : IEntidade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public Guid PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }
    }

    public enum TipoTransacao
    {
        Despesa,
        Receita
    }
}
