using System;
using System.Collections.Generic;

namespace Nava.Venda.Domain
{
    public class Venda
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public Funcionario Vendedor { get; set; }
        public IEnumerable<Item> Itens { get; set; }
        public StatusVenda Status { get; set; }
    }
}
