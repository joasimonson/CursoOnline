using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Util;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Extensions;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class ConclusaoMatriculaTest
    {
        private readonly Faker _faker;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly ConclusaoMatricula _conclusaoMatricula;
        private readonly Matricula _matricula;

        public ConclusaoMatriculaTest()
        {
            _faker = new Faker();

            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _conclusaoMatricula = new ConclusaoMatricula(_matriculaRepositorio.Object);

            _matricula = MatriculaBuilder.Novo().Build();
        }

        [Fact]
        public void DeveInformarNotaAluno()
        {
            decimal notaEsperada = _faker.Random.Number(Constants.NOTAMINIMA, Constants.NOTAMAXIMA);

            _matriculaRepositorio.Setup(r => r.ObterPorId(_matricula.Id)).Returns(_matricula);

            _conclusaoMatricula.Concluir(_matricula.Id, notaEsperada);

            Assert.Equal(notaEsperada, _matricula.NotaAluno);
        }

        [Fact]
        public void MatriculaDeveSerValida()
        {
            Matricula matriculaInvalida = null;
            int matriculaIdInvalida = _faker.Random.NumberPositive();
            decimal notaAluno = _faker.Random.Number(Constants.NOTAMINIMA, Constants.NOTAMAXIMA);

            _matriculaRepositorio.Setup(r => r.ObterPorId(It.IsAny<int>())).Returns(matriculaInvalida);

            Assert.Throws<RegraDominioException>(() => _conclusaoMatricula.Concluir(matriculaIdInvalida, notaAluno))
                .ValidarExcept<RegistroInexistenteException<Matricula>>();
        }
    }
}
