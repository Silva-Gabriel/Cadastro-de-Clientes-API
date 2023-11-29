using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Email
{
    public class EmailModelDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
