

using System.Text.Json.Serialization;

namespace CadastroDeClientes.Models.SubModels
{
    public class EmailModel
    {
        public long Id { get; set; }
        public string MainEmailAddress { get; set; }
        public string AlternativeEmailAddress { get; set; }
        [JsonIgnore]
        public ClientModel Client { get; set; }
        public long ClientModelId { get; set; }
    }
}