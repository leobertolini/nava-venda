using Nava.Venda.Domain;
using System;
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

        public Task<bool> AtualizarStatusAsync(Guid identificador, StatusVenda novoStatusVenda)
        {
            return vendaSqlAdapter.AtualizarStatusAsync(identificador, novoStatusVenda);
        }

        public Task<bool> RegistrarAsync(Domain.Venda venda)
        {
            return vendaSqlAdapter.RegistrarAsync(venda);
        }
    }
}
