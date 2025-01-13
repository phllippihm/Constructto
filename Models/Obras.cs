using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Constructto.Models
{
    public class Obras
    {
        [Key]
        public int ObraId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Obra { get; set; }

        public List<Funcionario>? Funcionarios { get; set; } = new List<Funcionario>();
    }
}
