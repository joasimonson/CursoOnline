using CursoOnline.Dominio.Base;

namespace CursoOnline.Dominio.Cursos
{
    public interface ICursoRepositorio : IRepositorioBase<Curso>
    {
        Curso ObterPeloNome(string nome);
    }
}
