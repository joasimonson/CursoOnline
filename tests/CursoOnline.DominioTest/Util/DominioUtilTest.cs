using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.Dominio.Util;
using CursoOnline.DominioTest.Extensions;
using Xunit;

namespace CursoOnline.DominioTest.Util
{
    public class DominioUtilTest
    {
        private readonly Faker _faker;

        public DominioUtilTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveConverterPublicoAlvo()
        {
            var publicoAlvo = _faker.Random.Enum<PublicoAlvoEnum>();

            var publicoAlvoEnum = DominioUtil.ConverterPublicoAlvo(publicoAlvo.ToString());

            Assert.Equal(publicoAlvo, publicoAlvoEnum);
        }

        [Fact]
        public void NaoDeveConverterPublicoAlvo()
        {
            var publicoAlvoInvalido = "PublicoAlvoInvalido";

            Assert.Throws<RegraDominioException>(() => DominioUtil.ConverterPublicoAlvo(publicoAlvoInvalido))
                .ValidarExcept<ParametroInvalidoException>();
        }
    }
}
