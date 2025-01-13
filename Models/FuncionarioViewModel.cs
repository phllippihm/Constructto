using System;
using System.ComponentModel.DataAnnotations;

namespace Constructto.Models
{
    public class FuncionarioViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }

        public string Telefone { get; set; }

        [Display(Name = "Possui carteira de habilitação?")]
        public bool CarteiraHabilitacao { get; set; }

        [Display(Name = "Possui filhos?")]
        public bool TemFilhos { get; set; }

        [Required(ErrorMessage = "O salário é obrigatório.")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "A forma de pagamento é obrigatória.")]
        public string FormaPagamento { get; set; }

        [Required(ErrorMessage = "A frequência de pagamento é obrigatória.")]
        public string FrequenciaPagamento { get; set; }

        [Display(Name = "Possui carteira assinada?")]
        public bool CarteiraAssinada { get; set; }

        public string Qualidades { get; set; }

        [Display(Name = "Tamanho de EPIs")]
        public string TamanhoEpis { get; set; }

        // Adicione a propriedade Altura
        [Display(Name = "Altura (em metros)")]
        public int? Altura { get; set; }

        // Adicione a propriedade Endereco
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        // Propriedade de chave estrangeira para a obra
        public int? ObraId { get; set; } // Certifique-se de que este campo seja enviado pela View

        // Lista de obras para preencher o dropdown
        public List<Obras>? Obras { get; set; }
    }
}
