using AutoMapper;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;

namespace CursoOnline.Ioc.Profiles
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            CreateMap<Aluno, AlunoDTO>()
                .ForMember(d => d.PublicoAlvoId, s => s.MapFrom(src => src.PublicoAlvo.ToString()));

            CreateMap<Curso, CursoDTO>()
                .ForMember(d => d.PublicoAlvoId, s => s.MapFrom(src => src.PublicoAlvo.ToString()));

            CreateMap<Matricula, MatriculaDTO>()
                .ForMember(d => d.AlunoId, s => s.MapFrom(src => src.Aluno.Id))
                .ForMember(d => d.NomeAluno, s => s.MapFrom(src => src.Aluno.Nome))
                .ForMember(d => d.CursoId, s => s.MapFrom(src => src.Curso.Id))
                .ForMember(d => d.NomeCurso, s => s.MapFrom(src => src.Curso.Nome));
        }
    }
}
