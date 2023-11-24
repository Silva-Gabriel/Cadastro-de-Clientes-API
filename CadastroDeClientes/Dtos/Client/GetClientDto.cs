using CadastroDeClientes.Enums;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Client
{
    public class GetClientDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime CustomerSince { get; set; }
        public StatusClientEnum Status { get; set; }
    }
}
