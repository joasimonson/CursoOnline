using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using System;

namespace CursoOnline.Dominio.Cursos
{
    public class SalvarCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public SalvarCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Salvar(CursoDTO cursoDTO)
        {
            var cursoSalvo = _cursoRepositorio.ObterPeloNome(cursoDTO.Nome);

            ValidadorRegra.Novo()
                .ComRegra(cursoSalvo != null && cursoSalvo.Id != cursoDTO.Id, () => throw new RegistroDuplicadoException(cursoDTO.Nome))
                .Validar();

            Curso curso;

            if (cursoDTO.Id == 0)
            {
                bool publicoAlvoValido = Enum.TryParse(cursoDTO.PublicoAlvoId, out PublicoAlvoEnum publicoAlvo);

                ValidadorRegra.Novo()
                    .ComRegra(!publicoAlvoValido, () => throw new ParametroInvalidoException(nameof(CursoDTO.PublicoAlvoId)))
                    .Validar();

                curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, publicoAlvo, cursoDTO.Valor);
                _cursoRepositorio.Salvar(curso);
            }
            else
            {
                curso = _cursoRepositorio.ObterPorId(cursoDTO.Id);
                curso.AlterarNome(cursoDTO.Nome);
            }
        }
    }
}
