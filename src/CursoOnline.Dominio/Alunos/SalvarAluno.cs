using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using System;

namespace CursoOnline.Dominio.Alunos
{
    public class SalvarAluno
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public SalvarAluno(IAlunoRepositorio alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public void Salvar(AlunoDTO alunoDTO)
        {
            var alunoCadastrado = _alunoRepositorio.ObterPorCPF(alunoDTO.CPF);

            ValidadorRegra.Novo()
                .ComRegra(alunoCadastrado != null && alunoDTO.Id != alunoCadastrado.Id, () => throw new RegistroDuplicadoException(alunoDTO.CPF))
                .Validar();

            Aluno aluno;

            if (alunoDTO.Id == 0)
            {
                bool publicoAlvoValido = Enum.TryParse(alunoDTO.PublicoAlvoId, out PublicoAlvoEnum publicoAlvo);

                ValidadorRegra.Novo()
                    .ComRegra(!publicoAlvoValido, () => throw new ParametroInvalidoException(nameof(AlunoDTO.PublicoAlvoId)))
                    .Validar();

                aluno = new Aluno(alunoDTO.Nome, alunoDTO.CPF, alunoDTO.Email, publicoAlvo);
                _alunoRepositorio.Salvar(aluno);
            }
            else
            {
                aluno = _alunoRepositorio.ObterPorId(alunoDTO.Id);
                aluno.AlterarAluno(alunoDTO.Nome);
            }
        }
    }
}
