using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Enums;
using System;

namespace CursoOnline.DominioTest.Builders
{
    public class AlunoBuilder
    {
        private readonly Faker _faker;

        private int _id;
        private string _nome;
        private string _cpf;
        private string _email;
        private PublicoAlvoEnum _publicoAlvo;

        private AlunoBuilder()
        {
            _faker = new Faker();

            _nome = _faker.Person.FullName;
            _cpf = _faker.Person.Cpf();
            _email = _faker.Person.Email;
            _publicoAlvo = _faker.Random.Enum<PublicoAlvoEnum>();
        }

        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public AlunoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AlunoBuilder ComCPF(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public AlunoBuilder ComPublicoAlvo(PublicoAlvoEnum publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public Aluno Build()
        {
            var aluno = new Aluno(_nome, _cpf, _email, _publicoAlvo);

            if (_id > 0)
            {
                var propInfo = aluno.GetType().GetProperty(nameof(aluno.Id));
                propInfo.SetValue(aluno, Convert.ChangeType(_id, propInfo.PropertyType), null);
            }

            return aluno;
        }
    }
}
