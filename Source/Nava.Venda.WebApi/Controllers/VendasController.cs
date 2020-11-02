using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nava.Venda.Domain;
using Nava.Venda.WebApi.Dtos;

namespace Nava.Venda.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IVendaService vendaService;

        public VendasController(IVendaService vendaService)
        {
            this.vendaService = vendaService ??
                throw new ArgumentNullException(nameof(vendaService));
        }

        /// <summary>
        /// Obtém uma venda pelo seu identificador.
        /// </summary>
        /// <param name="identificadorVenda">Identificador da venda.</param>
        /// <returns>Venda encontrada.</returns>
        [ProducesResponseType(typeof(VendaGetResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("{identificador}"), AllowAnonymous]
        public async Task<IActionResult> ObterVendaPorIdAsync(Guid identificador)
        {
            try
            {
                var venda = await vendaService.ObterPorIdAsync(identificador);
                
                var vendaGetResult = Mapper.Map<VendaGetResult>(venda);
                
                return Ok(vendaGetResult);
            }
            catch (NegocioException e)
            {
                return BadRequest(new
                {
                    MensagemErro = e.Message
                });
            }
        }

        /// <summary>
        /// Atualiza o status de uma venda.
        /// </summary>
        /// <param name="identificadorVenda">Identificador da venda.</param>
        /// <param name="novoStatusVenda">Novo status da venda.</param>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPatch("{identificador}"), AllowAnonymous]
        public async Task<IActionResult> AtualizarVendaAsync(Guid identificador, 
            StatusVendaDto novoStatusVendaDto)
        {
            var novoStatusVenda = Mapper.Map<StatusVenda>(novoStatusVendaDto);

            try
            {
                await vendaService.AtualizarStatusAsync(identificador, novoStatusVenda);
            }
            catch (NegocioException e)
            {
                return BadRequest(new 
                { 
                    MensagemErro = e.Message 
                });
            }

            return Ok();
        }

        /// <summary>
        /// Registra uma nova venda.
        /// </summary>
        /// <param name="venda">Venda a ser registrada.</param>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> RegistrarVendaAsync(VendaPost vendaPost)
        {
            var venda = Mapper.Map<Domain.Venda>(vendaPost);

            try
            { 
                await vendaService.RegistrarAsync(venda);
            }
            catch (NegocioException e)
            {
                return BadRequest(new
                {
                    MensagemErro = e.Message
                });
            }

            return Ok();
        }
    }
}
