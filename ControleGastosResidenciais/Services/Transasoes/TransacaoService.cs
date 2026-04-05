using ControleGastosResidenciais.Data;
using FinanceiroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidenciais.Services.Transasoes
{
    public class TransacaoService : ITransacaoService
    {
        private readonly AppDbContext _context;
        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> Criar(Transacao transacao)
        {
            var pessoa = await _context.Pessoas.FindAsync(transacao.PessoaId);
            var categoria = await _context.Categorias.FindAsync(transacao.CategoriaId);

            if (pessoa == null || categoria == null)
                throw new Exception("Pessoa ou Categoria não encontrada.");

            if (pessoa.Idade < 18 && transacao.Tipo == TipoTransacao.Receita)
                throw new Exception("Menores de 18 anos só podem registrar despesas.");

            bool incompativel = (transacao.Tipo == TipoTransacao.Receita && categoria.Finalidade == Finalidade.Despesa) ||
                                (transacao.Tipo == TipoTransacao.Despesa && categoria.Finalidade == Finalidade.Receita);

            if (incompativel)
                throw new Exception("Esta categoria não permite este tipo de transação.");

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }
    }
}
