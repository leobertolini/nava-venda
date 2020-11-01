using Microsoft.EntityFrameworkCore;
using Nava.Venda.Domain;

namespace Nava.Venda.SqlAdapter
{
    public class SqlAdapterContext : DbContext
    {
        public SqlAdapterContext(DbContextOptions<SqlAdapterContext> options) 
            : base(options)
        {

        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Domain.Venda> Vendas { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}
