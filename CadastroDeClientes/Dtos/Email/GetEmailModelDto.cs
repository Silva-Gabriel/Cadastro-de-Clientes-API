using CadastroDeClientes.Dtos.Email.AlternativeEmail;
using CadastroDeClientes.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Email
{
    public class GetEmailModelDto
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public long ClientModelId { get; set; }
    }
}
