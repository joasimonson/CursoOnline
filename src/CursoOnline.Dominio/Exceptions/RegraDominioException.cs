using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CursoOnline.Dominio
{
    [Serializable]
    public class RegraDominioException : Exception
    {
        public List<Exception> Exceptions { get; set; }

        public RegraDominioException()
        {
        }

        public RegraDominioException(List<Exception> exceptions)
        {
            Exceptions = exceptions;
        }

        public RegraDominioException(string message) : base(message)
        {
        }

        public RegraDominioException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegraDominioException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}