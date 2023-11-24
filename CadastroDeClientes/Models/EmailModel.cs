

using CadastroDeClientes.Models.SubModelCliente;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CadastroDeClientes.Models.SubModels
{
    public class EmailModel
    {
        public long Id { get; set; }
        [Required]
        [EmailAddress]
        public string MainEmail { get; set; }
        [EmailAddress]
        public List<AlternativeEmailModel> AlternativeEmails { get; set; }
        [JsonIgnore]
        public ClientModel Client { get; set; }
        public long ClientModelId { get; set; }
    }

}