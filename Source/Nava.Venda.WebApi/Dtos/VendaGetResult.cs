using System;
using System.Collections.Generic;

namespace Nava.Venda.WebApi.Dtos
{
    public class VendaGetResult
    {
        public Guid VendaId { get; set; }
        public DateTime Data { get; set; }
        public FuncionarioDto Vendedor { get; set; }
        public IEnumerable<ItemDto> Itens { get; set; }
        public StatusVendaDto Status { get; set; }
    }
}
