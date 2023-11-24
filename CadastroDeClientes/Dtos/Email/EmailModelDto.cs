using CadastroDeClientes.Dtos.Email.AlternativeEmail;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Email
{
    public class EmailModelDto
    {
        [Required]
        [EmailAddress]
        public string MainEmail { get; set; }
        public List<AlternativeEmailModelDto> AlternativeEmails { get; set; }
    }
}
