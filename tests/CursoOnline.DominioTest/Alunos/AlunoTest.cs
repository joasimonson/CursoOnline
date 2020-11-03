using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Extensions;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class AlunoTest
    {
        private readonly string _nome;
        private readonly string _cpf;
        private readonly string _email;
        private readonly PublicoAlvoEnum _publicoAlvo;

        public AlunoTest()
        {
            var faker = new Faker();

            _nome = faker.Random.Word();
            _cpf = faker.Person.Cpf();
            _email = faker.Person.Email;
            _publicoAlvo = faker.Random.Enum<PublicoAlvoEnum>();
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _nome,
                CPF = _cpf,
                Email = _email,
                PublicoAlvo = _publicoAlvo
            };

            var aluno = new Aluno(_nome, _cpf, _email, _publicoAlvo);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AlunoNaoDeveTerNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<RegraDominioException>(() => AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("CPF inválido")]
        [InlineData("00000000000")]
        public void AlunoNaoDeveTerCPFInvalido(string cpfInvalido)
        {
            Assert.Throws<RegraDominioException>(() => AlunoBuilder.Novo().ComCPF(cpfInvalido).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email inválido")]
        [InlineData("email@invalido")]
        public void AlunoNaoDeveTerEmailInvalido(string emailInvalido)
        {
            Assert.Throws<RegraDominioException>(() => AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Fact]
        public void DeveAlterarNomeAluno()
        {
            var aluno = AlunoBuilder.Novo().Build();

            aluno.AlterarAluno(_nome);

            Assert.Equal(_nome, aluno.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarNomeAlunoInvalido(string nomeInvalido)
        {
            var aluno = AlunoBuilder.Novo().Build();

            Assert.Throws<RegraDominioException>(() =>
                aluno.AlterarAluno(nomeInvalido))
                .ValidarExcept<ParametroInvalidoException>();
        }
    }
}
