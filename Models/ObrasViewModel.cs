using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Constructto.Models
{
    public class ObrasViewModel
    {
        public Obras Obras { get; set; } = new Obras();
        
        public List<Obras> ListaDeObras { get; set; } = new List<Obras>();

        public List<Funcionario> Disponiveis { get; set; } = new List<Funcionario>();
    }
}
