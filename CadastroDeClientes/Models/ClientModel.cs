using CadastroDeClientes.Enums;
using CadastroDeClientes.Models.SubModels;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Models
{
    public class ClientModel
    {
        public long Id { get; set; }
        [Required]
        [RegularExpression(@"([\d]{3}\.[\d]{3}\.[\d]{3}\-[\d]{2})", ErrorMessage = "CPF Inválido!")]
        public string CPF { get; set; }
        [Required, MinLength(3), MaxLength(30)]
        [RegularExpression(@"([A-z]+\w)+", ErrorMessage = "Digite um nome válido!")]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        [RegularExpression(@"(['A-z]+\w)+", ErrorMessage = "Digite um sobrenome válido!")]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        public DateTime CustomerSince { get; set; } = DateTime.UtcNow;
        public StatusClientEnum Status { get; set; } = StatusClientEnum.Ativo;
    }
}