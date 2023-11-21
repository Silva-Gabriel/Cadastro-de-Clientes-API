
namespace CadastroDeClientes.Models.SubModels
{
    public class AddressModel
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public ClientModel Client { get; set; }
        public long ClientModelId { get; set; }
    }
}