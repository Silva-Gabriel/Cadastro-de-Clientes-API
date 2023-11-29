using CadastroDeClientes.Models.SubModels;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CadastroDeClientes.Models.SubModelCliente
{
    public class AlternativeEmailModel
    {
        public long Id { get; set; }
        [EmailAddress]
        public string AlternativeEmail { get; set; }
        public EmailModel Email { get; set; }
        public long EmailModelId { get; set; }
    }
}
