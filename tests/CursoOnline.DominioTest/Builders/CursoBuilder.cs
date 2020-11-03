using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.DominioTest.Extensions;
using System;

namespace CursoOnline.DominioTest.Builders
{
    public class CursoBuilder
    {
        private readonly Faker _faker;

        private int _id;
        private string _nome;
        private string _descricao;
        private double _cargaHoraria;
        private PublicoAlvoEnum _publicoAlvo;
        private decimal _valor;

        private CursoBuilder()
        {
            _faker = new Faker();

            _nome = _faker.Person.FullName;
            _descricao = _faker.Lorem.Paragraph();
            _cargaHoraria = _faker.Random.DoublePositive(1, 200).Round();
            _publicoAlvo = _faker.Random.Enum<PublicoAlvoEnum>();
            _valor = _faker.Random.DecimalPositive(100, 5000).Round();
        }

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvoEnum publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public Curso Build()
        {
            var curso = new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);

            if (_id > 0)
            {
                var propInfo = curso.GetType().GetProperty(nameof(Curso.Id));
                propInfo.SetValue(curso, Convert.ChangeType(_id, propInfo.PropertyType), null);
            }

            return curso;
        }
    }
}
