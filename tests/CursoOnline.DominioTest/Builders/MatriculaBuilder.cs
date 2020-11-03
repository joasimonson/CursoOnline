using Bogus;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Util;
using CursoOnline.DominioTest.Extensions;

namespace CursoOnline.DominioTest.Builders
{
    public class MatriculaBuilder
    {
        private readonly Faker _faker;

        private Aluno _aluno;
        private Curso _curso;
        private decimal _valorPago;
        private bool _cancelada;
        private bool _cursoConcluido;

        private MatriculaBuilder()
        {
            _faker = new Faker();

            var alunoId = _faker.Random.NumberPositive();
            var cursoId = _faker.Random.NumberPositive();

            var publicoAlvo = _faker.Random.Enum<PublicoAlvoEnum>();

            _aluno = AlunoBuilder.Novo().ComId(alunoId).ComPublicoAlvo(publicoAlvo).Build();
            _curso = CursoBuilder.Novo().ComId(cursoId).ComPublicoAlvo(publicoAlvo).Build();

            _valorPago = _curso.Valor;
        }

        public static MatriculaBuilder Novo()
        {
            return new MatriculaBuilder();
        }

        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            _aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            _curso = curso;
            return this;
        }

        public MatriculaBuilder ComValorPago(decimal valorPago)
        {
            _valorPago = valorPago;
            return this;
        }

        public MatriculaBuilder ComCancelada(bool cancelada)
        {
            _cancelada = cancelada;
            return this;
        }

        public MatriculaBuilder ComCursoConcluido(bool cursoConcluido)
        {
            _cursoConcluido = cursoConcluido;
            return this;
        }

        public Matricula Build()
        {
            var matricula = new Matricula(_aluno, _curso, _valorPago);

            if (_cancelada)
            {
                matricula.Cancelar();
            }

            if (_cursoConcluido)
            {
                decimal notaAluno = _faker.Random.Decimal(Constants.NOTAMINIMA, Constants.NOTAMAXIMA);
                matricula.InformarNota(notaAluno);
            }

            return matricula;
        }
    }
}
