namespace CursoOnline.Dominio.Matriculas
{
    public class MatriculaDTO
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public string NomeAluno { get; set; }
        public int CursoId { get; set; }
        public string NomeCurso { get; set; }
        public decimal ValorPago { get; set; }
    }
}
