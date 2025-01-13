using Constructto.Data;
using Constructto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Constructto.Controllers
{
    public class ObrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Exibir a página de gerenciamento de funcionários
        public async Task<IActionResult> GerenciarFuncionarios()
        {
            var obras = await _context.Obras.Include(o => o.Funcionarios).ToListAsync();
            var funcionariosDisponiveis = await _context.Funcionarios
                .Where(f => f.ObraId == null)
                .ToListAsync();

            var model = new ObrasViewModel
            {
                ListaDeObras = obras, // Preenche a lista de obras
                Disponiveis = funcionariosDisponiveis // Preenche a lista de funcionários disponíveis
            };

            return View(model); // Passa o modelo para a view
        }

        // Alocar funcionário a uma obra
        [HttpPost]
        public async Task<IActionResult> AllocateFuncionario(int obraId, int funcionarioId)
        {
            var funcionario = await _context.Funcionarios.FindAsync(funcionarioId);

            if (funcionario != null)
            {
                funcionario.ObraId = obraId;
                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        // Remover funcionário de uma obra
        [HttpPost]
        public async Task<IActionResult> RemoveFuncionario(int obraId, int funcionarioId)
        {
            var funcionario = await _context.Funcionarios.FindAsync(funcionarioId);
            if (funcionario != null && funcionario.ObraId == obraId)
            {
                funcionario.ObraId = null;
                _context.Update(funcionario);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
