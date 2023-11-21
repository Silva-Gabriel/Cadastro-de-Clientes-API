using CadastroDeClientes.Dtos;
using Microsoft.AspNetCore.Http;

namespace CadastroDeClientes.Models.Response
{
    public class OkListModel : ResponseModel
    {
        public List<GetClientDto> Entity { get; set; }
        public OkListModel(List<GetClientDto> entity, string description)
        {
            Entity = entity;
            Description = description;
        }
    }
}
