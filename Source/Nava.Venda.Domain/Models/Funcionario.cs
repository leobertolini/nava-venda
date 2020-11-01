using System;

namespace Nava.Venda.Domain
{
    public class Funcionario
    {
        public Guid FuncionarioId { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Cargo Cargo { get; set; }
    }
}
