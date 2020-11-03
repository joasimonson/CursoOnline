using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.Dominio.Util;
using System;

namespace CursoOnline.Dominio.Alunos
{
    public class Aluno : EntidadeBase
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }

        private Aluno() { }

        public Aluno(string nome, string cpf, string email, PublicoAlvoEnum publicoAlvo)
        {
            ValidadorRegra.Novo()
                .ComRegra(String.IsNullOrWhiteSpace(nome), () => throw new ParametroInvalidoException(nameof(Aluno.Nome)))
                .ComRegra(!FuncUtil.ValidarCPF(cpf), () => throw new ParametroInvalidoException(nameof(Aluno.CPF)))
                .ComRegra(!FuncUtil.ValidarEmail(email), () => throw new ParametroInvalidoException(nameof(Aluno.Email)))
                .Validar();

            Nome = nome;
            CPF = cpf;
            Email = email;
            PublicoAlvo = publicoAlvo;
        }

        public void AlterarAluno(string nome)
        {
            ValidadorRegra.Novo()
                .ComRegra(String.IsNullOrWhiteSpace(nome), () => throw new ParametroInvalidoException(nameof(Aluno.Nome)))
                .Validar();

            Nome = nome;
        }
    }
}
