using System;
using System.Runtime.Serialization;

namespace CursoOnline.Dominio
{
    [Serializable]
    public class NaoEhPossivelCancelarComCursoConcluidoException : Exception
    {
        public NaoEhPossivelCancelarComCursoConcluidoException()
        {
        }

        public NaoEhPossivelCancelarComCursoConcluidoException(string message) : base(message)
        {
        }

        public NaoEhPossivelCancelarComCursoConcluidoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NaoEhPossivelCancelarComCursoConcluidoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}