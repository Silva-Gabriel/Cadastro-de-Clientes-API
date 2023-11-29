using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Dtos.Email;
using CadastroDeClientes.Models.SubModels;
using CadastroDeClientes.Responses;
using CadastroDeClientes.Service.Interfaces;
using CadastroDeClientes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CadastroDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _iemail;

        public EmailController(IEmailService email) {
            _iemail = email;
        }

        /// <summary>
        /// Cadastrar e-mail principal
        /// </summary>
        /// <remarks>
        /// <h1 align="center">Este endpoint cadastra o e-mail principal relacionando-o a um cliente</h1>
        /// </remarks>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="email">Email principal</param>
        /// <response code="400">BadRequest</response>>
        /// <response code="404">NotFound</response>>
        /// <response code="409">Conflict</response>>
        /// <returns>O Cliente com o email recém cadastrado</returns>
        [HttpPost("{id}")]
        [SwaggerResponse(200, "Success", typeof(GetClientDto))]
        public async Task<ActionResult<EmailModel>> Create(long id, EmailModelDto email) 
        {
            var response = await _iemail.Create(id, email);

            return response;
        }

        /// <summary>
        /// Obtém e-mail por id
        /// </summary>
        /// <remarks>
        /// <h1 align="center">Este endpoint obtém o e-mail principal a partir do id do cliente</h1>
        /// </remarks>
        /// <param name="id">identificador do cliente</param>
        /// <response code="400">BadRequest</response>>
        /// <response code="404">NotFound</response>>
        /// <response code="409">Conflict</response>>
        /// <returns>O E-mail cadastrado</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(201, "Success", typeof(GetEmailModelDto))]
        public async Task<ActionResult<GetEmailModelDto>> Get(long id) 
        {
            var response = await _iemail.Get(id);

            return response;
        }

        [HttpGet("Admin")]
        public async Task<ActionResult<List<GetEmailModelDto>>> GetAllAdmin()
        {
            var response = await _iemail.GetAllAdmin();

            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetEmailModelDto>> Update(long id, EmailModelDto email) {
            var response = await _iemail.Update(id, email);
            return Ok(response);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete() 
        //{
        //}
    }
}
