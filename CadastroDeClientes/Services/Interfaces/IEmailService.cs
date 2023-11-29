using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Dtos.Email;
using CadastroDeClientes.Models.SubModels;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ActionResult<EmailModel>> Create(long id, EmailModelDto email);
        Task<ActionResult<GetEmailModelDto>> Get(long id);
        Task<ActionResult<List<GetEmailModelDto>>> GetAllAdmin();
        Task<ActionResult<GetEmailModelDto>> Update(long id, EmailModelDto email);
        Task<IActionResult> Delete(long id);
    }
}
