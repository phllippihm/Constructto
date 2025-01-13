using System;
using System.ComponentModel.DataAnnotations;

namespace Constructto.Models
{
    public class Estoque
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O fornecedor é obrigatório.")]
        public string Fornecedor { get; set; }

        [Required(ErrorMessage = "A data de entrada é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataDeEntrada { get; set; }
    }
}
