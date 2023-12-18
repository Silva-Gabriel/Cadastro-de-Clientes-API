using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Service.Interfaces
{
    public interface IClientService
    {
        Task<ActionResult<GetClientDto>> Create(CreateClientDto client);
        Task<ActionResult<List<ClientModelDto>>> GetAllFullAcess();
        Task<ActionResult<List<GetClientDto>>> GetAll();
        ActionResult<GetClientDto> Get(long id);
        Task<ActionResult<ClientModel>> Delete(long id);
        Task<ActionResult<GetClientDto>> Edit(long id, EditClientDto client);
        Task<ActionResult<GetClientDto>> Inactive(long id);
        bool CPFDigitValidation(string cpf);
        bool IsClientNull(CreateClientDto clientDto);
    }
}
