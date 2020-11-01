using System;
using System.Collections.Generic;

namespace Nava.Venda.Domain
{
    public class Venda
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public Funcionario Funcionario { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public StatusVenda Status { get; set; }
    }
}
