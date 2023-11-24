using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Client
{
    public class EditClientDto
    {
        [MinLength(3), MaxLength(30)]
        [RegularExpression(@"([A-z]+\w)+", ErrorMessage = "Digite um nome válido!")]
        public string Name { get; set; }
        [MaxLength(50)]
        [RegularExpression(@"([A-z]+\w)+", ErrorMessage = "Digite um sobrenome válido!")]
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
