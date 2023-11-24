using CadastroDeClientes.Dtos.Client;
using Microsoft.AspNetCore.Http;

namespace CadastroDeClientes.Models.Response
{
    public class OkListModel
    {
        public List<GetClientDto> Entity { get; set; }
        public OkListModel(List<GetClientDto> entity)
        {
            Entity = entity;
        }
    }
}
