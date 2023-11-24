using CadastroDeClientes.Models;
using CadastroDeClientes.Models.SubModelCliente;
using CadastroDeClientes.Models.SubModels;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<PhoneModel> Phones { get; set; }
        public DbSet<EmailModel> Emails { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<AlternativeEmailModel> AlternativeEmails { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
