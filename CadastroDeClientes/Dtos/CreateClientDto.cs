using CadastroDeClientes.Enums;
using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Dtos
{
    public class CreateClientDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
    }
}
