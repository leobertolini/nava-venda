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
        }

        public async Task<Domain.Venda> ObterPorIdAsync(Guid identificador)
        {
            return await SqlAdapterContext.Vendas
                .FirstOrDefaultAsync(v => v.Id == identificador);
        }

        public async Task<bool> AtualizarStatusAsync(Guid identificador, StatusVenda novoStatusVenda)
        {
            var venda = await SqlAdapterContext.Vendas
                .FirstOrDefaultAsync(v => v.Id == identificador);

            venda.Status = novoStatusVenda;

            SqlAdapterContext.Vendas.Update(venda);

            var quantidadeRegistrosAtualizados = await SqlAdapterContext.SaveChangesAsync();

            return (quantidadeRegistrosAtualizados > 0);
        }

        public async Task<bool> RegistrarAsync(Domain.Venda venda)
        {
            await SqlAdapterContext.Vendas.AddAsync(venda);

            var quantidadeRegistrosCriados = await SqlAdapterContext.SaveChangesAsync();

            return (quantidadeRegistrosCriados > 0);
        }
    }
}
