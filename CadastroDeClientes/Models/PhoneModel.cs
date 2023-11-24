
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CadastroDeClientes.Models.SubModels
{
    public class PhoneModel
    {
        public long Id { get; set; }
        [Required]
        [RegularExpression(@"(^9[0-9]{8}$)", ErrorMessage = "Número de celular inválido!")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"[\d]{2}", ErrorMessage = "DDD inválido!")]
        public string DDD { get; set; }
        [Required]
        [RegularExpression(@"[\d]{2}", ErrorMessage = "Código do país inválido!")]
        public string CountryCode { get; set; }
        [JsonIgnore]
        public ClientModel Client { get; set; }
        public long ClientModelId { get; set; }
    }
}