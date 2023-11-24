using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos.Client
{
    public class ClientValueDto
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
