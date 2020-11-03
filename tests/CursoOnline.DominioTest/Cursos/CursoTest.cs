using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Extensions;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest
    {
        private readonly string _nome;
        private readonly string _descricao;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvoEnum _publicoAlvo;
        private readonly decimal _valor;
        private readonly ITestOutputHelper _output;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;

            var faker = new Faker();

            _nome = faker.Random.Word();
            _descricao = faker.Lorem.Paragraph();
            _cargaHoraria = faker.Random.Double(50, 1000);
            _publicoAlvo = faker.Random.Enum<PublicoAlvoEnum>();
            _valor = faker.Random.Decimal(100, 1000);
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                Descricao = _descricao,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            var curso = new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<RegraDominioException>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            Assert.Throws<RegraDominioException>(() =>
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void CursoNaoDeveTerValorInvalido(decimal valorInvalido)
        {
            Assert.Throws<RegraDominioException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarNome(_nome);

            Assert.Equal(_nome, curso.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<RegraDominioException>(() =>
                curso.AlterarNome(nomeInvalido))
                .ValidarExcept<ParametroInvalidoException>();
        }
    }
}
