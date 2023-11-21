using CadastroDeClientes.Dtos;

namespace CadastroDeClientes.Models.Response
{
    public class OkModel : ResponseModel
    {
        public GetClientDto Entity { get; set; }
        public OkModel(GetClientDto entity, string description) {
            Entity = entity;
            Description = description;
        }
    }
}
