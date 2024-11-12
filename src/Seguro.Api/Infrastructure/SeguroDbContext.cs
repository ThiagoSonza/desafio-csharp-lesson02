using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Seguro.Api.Domain.Proposta.Model;

namespace Seguro.Api.Infrastructure
{
    public class SeguroDbContext(DbContextOptions<SeguroDbContext> options) : DbContext(options)
    {
        public DbSet<PropostaDominio> Propostas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(500);
        }
    }
}