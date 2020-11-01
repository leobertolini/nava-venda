using System;
using System.Runtime.Serialization;

namespace Nava.Venda.Domain
{
    public class TransicaoStatusVendaInvalidaException : Exception
    {
        public TransicaoStatusVendaInvalidaException()
        {
        }

        public TransicaoStatusVendaInvalidaException(StatusVenda statusAtual, StatusVenda novoStatus) 
            : base($"Transição de status inválida de {statusAtual} para {novoStatus}.")
        {
        }

        public TransicaoStatusVendaInvalidaException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected TransicaoStatusVendaInvalidaException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
