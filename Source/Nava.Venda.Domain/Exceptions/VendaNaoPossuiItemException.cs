using System;
using System.Runtime.Serialization;

namespace Nava.Venda.Domain.Exceptions
{
    public class VendaNaoPossuiItemException : NegocioException
    {
        public VendaNaoPossuiItemException()
        {
        }

        public VendaNaoPossuiItemException(string message) 
            : base("Venda não possui nenhum item relacionado.")
        {
        }

        public VendaNaoPossuiItemException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected VendaNaoPossuiItemException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
