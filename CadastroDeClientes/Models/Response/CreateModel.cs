using CadastroDeClientes.Dtos.Client;

namespace CadastroDeClientes.Models.Response
{
    public class CreateModel
    {
        public CreateClientDto Entity { get; set; }
        public CreateModel(CreateClientDto entity)
        {
            Entity = entity;
        }
    }
}
