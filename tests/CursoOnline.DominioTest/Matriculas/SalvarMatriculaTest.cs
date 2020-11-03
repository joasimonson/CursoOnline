using CursoOnline.Dominio;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Extensions;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Matriculas
{
    public class SalvarMatriculaTest
    {
        private readonly Mock<ICursoRepositorio> _cursoRepositorio;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly Mock<IMatriculaRepositorio> _matriculaRepositorio;
        private readonly Matricula _matricula;
        private readonly MatriculaDTO _matriculaDTO;
        private readonly SalvarMatricula _salvarMatricula;

        public SalvarMatriculaTest()
        {
            _cursoRepositorio = new Mock<ICursoRepositorio>();
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _matriculaRepositorio = new Mock<IMatriculaRepositorio>();

            _matricula = MatriculaBuilder.Novo().Build();

            _cursoRepositorio.Setup(r => r.ObterPorId(_matricula.Curso.Id)).Returns(_matricula.Curso);
            _alunoRepositorio.Setup(r => r.ObterPorId(_matricula.Aluno.Id)).Returns(_matricula.Aluno);

            _matriculaDTO = new MatriculaDTO
            {
                AlunoId = _matricula.Aluno.Id,
                CursoId = _matricula.Curso.Id,
                ValorPago = _matricula.ValorPago
            };

            _salvarMatricula = new SalvarMatricula(_cursoRepositorio.Object, _alunoRepositorio.Object, _matriculaRepositorio.Object);
        }

        [Fact]
        public void CursoDaMatriculaDeveSerValido()
        {
            Curso cursoInvalido = null;

            _cursoRepositorio.Setup(r => r.ObterPorId(_matriculaDTO.CursoId)).Returns(cursoInvalido);

            Assert.Throws<RegraDominioException>(() => _salvarMatricula.Salvar(_matriculaDTO))
                .ValidarExcept<RegistroInexistenteException<MatriculaDTO>>();
        }

        [Fact]
        public void AlunoDaMatriculaDeveSerValido()
        {
            Aluno alunoInvalido = null;

            _alunoRepositorio.Setup(r => r.ObterPorId(_matriculaDTO.AlunoId)).Returns(alunoInvalido);

            Assert.Throws<RegraDominioException>(() => _salvarMatricula.Salvar(_matriculaDTO))
                .ValidarExcept<RegistroInexistenteException<MatriculaDTO>>();
        }

        [Fact]
        public void DeveCriarMatricula()
        {
            _salvarMatricula.Salvar(_matriculaDTO);

            _matriculaRepositorio.Verify(r => r.Salvar(
                It.Is<Matricula>(m =>
                    m.Curso == _matricula.Curso &&
                    m.Aluno == _matricula.Aluno &&
                    m.ValorPago == _matricula.ValorPago
                )
            ));
        }
    }
}
