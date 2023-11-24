using CadastroDeClientes.Enums;
using CadastroDeClientes.Models;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Client
{
    public class GetClientDto
    {
        public GetClientDto(ClientModel entity)
        {
            Id = entity.Id;
            FullName = entity.Name + " " + entity.LastName;
            CustomerSince = entity.CustomerSince;
            Status = entity.Status;
        }

        public GetClientDto() { }

        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime CustomerSince { get; set; }
        public StatusClientEnum Status { get; set; }
    }
}
