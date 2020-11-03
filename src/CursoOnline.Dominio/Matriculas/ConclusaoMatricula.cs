using CursoOnline.Dominio.Builders;

namespace CursoOnline.Dominio.Matriculas
{
    public class ConclusaoMatricula
    {
        private IMatriculaRepositorio _matriculaRepositorio;

        public ConclusaoMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Concluir(int matriculaId, decimal notaAluno)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorRegra.Novo()
                .ComRegra(matricula is null, () => throw new RegistroInexistenteException<Matricula>(matricula, r => r.Id))
                .Validar();

            matricula.InformarNota(notaAluno);
        }
    }
}
