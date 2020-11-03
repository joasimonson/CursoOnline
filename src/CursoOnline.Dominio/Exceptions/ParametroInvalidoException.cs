using System;

namespace CursoOnline.Dominio.Exceptions
{
    public class ParametroInvalidoException : ArgumentException
    {
        public ParametroInvalidoException(string message, string param) : base(message, param)
        {

        }

        public ParametroInvalidoException(string param) : base($"Parâmetro '{param}' inválido.", param)
        {

        }
    }
}
