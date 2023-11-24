using CadastroDeClientes.Dtos.Email;
using CadastroDeClientes.Enums;
using CadastroDeClientes.Models.SubModels;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Client
{
    public class CreateClientDto
    {
        [Required]
        [RegularExpression(@"([\d]{3}\.[\d]{3}\.[\d]{3}\-[\d]{2})", ErrorMessage = "CPF Inválido!")]
        public string CPF { get; set; }
        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }
        [Required,MinLength(3), MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
    }
}
