using CadastroDeClientes.Models.SubModelCliente;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Email.AlternativeEmail
{
    public class AlternativeEmailModelDto
    {
        [EmailAddress]
        public string AlternativeEmail { get; set; }
    }
}
