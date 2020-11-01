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

        public async Task<Domain.Venda> ObterPorIdAsync(Guid identificador)
        {
            var venda = await vendaSqlAdapter.ObterPorIdAsync(identificador);

            if (venda == null)
                throw new VendaNaoEncontradaException(identificador);

            return venda;
        }

        public async Task<bool> AtualizarStatusAsync(Guid identificador, StatusVenda novaStatusVenda)
        {
            var vendaParaAtualizar = await ObterPorIdAsync(identificador);

            //TODO: tratar regra de alteração de status
            vendaParaAtualizar.Status = novaStatusVenda;

            if (false)
                throw new TransicaoStatusVendaInvalidaException(vendaParaAtualizar.Status, novaStatusVenda);

            var operacaoRealizada = await vendaSqlAdapter
                .AtualizarStatusAsync(vendaParaAtualizar);

            return operacaoRealizada;
        }

        public Task<bool> RegistrarAsync(Domain.Venda venda)
        {
            return vendaSqlAdapter.RegistrarAsync(venda);
        }
    }
}
