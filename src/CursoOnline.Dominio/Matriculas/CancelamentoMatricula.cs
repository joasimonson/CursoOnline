using CursoOnline.Dominio.Builders;

namespace CursoOnline.Dominio.Matriculas
{
    public class CancelamentoMatricula
    {
        private IMatriculaRepositorio _matriculaRepositorio;

        public CancelamentoMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            this._matriculaRepositorio = matriculaRepositorio;
        }

        public void Cancelar(int matriculaId)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorRegra.Novo()
                .ComRegra(matricula is null, () => throw new RegistroInexistenteException<Matricula>(matricula, r => r.Id))
                .Validar();

            matricula.Cancelar();
        }
    }
}
