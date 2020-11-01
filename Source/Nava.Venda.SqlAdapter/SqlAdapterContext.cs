using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nava.Venda.SqlAdapter
{
    public class SqlAdapterContext : DbContext
    {
        public SqlAdapterContext(DbContextOptions<SqlAdapterContext> options) 
            : base(options)
        {

        }

        public DbSet<Domain.Funcionario> Funcionarios { get; set; }
        public DbSet<Domain.Item> Itens { get; set; }
        public DbSet<Domain.Venda> Vendas { get; set; }
    }
}
