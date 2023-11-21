using CadastroDeClientes.Enums;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos
{
    public class GetClientDto
    {
        public long Id { get; set; }
        [Required]
        [MinLength(11, ErrorMessage = "O campo precisa conter no máximo 11 caracteres")]
        [MaxLength(11, ErrorMessage = "O campo precisa conter no máximo 11 caracteres")]
        public string CPF { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CustomerSince { get; set; } = DateTime.Now;
        public StatusClientEnum Status { get; set; }
    }
}
