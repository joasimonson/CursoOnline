using CursoOnline.Dominio;
using CursoOnline.Dominio.Exceptions;
using System;
using System.Linq;
using Xunit;

namespace CursoOnline.DominioTest.Extensions
{
    public static class AssertExtensions
    {
        public static void ValidarExcept(this ParametroInvalidoException exception, string param)
        {
            Assert.Equal(param, exception.ParamName);
        }

        public static void ValidarExcept(this RegistroDuplicadoException exception, string idDuplicado)
        {
            Assert.Equal(idDuplicado, exception.IdentificadorDuplicado);
        }

        public static void ValidarExcept<TException>(this RegraDominioException exception) where TException : Exception
        {
            var except = exception.Exceptions.FirstOrDefault(e => e.GetType() == typeof(TException));

            Assert.NotNull(except);
        }
    }
}
