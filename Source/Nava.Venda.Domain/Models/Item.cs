using System;

namespace Nava.Venda.Domain
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
