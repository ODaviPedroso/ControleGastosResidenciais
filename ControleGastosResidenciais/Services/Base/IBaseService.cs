using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Services.Base
{
    // Interface genérica de CRUD para qualquer entidade que implemente IEntidade.
    // Centraliza o contrato de operações básicas evitando repetição nos serviços concretos.
    public interface IBaseService<T> where T : class, IEntidade
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task<T> Criar(T entidade);
        Task<T?> Atualizar(Guid id, T entidade);
        Task<bool> Deletar(Guid id);
    }
}
