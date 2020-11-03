using CursoOnline.Data.Contexts;
using CursoOnline.Dominio.Matriculas;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CursoOnline.Data.Repositorios
{
    public class MatriculaRepositorio : RepositorioBase<Matricula>, IMatriculaRepositorio
    {
        public MatriculaRepositorio(AppDbContext context) : base(context)
        {
        }

        public override IEnumerable<Matricula> Consultar()
        {
            var lista = Context.Set<Matricula>()
                .Include(i => i.Aluno)
                .Include(i => i.Curso);

            return lista;
        }
    }
}