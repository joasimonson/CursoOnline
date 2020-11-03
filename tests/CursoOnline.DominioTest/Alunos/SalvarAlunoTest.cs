using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Extensions;
using Moq;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class SalvarAlunoTest
    {
        private readonly Faker _faker;
        private readonly AlunoDTO _alunoDTO;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
        private readonly SalvarAluno _salvarAluno;

        public SalvarAlunoTest()
        {
            _faker = new Faker();

            _alunoDTO = new AlunoDTO
            {
                Nome = _faker.Random.Word(),
                CPF = _faker.Person.Cpf(),
                Email = _faker.Person.Email,
                PublicoAlvoId = _faker.Random.Enum<PublicoAlvoEnum>().ToString(),
            };

            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            _salvarAluno = new SalvarAluno(_alunoRepositorioMock.Object);
        }

        [Fact]
        public void DeveIncluirAluno()
        {
            _salvarAluno.Salvar(_alunoDTO);

            _alunoRepositorioMock.Verify(r => r.Salvar(
                It.Is<Aluno>(
                    c => c.Nome == _alunoDTO.Nome &&
                    c.CPF == _alunoDTO.CPF &&
                    c.Email == _alunoDTO.Email &&
                    c.PublicoAlvo == Enum.Parse<PublicoAlvoEnum>(_alunoDTO.PublicoAlvoId)
                )
            ));
        }

        [Fact]
        public void NaoDeveDuplicarAlunoComMesmoCPF()
        {
            var idAluno = _faker.Random.NumberPositive();

            var alunoSalvo = AlunoBuilder.Novo().ComId(idAluno).ComCPF(_alunoDTO.CPF).Build();

            _alunoRepositorioMock.Setup(r => r.ObterPorCPF(_alunoDTO.CPF)).Returns(alunoSalvo);

            Assert.Throws<RegraDominioException>(() => _salvarAluno.Salvar(_alunoDTO))
                .ValidarExcept<RegistroDuplicadoException>();
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            _alunoDTO.PublicoAlvoId = "PublicoAlvoInvalido";

            Assert.Throws<RegraDominioException>(() => _salvarAluno.Salvar(_alunoDTO))
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Fact]
        public void DeveAlterarDadosAluno()
        {
            string nomeAlterado = _faker.Person.FullName;
            _alunoDTO.Id = _faker.Random.NumberPositive();
            _alunoDTO.Nome = nomeAlterado;

            var aluno = AlunoBuilder.Novo().ComId(_alunoDTO.Id).Build();

            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDTO.Id)).Returns(aluno);

            _salvarAluno.Salvar(_alunoDTO);

            Assert.Equal(_alunoDTO.Nome, aluno.Nome);
        }

        [Fact]
        public void NaoDeveAdiconarQuandoForEdicao()
        {
            _alunoDTO.Id = _faker.Random.NumberPositive();

            var alunoSalvo = AlunoBuilder.Novo().ComId(_alunoDTO.Id).Build();
            _alunoRepositorioMock.Setup(a => a.ObterPorId(_alunoDTO.Id)).Returns(alunoSalvo);

            _salvarAluno.Salvar(_alunoDTO);

            _alunoRepositorioMock.Verify(r => r.Salvar(It.IsAny<Aluno>()), Times.Never);
        }

        [Fact]
        public void NaoDeveEditarInformacoesAluno()
        {
            _alunoDTO.Id = _faker.Random.NumberPositive();

            var alunoSalvo = AlunoBuilder.Novo().ComId(_alunoDTO.Id).Build();
            _alunoRepositorioMock.Setup(a => a.ObterPorId(_alunoDTO.Id)).Returns(alunoSalvo);

            _salvarAluno.Salvar(_alunoDTO);

            Assert.NotEqual(_alunoDTO.CPF, alunoSalvo.CPF);
            Assert.NotEqual(_alunoDTO.Email, alunoSalvo.Email);
            Assert.NotEqual(Enum.Parse<PublicoAlvoEnum>(_alunoDTO.PublicoAlvoId), alunoSalvo.PublicoAlvo);
        }
    }
}
