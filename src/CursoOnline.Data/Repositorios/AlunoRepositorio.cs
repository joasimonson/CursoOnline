using CursoOnline.Data.Contexts;
using CursoOnline.Dominio.Alunos;
using System.Linq;

namespace CursoOnline.Data.Repositorios
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {
        public AlunoRepositorio(AppDbContext context) : base(context)
        {
        }

        public Aluno ObterPorCPF(string cpf)
        {
            var curso = Context.Set<Aluno>().FirstOrDefault(c => c.CPF.Contains(cpf));
            return curso;
        }
    }
}
