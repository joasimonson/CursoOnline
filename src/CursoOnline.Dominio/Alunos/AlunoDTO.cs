using System.ComponentModel.DataAnnotations;

namespace CursoOnline.Dominio.Alunos
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PublicoAlvoId { get; set; }
    }
}
