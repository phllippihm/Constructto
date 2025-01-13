using System;
using System.ComponentModel.DataAnnotations;

namespace Constructto.Models
{
    public class EstoqueViewModel
    {
        // Propriedades semelhantes ao modelo Estoque

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que 0.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que 0.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O fornecedor é obrigatório.")]
        public string Fornecedor { get; set; }

        [Required(ErrorMessage = "A data de entrada é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataDeEntrada { get; set; }

        // Campos adicionais para interface de usuário, se necessário
        public bool IsEditing { get; set; } // Por exemplo, campo adicional para UI, se está em modo de edição
    }
}
