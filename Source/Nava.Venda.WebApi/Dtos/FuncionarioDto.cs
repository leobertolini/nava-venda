using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nava.Venda.WebApi.Dtos
{
    public class FuncionarioDto
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public CargoDto Cargo { get; set; }
    }
}
