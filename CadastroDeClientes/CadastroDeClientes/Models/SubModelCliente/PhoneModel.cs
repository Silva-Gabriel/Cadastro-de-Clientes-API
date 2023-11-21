
using System.Text.Json.Serialization;

namespace CadastroDeClientes.Models.SubModels
{
    public class PhoneModel
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string DDD { get; set; }
        public string CountryCode { get; set; }
        [JsonIgnore]
        public ClientModel Client { get; set; }
        public long ClientModelId { get; set; }
    }
}