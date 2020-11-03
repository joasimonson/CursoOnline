using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Util;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Extensions;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class MatriculaTest
    {
        private readonly Faker _faker;
        private readonly Curso _curso;
        private readonly Aluno _aluno;

        public MatriculaTest()
        {
            _faker = new Faker();

            var publicoAlvo = _faker.Random.Enum<PublicoAlvoEnum>();

            _curso = CursoBuilder.Novo().ComPublicoAlvo(publicoAlvo).Build();
            _aluno = AlunoBuilder.Novo().ComPublicoAlvo(publicoAlvo).Build();
        }

        [Fact]
        public void DeveCriarMatricula()
        {
            var matriculaEsperada = new
            {
                Aluno = _aluno,
                Curso = _curso,
                ValorPago = _curso.Valor
            };

            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            Aluno alunoInvalido = null;

            Assert.Throws<RegraDominioException>(() => MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            Curso cursoInvalido = null;

            Assert.Throws<RegraDominioException>(() => MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-500.75)]
        [InlineData(-1000)]
        public void NaoDeveCriarMatriculaComValorInvalido(decimal valorInvalido)
        {
            Assert.Throws<RegraDominioException>(() => MatriculaBuilder.Novo().ComValorPago(valorInvalido).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorMaiorQueValorDoCurso()
        {
            decimal valorMaiorQueValorDoCurso = _curso.Valor + 1;

            Assert.Throws<RegraDominioException>(() => MatriculaBuilder.Novo()
                .ComCurso(_curso)
                .ComAluno(_aluno)
                .ComValorPago(valorMaiorQueValorDoCurso)
                .Build())
                    .ValidarExcept<ValorMatriculaMaiorQueCursoException>();
        }

        [Fact]
        public void DeveIndicarQueHouveDescontoNaMatricula()
        {
            decimal valorPagoComDesconto = _curso.Valor - 1;

            var matricula = MatriculaBuilder.Novo()
                .ComCurso(_curso)
                .ComAluno(_aluno)
                .ComValorPago(valorPagoComDesconto)
                .Build();

            Assert.True(matricula.TemDesconto);
        }

        [Fact]
        public void PublicoAlvoAlunoECursoNaoPodemSerDiferentes()
        {
            var publicoAlvo = _faker.Random.Enum<PublicoAlvoEnum>();
            var publicoAlvoDiferente = _faker.Random.Enum(publicoAlvo);

            var curso = CursoBuilder.Novo().ComPublicoAlvo(publicoAlvo).Build();
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(publicoAlvoDiferente).Build();

            Assert.Throws<RegraDominioException>(() => MatriculaBuilder.Novo()
                .ComCurso(curso)
                .ComAluno(aluno)
                .Build())
                    .ValidarExcept<PublicoAlvoCursoEAlunoDiferenteException>();
        }

        [Fact]
        public void DeveInformarNotaValidaParaMatricula()
        {
            decimal notaEsperada = _faker.Random.Decimal(Constants.NOTAMINIMA, Constants.NOTAMAXIMA);

            var matricula = MatriculaBuilder.Novo().Build();

            matricula.InformarNota(notaEsperada);

            Assert.True(matricula.CursoConcluido);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void NaoDeveInformarNotaInvalida(decimal notaInvalida)
        {
            var matricula = MatriculaBuilder.Novo().Build();

            Assert.Throws<RegraDominioException>(() => matricula.InformarNota(notaInvalida))
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.Cancelar();

            Assert.True(matricula.Cancelada);
        }

        [Fact]
        public void NaoDeveInformarNotaQuandoMatriculaCancelada()
        {
            decimal notaAluno = _faker.Random.Decimal(Constants.NOTAMINIMA, Constants.NOTAMAXIMA);
            bool matriculaCancelada = true;

            var matricula = MatriculaBuilder.Novo().ComCancelada(matriculaCancelada).Build();

            Assert.Throws<RegraDominioException>(() => matricula.InformarNota(notaAluno))
                .ValidarExcept<NaoEhPossivelInformarNotaComMatriculaCanceladaException>();
        }

        [Fact]
        public void NaoDeveCancelarQuandoMatriculaConcluida()
        {
            bool cursoConcluido = true;

            var matricula = MatriculaBuilder.Novo().ComCursoConcluido(cursoConcluido).Build();

            Assert.Throws<RegraDominioException>(() => matricula.Cancelar())
                .ValidarExcept<NaoEhPossivelCancelarComCursoConcluidoException>();
        }
    }
}
