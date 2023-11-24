using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Service.Interfaces
{
    public interface IClient
    {
        Task<ActionResult<ClientModel>> Create(CreateClientDto client);
        Task<ActionResult<List<GetClientDto>>> GetAll();
        ActionResult<GetClientDto> Get(long id);
        Task<ActionResult<ClientModel>> Delete(long id);
        Task<ActionResult<GetClientDto>> Edit(long id, EditClientDto client);
        Task<ActionResult<GetClientDto>> Inactive(long id);
    }
}
