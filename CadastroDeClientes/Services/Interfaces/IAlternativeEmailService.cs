using CadastroDeClientes.Models.SubModelCliente;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Services.Interfaces
{
    public interface IAlternativeEmailService
    {
        Task<ActionResult<List<AlternativeEmailModel>>> Get(long id);
    }
}
