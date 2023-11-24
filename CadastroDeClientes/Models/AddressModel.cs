
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CadastroDeClientes.Models.SubModels
{
    public class AddressModel
    {
        public long Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"([\d]{5}-[\d]{3})", ErrorMessage = "Invalid Zipcode!")]
        public string Zipcode { get; set; }
        [JsonIgnore]
        public ClientModel Client { get; set; }
        public long ClientModelId { get; set; }
    }
}