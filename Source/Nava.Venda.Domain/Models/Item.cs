using System;

namespace Nava.Venda.Domain
{
    public class Item
    {
        public Guid ItemId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
