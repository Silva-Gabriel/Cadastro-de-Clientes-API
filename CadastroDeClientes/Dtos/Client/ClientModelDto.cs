using CadastroDeClientes.Enums;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Client
{
    public class ClientModelDto
    {
        public long Id { get; set; }
        public string CPF { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CustomerSince { get; set; }
        public StatusClientEnum Status { get; set; }
    }
}
