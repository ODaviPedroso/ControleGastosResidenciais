using ControleGastosResidenciais.Services.Categorias;
using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Controllers
{
    public class CategoriasController : BaseController<Categoria>
    {
        public CategoriasController(ICategoriaService service) : base(service)
        {
        }
    }
}
