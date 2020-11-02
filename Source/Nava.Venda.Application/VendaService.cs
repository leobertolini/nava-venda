using Nava.Venda.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> AtualizarStatusAsync(Guid identificador, StatusVenda novoStatusVenda)
        {
            var vendaParaAtualizar = await ObterPorIdAsync(identificador);

            //Obtém as configurações de transições possíveis entre os status de venda.
            var configuracaoTransicaoStatusPossivel = ObterConfiguracaoTransicoesStatus();

            //Obtém os status permitidos, baseado na configuração e no status atual da venda. 
            var transicoesPossiveis = configuracaoTransicaoStatusPossivel
                .Where(t => t.StatusAtual == vendaParaAtualizar.Status)
                .Select(r => r.StatusPermitido);

            //Verifica se o novo status para a venda é permitido.
            if (!transicoesPossiveis.Contains(novoStatusVenda))
                throw new TransicaoStatusVendaInvalidaException(vendaParaAtualizar.Status, novoStatusVenda);

            vendaParaAtualizar.Status = novoStatusVenda;

            var operacaoRealizada = await vendaSqlAdapter   
                .AtualizarStatusAsync(vendaParaAtualizar);

            return operacaoRealizada;
        }

        public Task<bool> RegistrarAsync(Domain.Venda venda)
        {
            if (venda.Itens?.Count() == 0 || venda.Itens == null)
                throw new VendaNaoPossuiItemException(venda.VendaId);
            
            return vendaSqlAdapter.RegistrarAsync(venda);
        }

        private IList<(StatusVenda StatusAtual, StatusVenda StatusPermitido)> ObterConfiguracaoTransicoesStatus()
        {
            //Configura as transições possíveis de status usando Tuplas.
            return new List<(StatusVenda StatusAtual, StatusVenda StatusPermitido)>()
            {
                (StatusVenda.AguardandoPagamento, StatusVenda.PagamentoAprovado),
                (StatusVenda.AguardandoPagamento, StatusVenda.Cancelada),
                (StatusVenda.PagamentoAprovado, StatusVenda.EnviadoTransportadora),
                (StatusVenda.PagamentoAprovado, StatusVenda.Cancelada),
                (StatusVenda.EnviadoTransportadora, StatusVenda.Entregue)
            };
        }
    }
}
