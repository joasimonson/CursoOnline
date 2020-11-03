using System;
using System.ComponentModel.DataAnnotations;

namespace CursoOnline.Dominio.Cursos
{
    public class CursoDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        [Range(1, double.PositiveInfinity, ErrorMessage = "The field {0} must be greater than {1}.")]
        public double CargaHoraria { get; set; }
        [Required]
        public string PublicoAlvoId { get; set; }
        [Required]
        [Range(1.0, double.PositiveInfinity, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Valor { get; set; }
    }
}
