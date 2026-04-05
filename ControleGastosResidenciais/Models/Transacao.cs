using System.ComponentModel.DataAnnotations;

namespace FinanceiroApi.Models
{
    public class Transacao
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
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