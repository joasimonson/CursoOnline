using System;
using System.Runtime.Serialization;

namespace CursoOnline.Dominio
{
    public class PublicoAlvoCursoEAlunoDiferenteException : Exception
    {
        public PublicoAlvoCursoEAlunoDiferenteException() : base("Público alvo do curso e do aluno não podem ser diferentes")
        {
        }

        public PublicoAlvoCursoEAlunoDiferenteException(string message) : base(message)
        {
        }

        public PublicoAlvoCursoEAlunoDiferenteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PublicoAlvoCursoEAlunoDiferenteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}