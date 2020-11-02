using Microsoft.EntityFrameworkCore;
using Nava.Venda.Domain;
using System;
using System.Threading.Tasks;

namespace Nava.Venda.SqlAdapter
{
    public class VendaSqlAdapter : IVendaSqlAdapter
    {
        private readonly SqlAdapterContext SqlAdapterContext;

        public VendaSqlAdapter(SqlAdapterContext sqlAdapterContext)
        {
            SqlAdapterContext = sqlAdapterContext ??
                throw new ArgumentNullException(nameof(sqlAdapterContext));

            InicializarSqlContext();
        }

        public async Task<Domain.Venda> ObterPorIdAsync(Guid identificador)
        {
            return await SqlAdapterContext.Vendas
                .FirstOrDefaultAsync(b => b.VendaId == identificador);
        }

        public async Task<bool> AtualizarStatusAsync(Domain.Venda venda)
        {
            SqlAdapterContext.Vendas.Update(venda);

            var quantidadeRegistrosAtualizados = await SqlAdapterContext.SaveChangesAsync();

            return (quantidadeRegistrosAtualizados > 0);
        }

        public async Task<bool> RegistrarAsync(Domain.Venda venda)
        {
            venda.VendaId = Guid.NewGuid();

            await SqlAdapterContext.Vendas.AddAsync(venda);

            var quantidadeRegistrosCriados = await SqlAdapterContext.SaveChangesAsync();

            return (quantidadeRegistrosCriados > 0);
        }

        private void InicializarSqlContext()
        {
            //Relações entre Itens e Funcionários com a Venda não estavam sendo carregadas.
            //Por esse motivo foi realizada essa inicialização.
            //TODO: Verificar relações não carregadas no EntityFramework.
            SqlAdapterContext.Itens.ToArrayAsync();
            SqlAdapterContext.Funcionarios.ToArrayAsync();
        }
    }
}
