using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using System;

namespace CursoOnline.Dominio.Cursos
{
    public class Curso : EntidadeBase
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }

        private Curso() { }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvoEnum publicoAlvo, decimal valor)
        {
            ValidadorRegra.Novo()
                .ComRegra(String.IsNullOrWhiteSpace(nome), () => throw new ParametroInvalidoException(nameof(Nome)))
                .ComRegra(cargaHoraria < 1, () => throw new ParametroInvalidoException(nameof(CargaHoraria)))
                .ComRegra(valor < 1, () => throw new ParametroInvalidoException(nameof(Valor)))
                .Validar();

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public void AlterarNome(string nome)
        {
            ValidadorRegra.Novo()
                .ComRegra(String.IsNullOrWhiteSpace(nome), () => throw new ParametroInvalidoException(nameof(Nome)))
                .Validar();

            Nome = nome;
        }
    }
}
