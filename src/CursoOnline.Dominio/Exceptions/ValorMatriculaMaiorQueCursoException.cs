using System;
using System.Runtime.Serialization;

namespace CursoOnline.Dominio
{
    [Serializable]
    public class ValorMatriculaMaiorQueCursoException : Exception
    {
        public ValorMatriculaMaiorQueCursoException() : base("Valor pago maior que valor do curso")
        {
        }

        public ValorMatriculaMaiorQueCursoException(string message) : base(message)
        {
        }

        public ValorMatriculaMaiorQueCursoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValorMatriculaMaiorQueCursoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}