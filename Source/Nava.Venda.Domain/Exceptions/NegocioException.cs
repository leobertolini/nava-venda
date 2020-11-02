using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Nava.Venda.Domain
{
    public class NegocioException : Exception
    {
        public NegocioException()
        {
        }

        public NegocioException(string message) : base(message)
        {
        }

        public NegocioException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NegocioException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
