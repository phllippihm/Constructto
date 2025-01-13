using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Constructto.Models
{
    public class Funcionario
    {
        public int Id { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public string Telefone { get; set; }

        [Required]
        public bool CarteiraAssinada { get; set; }

        public int? Altura { get; set; }

        public string Endereco { get; set; }

        [Required]
        public bool TemFilhos { get; set; }

        [Required]
        public decimal Salario { get; set; }

        [Required]
        [MaxLength(50)]
        public string FormaPagamento { get; set; }

        [Required]
        [MaxLength(50)]
        public string FrequenciaPagamento { get; set; }

        [MaxLength(10)]
        public string TamanhoEpis { get; set; }

        [MaxLength(500)]
        public string PrincipaisQualidades { get; set; }

        [MaxLength(50)]
        public string CarteiraHabilitacao { get; set; }

        [StringLength(13)] 
        public string Sexo { get; set; }

     
        public int? ObraId { get; set; }

        
        [ForeignKey("ObraId")]
        public Obras? Obra { get; set; }


    }
}
