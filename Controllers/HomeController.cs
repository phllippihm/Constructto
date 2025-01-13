using Constructto.Models; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Logging; 
using System.Diagnostics; 

namespace Constructto.Controllers
{
    // Aplica autenticação obrigatória para todas as ações do controlador.
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Injeta o serviço de logging, usado para registrar informações de diagnóstico ou erros.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        // GET: /Home/Index
        public IActionResult Index()
        {
            return View(); 
        }

       

        // Ação para tratar erros.
        // Exibe uma página de erro personalizada com informações sobre a requisição.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Cria um modelo de erro com o ID da requisição atual (se disponível).
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
