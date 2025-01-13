using Constructto.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Diagnostics;

var defaultCulture = new CultureInfo("pt-BR"); 
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do banco de dados e do Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura��o do ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();


// Configura��o das op��es de cookies de autentica��o
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

// Adiciona servi�os de controle e visualiza��es ao cont�iner
builder.Services.AddControllersWithViews();

var app = builder.Build();



// Configura��o do pipeline de requisi��o HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Configura��o do mapeamento das rotas dos controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "GerenciaObras",
    pattern: "{controller=GerenciaObras}/{action=ListaObras}/{id?}");

app.MapControllerRoute(
    name: "estoque",
    pattern: "{controller=Estoque}/{action=ListaEstoque}/{id?}");

app.MapControllerRoute(
    name: "funcionario",
    pattern: "{controller=Funcionario}/{action=ListaFuncionarios}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        // Tentar fazer uma consulta simples para verificar a conex�o
        dbContext.Database.CanConnect();
        Console.WriteLine("Conex�o com o banco de dados bem-sucedida!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao conectar com o banco de dados: " + ex.Message);
    }
}

app.Run();
