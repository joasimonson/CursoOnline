using System;

namespace CursoOnline.Dominio.Exceptions
{
    public class RegistroDuplicadoException : Exception
    {
        public readonly string IdentificadorDuplicado;

        public RegistroDuplicadoException(string message, string idDuplicado) : base(message)
        {
            IdentificadorDuplicado = idDuplicado;
        }

        public RegistroDuplicadoException(string idDuplicado) : base($"Registro '{idDuplicado}' já cadastrado.")
        {
            IdentificadorDuplicado = idDuplicado;
        }

        public RegistroDuplicadoException()
        {
        }

        public RegistroDuplicadoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}