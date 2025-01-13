using Constructto.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Constructto.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager; // Gerencia os usuários, incluindo criação, busca e validação
        private readonly SignInManager<IdentityUser> _signInManager; // Gerencia a autenticação e sessões de login

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager; // Injeta o serviço de gerenciamento de usuários
            _signInManager = signInManager; // Injeta o serviço de gerenciamento de login
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Layout"] = "_LayoutLogin"; // Define o layout para a página de login
            return View(); // Renderiza a página de login
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) // Verifica se o modelo recebido é válido
            {
                // Verificar se o usuário existe
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Usuário não existe."); // Adiciona mensagem de erro caso o usuário não seja encontrado
                    return View(model); // Retorna à view com o erro
                }

                // Verificar se a senha está correta
                var result = await _signInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    model.RememberMe, // Indica se a autenticação será persistente
                    lockoutOnFailure: false // Evita o bloqueio do usuário após falhas repetidas
                );

                if (result.Succeeded) // Caso o login seja bem-sucedido
                {
                    return RedirectToAction("Index", "Home"); // Redireciona para a página inicial
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Senha incorreta."); // Adiciona mensagem de erro se a senha estiver errada
                }
            }

            // Se o ModelState não for válido, ou se a autenticação falhar, retorna à View com os erros
            ViewData["Layout"] = "_LayoutLogin"; // Certifica-se de usar o layout correto
            return View(model); // Renderiza novamente a view com as mensagens de erro
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Layout"] = "_LayoutLogin"; // Define o layout para a página de registro
            return View(); // Renderiza a página de registro
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistroViewModel model)
        {
            if (ModelState.IsValid) // Verifica se os dados enviados no modelo são válidos
            {
                // Cria um novo usuário usando os dados fornecidos no modelo
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password); // Registra o usuário no banco de dados

                if (result.Succeeded) // Caso o registro seja bem-sucedido
                {
                    await _signInManager.SignInAsync(user, isPersistent: false); // Realiza o login automático
                    return RedirectToAction("Index", "Home"); // Redireciona para a página inicial
                }
                else
                {
                    // Adiciona mensagens de erro para cada problema encontrado durante o registro
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            ViewData["Layout"] = "_LayoutLogin"; // Certifica-se de usar o layout correto
            return View(model); // Renderiza novamente a view de registro com os erros
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Realiza o logout do usuário atual
            return RedirectToAction("Login", "Account"); // Redireciona para a página de login
        }
    }
}
