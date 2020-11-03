using CursoOnline.Data.Contexts;
using CursoOnline.Dominio.Cursos;
using System.Linq;

namespace CursoOnline.Data.Repositorios
{
    public class CursoRepositorio : RepositorioBase<Curso>, ICursoRepositorio
    {
        public CursoRepositorio(AppDbContext context) : base(context)
        {
        }

        public Curso ObterPeloNome(string nome)
        {
            var curso = Context.Set<Curso>().FirstOrDefault(c => c.Nome.Contains(nome));
            return curso;
        }
    }
}
