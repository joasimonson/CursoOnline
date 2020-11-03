using System;
using System.Runtime.Serialization;

namespace CursoOnline.Dominio
{
    [Serializable]
    public class NaoEhPossivelInformarNotaComMatriculaCanceladaException : Exception
    {
        public NaoEhPossivelInformarNotaComMatriculaCanceladaException()
        {
        }

        public NaoEhPossivelInformarNotaComMatriculaCanceladaException(string message) : base(message)
        {
        }

        public NaoEhPossivelInformarNotaComMatriculaCanceladaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NaoEhPossivelInformarNotaComMatriculaCanceladaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}