using CadastroDeClientes.Models;
using CadastroDeClientes.Models.SubModels;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ClientModel>().HasMany(client => client.Addresses).WithOne(adress => adress.Client).HasForeignKey(fkey => fkey.ClientModelId).HasPrincipalKey(key => key.Id);
            //modelBuilder.Entity<ClientModel>().HasMany(phones => phones.Phones).WithOne(client => client.Client).HasForeignKey(key => key.ClientModelId).HasPrincipalKey(key => key.Id);
            //modelBuilder.Entity<ClientModel>().HasMany(emails => emails.Emails).WithOne(client => client.Client).HasForeignKey(key => key.ClientModelId).HasPrincipalKey(key => key.Id);
        }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<PhoneModel> Phones { get; set; }
        public DbSet<EmailModel> Emails { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
