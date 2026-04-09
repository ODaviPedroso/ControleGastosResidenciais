using ControleGastosResidenciais.Data;
using FinanceiroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidenciais.Services.Base
{
    // Implementação genérica de CRUD usando Entity Framework Core.
    // Serviços concretos herdam esta classe e podem sobrescrever métodos para adicionar regras de negócio.
    public class BaseService<T> : IBaseService<T> where T : class, IEntidade
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseService(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Retorna todos os registros da tabela correspondente ao tipo T
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        // Busca um registro pelo Id; retorna null se não encontrado
        public async Task<T?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Persiste uma nova entidade no banco. Pode ser sobrescrito para adicionar validações
        public virtual async Task<T> Criar(T entidade)
        {
            _dbSet.Add(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

        // Atualiza os valores de uma entidade existente pelo Id.
        // Usa SetValues para copiar apenas as propriedades escalares, preservando o rastreamento do EF
        public async Task<T?> Atualizar(Guid id, T entidade)
        {
            var existente = await _dbSet.FindAsync(id);
            if (existente == null)
                return null;

            entidade.Id = id;
            _context.Entry(existente).CurrentValues.SetValues(entidade);
            await _context.SaveChangesAsync();
            return existente;
        }

        // Remove uma entidade pelo Id; retorna false se o registro não existir
        public async Task<bool> Deletar(Guid id)
        {
            var entidade = await _dbSet.FindAsync(id);
            if (entidade == null)
                return false;

            _dbSet.Remove(entidade);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
