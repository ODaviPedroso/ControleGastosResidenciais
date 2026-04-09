using FinanceiroApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidenciais.Data.Configurations
{
    public class PessoaConfig : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas", t =>
                t.HasCheckConstraint("CK_Pessoa_Idade", "\"Idade\" >= 0 AND \"Idade\" <= 150"));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Idade)
                .HasColumnName("Idade");

            builder.HasMany(p => p.Transacoes)
                .WithOne(t => t.Pessoa)
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
