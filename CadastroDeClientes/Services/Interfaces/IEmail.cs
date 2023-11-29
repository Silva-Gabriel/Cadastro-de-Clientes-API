using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Dtos.Email;
using CadastroDeClientes.Models.SubModels;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Services.Interfaces
{
    public interface IEmail
    {
        Task<ActionResult<EmailModel>> Create(long id, EmailModelDto email);
        Task<ActionResult<EmailModel>> Get(long id);
    }
}
