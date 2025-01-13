using Constructto.Data; // Namespace para o contexto do banco de dados.
using Constructto.Models; // Namespace para os modelos e ViewModels.
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Constructto.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly ApplicationDbContext _context; // Contexto do banco de dados.

        public FuncionarioController(ApplicationDbContext context)
        {
            _context = context; // Injeção do contexto via construtor.
        }

        // GET: /Funcionario/Register
        [HttpGet]
        public IActionResult RegisterFuncionario()
        {
            // Popula a lista de obras no ViewBag para exibir no formulário.
            var obras = _context.Obras.ToList();
            if (obras == null || !obras.Any())
            {
                obras = new List<Obras>(); // Inicializa com lista vazia se não houver obras.
            }

            ViewBag.Obras = obras; // Passa a lista para a ViewBag.

            var model = new FuncionarioViewModel
            {
                Obras = obras // Adiciona a lista de obras ao ViewModel.
            };

            return View(model); // Retorna a View com o modelo.
        }

        // POST: /Funcionario/Register
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> RegisterFuncionario(FuncionarioViewModel model)
        {
            if (ModelState.IsValid) // Verifica se o modelo é válido.
            {
                Funcionario funcionario;

                // Verifica se é uma criação (ID = 0) ou edição.
                if (model.Id == 0)
                {
                    funcionario = new Funcionario(); // Novo funcionário.
                    _context.Funcionarios.Add(funcionario); // Adiciona ao banco.
                }
                else
                {
                    funcionario = await _context.Funcionarios.FindAsync(model.Id); // Busca o funcionário existente.
                    if (funcionario == null)
                    {
                        return NotFound(); // Retorna 404 se não encontrado.
                    }
                }

                // Preenche os dados do funcionário com os valores do modelo.
                funcionario.Nome = model.Nome;
                funcionario.CPF = model.CPF;
                funcionario.DataNascimento = model.DataNascimento;
                funcionario.Sexo = model.Sexo;
                funcionario.Telefone = model.Telefone;
                funcionario.CarteiraHabilitacao = model.CarteiraHabilitacao ? "Sim" : "Não";
                funcionario.TemFilhos = model.TemFilhos;
                funcionario.Salario = model.Salario;
                funcionario.FormaPagamento = model.FormaPagamento;
                funcionario.FrequenciaPagamento = model.FrequenciaPagamento;
                funcionario.CarteiraAssinada = model.CarteiraAssinada;
                funcionario.PrincipaisQualidades = model.Qualidades;
                funcionario.TamanhoEpis = model.TamanhoEpis;
                funcionario.Altura = model.Altura;
                funcionario.Endereco = model.Endereco;
                funcionario.ObraId = model.ObraId;

                await _context.SaveChangesAsync(); // Salva as mudanças no banco.
                return RedirectToAction("ListaFuncionarios"); // Redireciona após salvar.
            }

            // Em caso de erro, repopula a lista de obras e retorna a View.
            ViewBag.Obras = _context.Obras.ToList();
            return View(model);
        }

        // GET: /Funcionario/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Retorna 404 se o ID não for fornecido.
            }

            var funcionario = await _context.Funcionarios.FindAsync(id); // Busca o funcionário.
            if (funcionario == null)
            {
                return NotFound(); // Retorna 404 se não encontrado.
            }

            // Preenche o ViewModel com os dados do funcionário.
            var model = new FuncionarioViewModel
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                CPF = funcionario.CPF,
                DataNascimento = funcionario.DataNascimento,
                Sexo = funcionario.Sexo,
                Telefone = funcionario.Telefone,
                CarteiraHabilitacao = funcionario.CarteiraHabilitacao == "Sim",
                TemFilhos = funcionario.TemFilhos,
                Salario = funcionario.Salario,
                FormaPagamento = funcionario.FormaPagamento,
                FrequenciaPagamento = funcionario.FrequenciaPagamento,
                CarteiraAssinada = funcionario.CarteiraAssinada,
                Qualidades = funcionario.PrincipaisQualidades,
                TamanhoEpis = funcionario.TamanhoEpis,
                Altura = funcionario.Altura,
                Endereco = funcionario.Endereco,
                ObraId = funcionario.ObraId,
                Obras = _context.Obras.ToList() // Preenche o dropdown de obras.
            };

            return View("RegisterFuncionario", model); // Retorna a View com o modelo.
        }

        // POST: /Funcionario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FuncionarioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound(); // Retorna 404 se o ID não coincidir.
            }

            if (ModelState.IsValid)
            {
                var funcionario = await _context.Funcionarios.FindAsync(id); // Busca o funcionário.
                if (funcionario == null)
                {
                    return NotFound();
                }

                // Atualiza os dados do funcionário.
                funcionario.Nome = model.Nome;
                funcionario.CPF = model.CPF;
                funcionario.DataNascimento = model.DataNascimento;
                funcionario.Sexo = model.Sexo;
                funcionario.Telefone = model.Telefone;
                funcionario.CarteiraHabilitacao = model.CarteiraHabilitacao ? "Sim" : "Não";
                funcionario.TemFilhos = model.TemFilhos;
                funcionario.Salario = model.Salario;
                funcionario.FormaPagamento = model.FormaPagamento;
                funcionario.FrequenciaPagamento = model.FrequenciaPagamento;
                funcionario.CarteiraAssinada = model.CarteiraAssinada;
                funcionario.PrincipaisQualidades = model.Qualidades;
                funcionario.TamanhoEpis = model.TamanhoEpis;
                funcionario.Altura = model.Altura;
                funcionario.Endereco = model.Endereco;
                funcionario.ObraId = model.ObraId;

                try
                {
                    await _context.SaveChangesAsync(); // Salva as mudanças.
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
                    {
                        return NotFound(); // Retorna 404 se o funcionário não existir mais.
                    }
                    else
                    {
                        throw; // Repassa o erro.
                    }
                }
                return RedirectToAction(nameof(ListaFuncionarios));
            }

            // Em caso de erro, repopula a lista de obras.
            ViewBag.Obras = _context.Obras.ToList();
            return View("RegisterFuncionario", model);
        }

        // Lista todos os funcionários.
        public async Task<IActionResult> ListaFuncionarios()
        {
            var funcionarios = await _context.Funcionarios.Include(f => f.Obra).ToListAsync();
            return View(funcionarios); // Retorna os funcionários com as obras relacionadas.
        }

        // Verifica se o funcionário existe no banco.
        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }

        // Exibe a confirmação de exclusão.
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario); // Exibe a página de exclusão.
        }

        // Confirma a exclusão de um funcionário.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario); // Remove do banco.
                await _context.SaveChangesAsync(); // Salva as mudanças.
            }

            return RedirectToAction(nameof(ListaFuncionarios)); // Redireciona após a exclusão.
        }

        // Filtra os funcionários para busca AJAX.
        public async Task<IActionResult> ListaFuncionariosAjax(string searchField, string searchTerm)
        {
            var funcionarios = from f in _context.Funcionarios select f;

            // Aplica o filtro com base nos parâmetros fornecidos.
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchField)
                {
                    case "Nome":
                        funcionarios = funcionarios.Where(f => f.Nome.Contains(searchTerm));
                        break;
                    case "CPF":
                        funcionarios = funcionarios.Where(f => f.CPF.Contains(searchTerm));
                        break;
                    case "CarteiraHabilitacao":
                        var habilitacao = searchTerm.ToLower() == "sim" ? "Sim" : "Não";
                        funcionarios = funcionarios.Where(f => f.CarteiraHabilitacao == habilitacao);
                        break;
                    case "PrincipaisQualidades":
                        funcionarios = funcionarios.Where(f => f.PrincipaisQualidades.Contains(searchTerm));
                        break;
                }
            }

            // Retorna a PartialView com os resultados.
            return PartialView("_FuncionarioTableBody", await funcionarios.ToListAsync());
        }
    }
}
