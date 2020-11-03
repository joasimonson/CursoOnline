using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class SalvarMatricula
    {
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IMatriculaRepositorio _matriculaRepositorio;

        public SalvarMatricula(ICursoRepositorio cursoRepositorio, IAlunoRepositorio alunoRepositorio, IMatriculaRepositorio matriculaRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
            _alunoRepositorio = alunoRepositorio;
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Salvar(MatriculaDTO matriculaDTO)
        {
            var curso = _cursoRepositorio.ObterPorId(matriculaDTO.CursoId);
            var aluno = _alunoRepositorio.ObterPorId(matriculaDTO.AlunoId);

            ValidadorRegra.Novo()
                .ComRegra(curso is null, () => throw new RegistroInexistenteException<MatriculaDTO>(matriculaDTO, r => r.CursoId))
                .ComRegra(aluno is null, () => throw new RegistroInexistenteException<MatriculaDTO>(matriculaDTO, r => r.AlunoId))
                .Validar();

            var matricula = new Matricula(aluno, curso, matriculaDTO.ValorPago);

            _matriculaRepositorio.Salvar(matricula);
        }
    }
}
