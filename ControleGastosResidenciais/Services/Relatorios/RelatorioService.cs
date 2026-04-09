using ControleGastosResidenciais.Data;
using ControleGastosResidenciais.Dtos;
using FinanceiroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidenciais.Services.Relatorios
{
    public class RelatorioService : IRelatorioService
    {
        private readonly AppDbContext _context;

        public RelatorioService(AppDbContext context)
        {
            _context = context;
        }

        // Busca todas as pessoas com suas transações e calcula receitas, despesas e saldo de cada uma.
        // Ao final, soma os totais gerais de todas as pessoas.
        public async Task<TotaisResponse> GetTotaisPorPessoa()
        {
            var pessoas = await _context.Pessoas
                .Include(p => p.Transacoes)
                .ToListAsync();

            var itens = pessoas.Select(p =>
            {
                var receitas = p.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
                var despesas = p.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

                return new TotalItemDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    TotalReceitas = receitas,
                    TotalDespesas = despesas,
                    Saldo = receitas - despesas
                };
            }).ToList();

            return BuildResponse(itens);
        }

        // Busca todas as categorias e calcula receitas, despesas e saldo considerando
        // todas as transações vinculadas a cada categoria.
        public async Task<TotaisResponse> GetTotaisPorCategoria()
        {
            var categorias = await _context.Categorias.ToListAsync();
            var transacoes = await _context.Transacoes.ToListAsync();

            var itens = categorias.Select(c =>
            {
                var transacoesDaCategoria = transacoes.Where(t => t.CategoriaId == c.Id).ToList();
                var receitas = transacoesDaCategoria.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
                var despesas = transacoesDaCategoria.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);

                return new TotalItemDto
                {
                    Id = c.Id,
                    Nome = c.Descricao,
                    TotalReceitas = receitas,
                    TotalDespesas = despesas,
                    Saldo = receitas - despesas
                };
            }).ToList();

            return BuildResponse(itens);
        }

        // Monta o objeto de resposta somando os totais gerais a partir da lista de itens calculados
        private static TotaisResponse BuildResponse(List<TotalItemDto> itens)
        {
            var totalReceitas = itens.Sum(i => i.TotalReceitas);
            var totalDespesas = itens.Sum(i => i.TotalDespesas);

            return new TotaisResponse
            {
                Itens = itens,
                TotalGeralReceitas = totalReceitas,
                TotalGeralDespesas = totalDespesas,
                SaldoLiquido = totalReceitas - totalDespesas
            };
        }
    }
}
