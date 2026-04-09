using ControleGastosResidenciais.Data;
using ControleGastosResidenciais.Services.Base;
using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Services.Transasoes
{
    public class TransacaoService : BaseService<Transacao>, ITransacaoService
    {
        public TransacaoService(AppDbContext context) : base(context)
        {
        }

        // Sobrescreve o Criar padrão para aplicar as regras de negócio antes de persistir
        public override async Task<Transacao> Criar(Transacao transacao)
        {
            var pessoa = await _context.Pessoas.FindAsync(transacao.PessoaId);
            var categoria = await _context.Categorias.FindAsync(transacao.CategoriaId);

            if (pessoa == null || categoria == null)
                throw new Exception("Pessoa ou Categoria não encontrada.");

            // Regra: menores de 18 anos só podem registrar despesas
            if (pessoa.Idade < 18 && transacao.Tipo == TipoTransacao.Receita)
                throw new Exception("Menores de 18 anos só podem registrar despesas.");

            // Regra: a finalidade da categoria deve ser compatível com o tipo da transação
            bool incompativel = (transacao.Tipo == TipoTransacao.Receita && categoria.Finalidade == Finalidade.Despesa) ||
                                (transacao.Tipo == TipoTransacao.Despesa && categoria.Finalidade == Finalidade.Receita);

            if (incompativel)
                throw new Exception("Esta categoria não permite este tipo de transação.");

            return await base.Criar(transacao);
        }
    }
}
