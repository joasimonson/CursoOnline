using Bogus;
using CursoOnline.Dominio;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Exceptions;
using CursoOnline.DominioTest.Builders;
using CursoOnline.DominioTest.Extensions;
using Moq;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class SalvarCursoTest
    {
        private readonly Faker _faker;
        private readonly CursoDTO _cursoDTO;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly SalvarCurso _salvarCurso;

        public SalvarCursoTest()
        {
            _faker = new Faker();

            _cursoDTO = new CursoDTO
            {
                Nome = _faker.Person.FullName,
                Descricao = _faker.Lorem.Paragraph(),
                CargaHoraria = _faker.Random.Double(50, 1000),
                PublicoAlvoId = _faker.Random.Enum<PublicoAlvoEnum>().ToString(),
                Valor = _faker.Random.Decimal(100, 1000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _salvarCurso = new SalvarCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveIncluirCurso()
        {
            _salvarCurso.Salvar(_cursoDTO);

            _cursoRepositorioMock.Verify(r => r.Salvar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDTO.Nome &&
                    c.Descricao == _cursoDTO.Descricao &&
                    c.CargaHoraria == _cursoDTO.CargaHoraria &&
                    c.PublicoAlvo == Enum.Parse<PublicoAlvoEnum>(_cursoDTO.PublicoAlvoId) &&
                    c.Valor == _cursoDTO.Valor
                )
            ));

            //_cursoRepositorioMock.Verify(r => r, Times.AtLeast(2)); // Validação para especificar a chamada do método em x vezes
            //_cursoRepositorioMock.Verify(r => r, Times.Never); // Validação para especificar que o método não deve ser chamado
        }

        [Fact]
        public void NaoDeveDuplicarCursoComMesmoNome()
        {
            var idCurso = _faker.Random.Int(1, int.MaxValue);

            var cursoSalvo = CursoBuilder.Novo().ComId(idCurso).ComNome(_cursoDTO.Nome).Build();

            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDTO.Nome)).Returns(cursoSalvo);

            Assert.Throws<RegraDominioException>(() => _salvarCurso.Salvar(_cursoDTO))
                .ValidarExcept<RegistroDuplicadoException>();
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            _cursoDTO.PublicoAlvoId = "PublicoAlvoInvalido";

            Assert.Throws<RegraDominioException>(() => _salvarCurso.Salvar(_cursoDTO))
                .ValidarExcept<ParametroInvalidoException>();
        }

        [Fact]
        public void DeveAlterarDadosCurso()
        {
            string nomeAlterado = _faker.Person.FullName;
            _cursoDTO.Id = _faker.Random.Int(1, int.MaxValue);
            _cursoDTO.Nome = nomeAlterado;

            var curso = CursoBuilder.Novo().ComId(_cursoDTO.Id).Build();

            _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDTO.Id)).Returns(curso);

            _salvarCurso.Salvar(_cursoDTO);

            Assert.Equal(_cursoDTO.Nome, curso.Nome);
            //Assert.Equal(_cursoDTO.CargaHoraria, curso.CargaHoraria);
            //Assert.Equal(_cursoDTO.Valor, curso.Valor);
        }
    }
}
