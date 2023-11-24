using CadastroDeClientes.Dtos.Client;

namespace CadastroDeClientes.Models.Response
{
    public class OkModel
    {
        public GetClientDto Entity { get; set; }
        public OkModel(GetClientDto entity) {
            Entity = entity;
        }
    }
}
