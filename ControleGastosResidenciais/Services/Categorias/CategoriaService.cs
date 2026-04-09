using ControleGastosResidenciais.Data;
using ControleGastosResidenciais.Services.Base;
using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Services.Categorias
{
    public class CategoriaService : BaseService<Categoria>, ICategoriaService
    {
        public CategoriaService(AppDbContext context) : base(context)
        {
        }
    }
}
