using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using System;

namespace CursoOnline.Dominio.Util
{
    public static class DominioUtil
    {
        public static PublicoAlvoEnum ConverterPublicoAlvo(string publicoAlvo)
        {
            bool publicoAlvoValido = Enum.TryParse(publicoAlvo, out PublicoAlvoEnum publicoAlvoEnum);

            ValidadorRegra.Novo()
                .ComRegra(!publicoAlvoValido, () => throw new ParametroInvalidoException(nameof(publicoAlvo)))
                .Validar();

            return publicoAlvoEnum;
        }
    }
}
