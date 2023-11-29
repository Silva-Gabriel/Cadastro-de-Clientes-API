using CadastroDeClientes.Dtos.Email;
using CadastroDeClientes.Models.SubModels;
using CadastroDeClientes.Service.Interfaces;
using CadastroDeClientes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmail _iemail;

        public EmailController(IEmail email) {
            _iemail = email;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<EmailModel>> Create(long id, EmailModelDto email) 
        {
            var response = await _iemail.Create(id, email);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmailModel>> Get(long id) 
        {
            var response = await _iemail.Get(id);

            return response;
        }
    }
}
