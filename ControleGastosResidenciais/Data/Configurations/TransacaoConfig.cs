using FinanceiroApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidenciais.Data.Configurations
{
    public class TransacaoConfig : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes", t =>
                t.HasCheckConstraint("CK_Transacao_Valor", "\"Valor\" >= 0.01"));

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Descricao)
                .HasColumnName("Descricao")
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(t => t.Valor)
                .HasColumnName("Valor");

            builder.Property(t => t.Tipo)
                .HasColumnName("Tipo");

            builder.Property(t => t.CategoriaId)
                .HasColumnName("CategoriaId");

            builder.Property(t => t.PessoaId)
                .HasColumnName("PessoaId");

            builder.HasOne(t => t.Categoria)
                .WithMany()
                .HasForeignKey(t => t.CategoriaId);
        }
    }
}
