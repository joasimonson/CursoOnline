using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Base;
using CursoOnline.Dominio.Builders;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Exceptions;

namespace CursoOnline.Dominio.Matriculas
{
    public class Matricula : EntidadeBase
    {
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public decimal ValorPago { get; private set; }
        public bool TemDesconto { get; private set; }
        public decimal NotaAluno { get; private set; }
        public bool CursoConcluido { get; private set; }
        public bool Cancelada { get; private set; }

        private Matricula() { }

        public Matricula(Aluno aluno, Curso curso, decimal valorPago)
        {
            ValidadorRegra.Novo()
                .ComRegra(aluno == null, () => throw new ParametroInvalidoException(nameof(Aluno)))
                .ComRegra(curso == null, () => throw new ParametroInvalidoException(nameof(Curso)))
                .ComRegra(valorPago < 1, () => throw new ParametroInvalidoException(nameof(ValorPago)))
                .ComRegra(valorPago > curso?.Valor, () => throw new ValorMatriculaMaiorQueCursoException())
                .ComRegra(aluno?.PublicoAlvo != curso?.PublicoAlvo, () => throw new PublicoAlvoCursoEAlunoDiferenteException())
                .Validar();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
            TemDesconto = ValorPago < Curso.Valor;
        }

        public void InformarNota(decimal nota)
        {
            ValidadorRegra.Novo()
                .ComRegra(Cancelada, () => throw new NaoEhPossivelInformarNotaComMatriculaCanceladaException())
                .ComRegra(nota < 0 || nota > 10, () => throw new ParametroInvalidoException(nameof(NotaAluno)))
                .Validar();

            NotaAluno = nota;
            CursoConcluido = true;
        }

        public void Cancelar()
        {
            ValidadorRegra.Novo()
                .ComRegra(CursoConcluido, () => throw new NaoEhPossivelCancelarComCursoConcluidoException())
                .Validar();
            
            Cancelada = true;
        }
    }
}
