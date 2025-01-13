using Constructto.Models; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Logging; 
using System.Diagnostics; 

namespace Constructto.Controllers
{
    // Aplica autentica��o obrigat�ria para todas as a��es do controlador.
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Injeta o servi�o de logging, usado para registrar informa��es de diagn�stico ou erros.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        // GET: /Home/Index
        public IActionResult Index()
        {
            return View(); 
        }

       

        // A��o para tratar erros.
        // Exibe uma p�gina de erro personalizada com informa��es sobre a requisi��o.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Cria um modelo de erro com o ID da requisi��o atual (se dispon�vel).
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
