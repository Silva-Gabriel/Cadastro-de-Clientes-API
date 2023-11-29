

using CadastroDeClientes.Models.SubModelCliente;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Models.SubModels
{
    public class EmailModel
    {
        public long Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public List<AlternativeEmailModel> AlternativeEmails { get; set; }
        public ClientModel Client { get; set; }
        public long ClientModelId { get; set; }
    }
}