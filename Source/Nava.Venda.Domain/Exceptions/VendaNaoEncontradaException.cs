using System;
using System.Runtime.Serialization;

namespace Nava.Venda.Domain
{
    public class VendaNaoEncontradaException : Exception
    {
        public VendaNaoEncontradaException()
        {
        }

        public VendaNaoEncontradaException(Guid identificador) 
            : base($"Venda não encontrada. Id: {identificador}")
        {
        }

        public VendaNaoEncontradaException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected VendaNaoEncontradaException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
