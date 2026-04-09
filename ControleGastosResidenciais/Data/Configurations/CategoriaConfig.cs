using FinanceiroApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidenciais.Data.Configurations
{
    public class CategoriaConfig : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Descricao)
                .HasColumnName("Descricao")
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(c => c.Finalidade)
                .HasColumnName("Finalidade");
        }
    }
}
