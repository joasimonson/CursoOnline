using CursoOnline.Data.Contexts;
using CursoOnline.Data.Repositorios;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.Ioc
{
    public static class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("CursoOnline"));
            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));

            services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
            services.AddScoped<ICursoRepositorio, CursoRepositorio>();
            services.AddScoped<IMatriculaRepositorio, MatriculaRepositorio>();

            services.AddScoped<SalvarAluno>();
            services.AddScoped<SalvarCurso>();
            services.AddScoped<SalvarMatricula>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<ConclusaoDaMatricula>();
            //services.AddScoped<CancelamentoDaMatricula>();
        }
    }
}
