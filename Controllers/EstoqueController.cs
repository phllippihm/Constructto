using Constructto.Data; 
using Constructto.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Constructto.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext _context; // Contexto do banco de dados.

        // Construtor que injeta o contexto do banco.
        public EstoqueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista todos os produtos no estoque.
        public async Task<IActionResult> ListaEstoque()
        {
            // Obtém todos os produtos do banco de dados.
            var produtos = await _context.Estoque.ToListAsync();
            return View(produtos); // Retorna a lista para a View.
        }

        // Pesquisa dinâmica no estoque (usada por AJAX).
        public async Task<IActionResult> ListaEstoqueAjax(string searchField, string searchTerm)
        {
            var produtos = from p in _context.Estoque select p; // Inicializa a query.

            // Filtro baseado no campo e termo de pesquisa.
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchField)
                {
                    case "NomeProduto":
                        produtos = produtos.Where(p => p.NomeProduto.Contains(searchTerm)); // Filtra pelo nome.
                        break;
                    case "Fornecedor":
                        produtos = produtos.Where(p => p.Fornecedor.Contains(searchTerm)); // Filtra pelo fornecedor.
                        break;
                    case "Preco":
                        if (decimal.TryParse(searchTerm, out decimal preco))
                        {
                            produtos = produtos.Where(p => p.Preco == preco); // Filtra pelo preço.
                        }
                        break;
                }
            }

            // Retorna a PartialView com os resultados.
            return PartialView("_EstoqueTableBody", await produtos.ToListAsync());
        }

        // Carrega a view de registro (criação ou edição) de produto.
        [HttpGet]
        public async Task<IActionResult> RegistraProduto(int? id)
        {
            if (id == null)
            {
                // Caso seja um novo produto.
                return View(new EstoqueViewModel());
            }

            // Busca o produto no banco.
            var produto = await _context.Estoque.FindAsync(id);
            if (produto == null)
            {
                return NotFound(); // Retorna 404 se não encontrado.
            }

            // Preenche o ViewModel com os dados do produto para edição.
            var model = new EstoqueViewModel
            {
                Id = produto.Id,
                NomeProduto = produto.NomeProduto,
                Quantidade = produto.Quantidade,
                Preco = produto.Preco,
                Fornecedor = produto.Fornecedor,
                DataDeEntrada = produto.DataDeEntrada
            };

            return View(model);
        }

        // Salva um novo produto ou edita um existente.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistraProduto(EstoqueViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Mapeia os dados do ViewModel para o modelo do banco.
                var produto = new Estoque
                {
                    Id = model.Id, // Se for 0, será um novo registro.
                    NomeProduto = model.NomeProduto,
                    Quantidade = model.Quantidade,
                    Preco = model.Preco,
                    Fornecedor = model.Fornecedor,
                    DataDeEntrada = model.DataDeEntrada
                };

                if (produto.Id == 0)
                {
                    _context.Estoque.Add(produto); // Adiciona um novo produto.
                }
                else
                {
                    _context.Estoque.Update(produto); // Atualiza um produto existente.
                }

                await _context.SaveChangesAsync(); // Salva as mudanças.
                return RedirectToAction(nameof(ListaEstoque));
            }

            return View(model); // Retorna a view se houver erro na validação.
        }

        // Carrega a view de exclusão de produto.
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Retorna 404 se o ID não for fornecido.
            }

            var produto = await _context.Estoque.FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound(); // Retorna 404 se o produto não for encontrado.
            }

            return View(produto); // Retorna os dados do produto para confirmação.
        }

        // Exclui um produto do banco de dados.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Estoque.FindAsync(id);
            _context.Estoque.Remove(produto); // Remove o produto.
            await _context.SaveChangesAsync(); // Salva as mudanças.
            return RedirectToAction(nameof(ListaEstoque));
        }

        // Duplica um produto com base no ID fornecido.
        [HttpGet]
        public async Task<IActionResult> DuplicarProduto(int id)
        {
            var produto = await _context.Estoque.FindAsync(id); // Busca o produto.
            if (produto == null)
            {
                return NotFound(); // Retorna 404 se não encontrado.
            }

            // Cria um novo ViewModel com os dados do produto.
            var model = new EstoqueViewModel
            {
                NomeProduto = produto.NomeProduto,
                Quantidade = produto.Quantidade,
                Preco = produto.Preco,
                Fornecedor = produto.Fornecedor,
                DataDeEntrada = produto.DataDeEntrada
            };

            return View("RegistraProduto", model); // Redireciona para a tela de registro.
        }
    }
}
