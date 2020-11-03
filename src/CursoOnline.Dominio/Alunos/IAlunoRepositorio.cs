using CursoOnline.Dominio.Base;

namespace CursoOnline.Dominio.Alunos
{
    public interface IAlunoRepositorio : IRepositorioBase<Aluno>
    {
        Aluno ObterPorCPF(string cpf);
    }
}
