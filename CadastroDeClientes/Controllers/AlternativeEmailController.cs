using CadastroDeClientes.Models.SubModelCliente;
using CadastroDeClientes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlternativeEmailController : ControllerBase
    {
        private readonly IAlternativeEmailService _ialternativeEmail;

        public AlternativeEmailController(IAlternativeEmailService ialternativeEmail)
        {
            _ialternativeEmail = ialternativeEmail;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<AlternativeEmailModel>>> Get(long id) {
            var response = await _ialternativeEmail.Get(id);

            return response;
            
        }
    }
}
