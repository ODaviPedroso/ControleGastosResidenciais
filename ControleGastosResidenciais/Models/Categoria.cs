using System.ComponentModel.DataAnnotations;

namespace FinanceiroApi.Models
{
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(400)]
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

