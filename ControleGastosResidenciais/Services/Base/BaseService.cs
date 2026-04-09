using ControleGastosResidenciais.Data;
using FinanceiroApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidenciais.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : class, IEntidade
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseService(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Criar(T entidade)
        {
            _dbSet.Add(entidade);
            await _context.SaveChangesAsync();
            return entidade;
        }

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
