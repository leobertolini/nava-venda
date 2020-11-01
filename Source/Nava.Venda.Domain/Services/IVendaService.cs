using System;
using System.Threading.Tasks;

namespace Nava.Venda.Domain
{
    public interface IVendaService
    {
        /// <summary>
        /// Obtém uma venda a partir do seu identificador.
        /// </summary>
        /// <param name="identificador">identificador da venda.</param>
        /// <returns>Venda obtida.</returns>
        Task<Venda> ObterPorIdAsync(Guid identificador);

        /// <summary>
        /// Registra uma nova venda.
        /// </summary>
        /// <param name="venda">Venda a ser registrada.</param>
        /// <returns>Valor booleano indicando o sucesso da operação.</returns>
        Task<bool> RegistrarAsync(Venda venda);

        /// <summary>
        /// Atualiza uma venda existente.
        /// </summary>
        /// <param name="identificador">Identificador da venda a ser atualizada.</param>
        /// <param name="novoStatusVenda">Novo status a ser atribuído à venda.</param>
        /// <returns>Valor booleano indicando o sucesso da operação.</returns>
        Task<bool> AtualizarStatusAsync(Guid identificador, StatusVenda novoStatusVenda);
    }
}
