using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nava.Venda.Domain;

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
        /// <param name="identificador">Identificador da venda.</param>
        /// <returns>Venda encontrada</returns>
        [ProducesResponseType(typeof(Domain.Venda), 200)]
        [HttpGet("{identificador}"), AllowAnonymous]
        public async Task<IActionResult> GetVendaPorIdAsync(Guid identificador)
        {
            var venda = await vendaService.ObterPorIdAsync(identificador);

            //mapper venda para vendaGetResult
            
            return Ok(venda);
        }
    }
}
