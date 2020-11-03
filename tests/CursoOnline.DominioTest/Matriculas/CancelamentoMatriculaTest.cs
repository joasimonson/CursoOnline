using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Util;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class CancelamentoMatriculaTest
    {
        private readonly Faker _faker;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly CancelamentoMatricula _cancelamentoMatricula;
        private readonly Matricula _matricula;

        public CancelamentoMatriculaTest()
        {
            _faker = new Faker();

            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _cancelamentoMatricula = new CancelamentoMatricula(_matriculaRepositorio.Object);

            _matricula = MatriculaBuilder.Novo().Build();
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            _matriculaRepositorio.Setup(r => r.ObterPorId(_matricula.Id)).Returns(_matricula);

            _cancelamentoMatricula.Cancelar(_matricula.Id);

            Assert.True(_matricula.Cancelada);
        }

        [Fact]
        public void MatriculaDeveSerValida()
        {
            Matricula matriculaInvalida = null;
            int matriculaIdInvalida = _faker.Random.NumberPositive();

            _matriculaRepositorio.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            Assert.Throws<RegraDominioException>(() => _cancelamentoMatricula.Cancelar(matriculaIdInvalida))
                .ValidarExcept<RegistroInexistenteException<Matricula>>();
        }
    }
}
