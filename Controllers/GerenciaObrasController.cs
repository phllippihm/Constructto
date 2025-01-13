using Constructto.Data; 
using Constructto.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using System.Threading.Tasks; 

namespace Constructto.Controllers
{
    public class GerenciaObrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        
        public GerenciaObrasController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        // GET: GerenciaObras/ListaObras
        public async Task<IActionResult> ListaObras()
        {
            // Prepara o ViewModel com a lista de obras do banco de dados.
            var viewModel = new ObrasViewModel
            {
                ListaDeObras = await _context.Obras.ToListAsync() // Recupera todas as obras.
            };
            return View(viewModel); // Retorna a view com os dados.
        }

        // Abre a tela para criar uma nova obra.
        // GET: GerenciaObras/Create
        public IActionResult Create()
        {
            // Cria um novo ViewModel vazio.
            var viewModel = new ObrasViewModel();
            return View("RegistraObras", viewModel); // Retorna a view de registro de obras.
        }

        // Salva uma nova obra no banco de dados.
        // POST: GerenciaObras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ObrasViewModel viewModel)
        {
            if (ModelState.IsValid) // Verifica se o modelo é válido.
            {
                _context.Obras.Add(viewModel.Obras); // Adiciona a nova obra ao contexto.
                await _context.SaveChangesAsync(); // Salva as alterações no banco.
                return RedirectToAction(nameof(ListaObras)); // Redireciona para a lista de obras.
            }
            return View("RegistraObras", viewModel); 
        }

        // Abre a tela para editar uma obra existente.
        // GET: GerenciaObras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound(); // Retorna 404 se o ID for nulo.

            // Busca a obra no banco de dados, incluindo seus funcionários.
            var obra = await _context.Obras.Include(o => o.Funcionarios).FirstOrDefaultAsync(o => o.ObraId == id);
            if (obra == null) return NotFound(); // Retorna 404 se a obra não for encontrada.

            // Prepara o ViewModel com os dados da obra.
            var viewModel = new ObrasViewModel
            {
                Obras = obra
            };

            return View("RegistraObras", viewModel); // Retorna a view de edição.
        }

        // Salva as alterações feitas em uma obra existente.
        // POST: GerenciaObras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ObrasViewModel viewModel)
        {
            if (id != viewModel.Obras.ObraId) return NotFound(); // Verifica se o ID coincide.

            if (ModelState.IsValid) // Verifica se o modelo é válido.
            {
                try
                {
                    // Marca o objeto como modificado para ser atualizado no banco.
                    _context.Entry(viewModel.Obras).State = EntityState.Modified;
                    await _context.SaveChangesAsync(); // Salva as alterações no banco.
                    return RedirectToAction(nameof(ListaObras));
                }
                catch (DbUpdateConcurrencyException) // Trata conflitos de concorrência.
                {
                    if (!_context.Obras.Any(e => e.ObraId == id)) // Verifica se a obra ainda existe.
                        return NotFound();
                    throw; 
                }
            }
            return View("RegistraObras", viewModel); // Retorna a view em caso de erro.
        }

        // Abre a tela de confirmação para excluir uma obra.
        // GET: GerenciaObras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Retorna 404 se o ID for nulo.
            }

            // Busca a obra no banco de dados.
            var obra = await _context.Obras.FirstOrDefaultAsync(m => m.ObraId == id);
            if (obra == null)
            {
                return NotFound(); // Retorna 404 se a obra não for encontrada.
            }

            return View(obra); // Exibe a página de confirmação.
        }

        // Confirma a exclusão de uma obra.
        // POST: GerenciaObras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Busca a obra e seus funcionários no banco.
            var obra = await _context.Obras.Include(o => o.Funcionarios).FirstOrDefaultAsync(o => o.ObraId == id);

            if (obra == null)
            {
                return NotFound(); // Retorna 404 se a obra não for encontrada.
            }

            // Desvincula os funcionários da obra antes de excluí-la.
            foreach (var funcionario in obra.Funcionarios.ToList())
            {
                funcionario.ObraId = null;
                _context.Funcionarios.Update(funcionario);
            }

            // Remove a obra do banco.
            _context.Obras.Remove(obra);
            await _context.SaveChangesAsync(); // Salva as alterações.
            return RedirectToAction(nameof(ListaObras));
        }

        // Filtra obras dinamicamente para busca na interface.
        // GET: GerenciaObras/ListaObrasAjax
        public async Task<IActionResult> ListaObrasAjax(string searchTerm)
        {
            // Filtra as obras com base no termo de busca.
            var obras = await _context.Obras.Where(o => o.Obra.Contains(searchTerm)).ToListAsync();
            return PartialView("_ObrasTableBody", obras); // Retorna apenas o corpo da tabela.
        }
    }
}