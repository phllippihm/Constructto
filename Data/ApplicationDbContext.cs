using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Constructto.Models;
using Microsoft.AspNetCore.Identity;

namespace Constructto.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

     
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Obras> Obras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do relacionamento entre Funcionario e Obras um para muitos
            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.Obra) // Um Funcionario tem uma Obra
                .WithMany(o => o.Funcionarios) // Uma Obra tem muitos Funcionarios
                .HasForeignKey(f => f.ObraId) // Chave estrangeira no Funcionario
                .OnDelete(DeleteBehavior.SetNull); // Ao excluir uma Obra, o ObraId no Funcionario será definido como nulo
        }
    }
}
