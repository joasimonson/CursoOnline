using CursoOnline.Data.Contexts;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.DominioTest.Builders;
using System.Collections.Generic;

namespace CursoOnline.Ioc.Data
{
    public static class ImportDbData
    {
        public static void ImportData(AppDbContext context)
        {
            for (int i = 0; i < 5; i++)
            {
                var matricula = MatriculaBuilder.Novo().Build();

                context.Matriculas.Add(matricula);
                context.Alunos.Add(matricula.Aluno);
                context.Cursos.Add(matricula.Curso);
            }

            //var matriculas = new List<Matricula>()
            //{
            //    MatriculaBuilder.Novo().Build(),
            //    ,
            //};

            //foreach (var item in collection)
            //{

            //}

            ////var alunos = new List<Aluno>()
            ////{
            ////    AlunoBuilder.Novo().Build(),
            ////    AlunoBuilder.Novo().Build(),
            ////};
            //context.Alunos.AddRange(matriculas[0].Aluno);

            ////var cursos = new List<Curso>()
            ////{
            ////    CursoBuilder.Novo().Build(),
            ////    CursoBuilder.Novo().Build()
            ////};
            //context.Cursos.AddRange(cursos);

            

            context.SaveChanges();
        }
    }
}
