using Nava.Venda.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Nava.Venda.Application
{
    public class VendaService : IVendaService
    {
        private readonly IVendaSqlAdapter vendaSqlAdapter;

        public VendaService(IVendaSqlAdapter vendaSqlAdapter)
        {
            this.vendaSqlAdapter = vendaSqlAdapter ??
                throw new ArgumentNullException(nameof(vendaSqlAdapter));
        }

        public Task<Domain.Venda> ObterPorIdAsync(Guid identificador)
        {
            return vendaSqlAdapter.ObterPorIdAsync(identificador);
        }

        public Task<Guid> AtualizarStatusAsync(Guid identificador, StatusVenda novoStatusVenda)
        {
            return vendaSqlAdapter.AtualizarStatusAsync(identificador, novoStatusVenda);
        }

        public Task<Guid> RegistrarAsync(Domain.Venda venda)
        {
            return vendaSqlAdapter.RegistrarAsync(venda);
        }
    }
}
