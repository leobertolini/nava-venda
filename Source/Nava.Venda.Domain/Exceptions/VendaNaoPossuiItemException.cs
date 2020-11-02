using System;
using System.Runtime.Serialization;

namespace Nava.Venda.Domain
{
    public class VendaNaoPossuiItemException : NegocioException
    {
        public VendaNaoPossuiItemException()
        {
        }

        public VendaNaoPossuiItemException(Guid identificadorVenda) 
            : base($"Venda não possui nenhum item relacionado. VendaId: {identificadorVenda}")
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
