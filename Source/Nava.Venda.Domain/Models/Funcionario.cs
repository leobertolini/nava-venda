using System;

namespace Nava.Venda.Domain
{
    public class Funcionario
    {
        public Guid Id { get; set; }
        public int Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Cargo Cargo { get; set; }
    }
}
