using CadastroDeClientes.Dtos;

namespace CadastroDeClientes.Models.Response
{
    public class CreateModel : ResponseModel
    {
        public CreateClientDto Entity { get; set; }
        public CreateModel(CreateClientDto entity, string description)
        {
            Entity = entity;
            Description = description;
        }
    }
}
