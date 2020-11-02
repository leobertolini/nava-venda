using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Nava.Venda.Domain;
using Nava.Venda.SqlAdapter;
using Nava.Venda.SqlAdapter.Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Nava.Venda.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        static Startup()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<WebApiMapperProfile>();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<SqlAdapterContext>(
                opt => opt.UseInMemoryDatabase("database_vendas"));

            services.AddSwaggerGen();

            services.AddApplication();
            services.AddSqlAdapter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var sqlContext = app.ApplicationServices.GetService<SqlAdapterContext>();

            IncluirDadosDatabaseInMemory(sqlContext);
        }

        private static void IncluirDadosDatabaseInMemory(SqlAdapterContext sqlContext)
        {
            var funcionarioMock = new Funcionario()
            {
                FuncionarioId = Guid.Parse("49332671-a532-4865-a280-73c66329b753"),
                Cargo = Cargo.Vendedor,
                Cpf = "07920357652",
                Nome = "Leonardo Bertolini",
                Email = "bertolini.leo@gmail.com",
                Telefone = "99687-0094"
            };

            sqlContext.Funcionarios.Add(funcionarioMock);

            var itemMock1 = new Item()
            {
                ItemId = Guid.NewGuid(),
                Nome = "Produto 1",
                Valor = 35.90M
            };

            var itemMock2 = new Item()
            {
                ItemId = Guid.NewGuid(),
                Nome = "Produto 2",
                Valor = 99.90M
            };

            var itemMock3 = new Item()
            {
                ItemId = Guid.NewGuid(),
                Nome = "Produto 3",
                Valor = 149.90M
            };

            sqlContext.Itens.Add(itemMock1);
            sqlContext.Itens.Add(itemMock2);
            sqlContext.Itens.Add(itemMock3);

            var vendaMock1 = new Domain.Venda()
            {
                VendaId = Guid.Parse("0fd1cc41-55ab-47a8-af7e-ccc606a4a648"),
                Data = DateTime.Now,
                Status = StatusVenda.PagamentoAprovado,
                Vendedor = funcionarioMock,
                Itens = new List<Item>()
                {
                    itemMock1
                }
            };

            var vendaMock2 = new Domain.Venda()
            {
                VendaId = Guid.Parse("ebf70b0b-f275-48eb-b6d5-61eaa050b86d"),
                Data = DateTime.Now,
                Status = StatusVenda.EnviadoTransportadora,
                Vendedor = funcionarioMock,
                Itens = new List<Item>()
                {
                    itemMock2, itemMock3
                }
            };

            sqlContext.Vendas.Add(vendaMock1);
            sqlContext.Vendas.Add(vendaMock2);

            sqlContext.SaveChangesAsync();
        }
    }
}
