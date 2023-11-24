using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Models;
using CadastroDeClientes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CadastroDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _iclient;
        public ClientController(IClient iclient)
        {
            _iclient = iclient;
        }

        /// <summary>
        /// Cadastrar um cliente
        /// </summary>
        /// <remarks>
        /// <h1 align="center">Este endpoint cadastra um cliente na base de dados de clientes</h1>
        /// </remarks>
        /// <param name="clientDto">Dados do cliente</param>
        /// <returns>Cliente Recém-criado</returns>
        /// <response code="201"></response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponse(201, "Success", typeof(ClientValueDto))]
        public async Task<ActionResult<ClientModel>> Create(CreateClientDto clientDto)
        {
            var client = await _iclient.Create(clientDto);
            return client;
        }

        /// <summary>
        /// Obter lista de clientes 
        /// </summary>
        /// <returns>Lista de clientes</returns>
        /// <response code="200">Sucess</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetClientDto>>> GetAll() {
            var clients = await _iclient.GetAll();
            return clients;
        }

        /// <summary>
        /// Obter um cliente específico
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Cliente específico</returns>
        /// <response code="200">Sucess</response>>
        /// <response code="404">Client Not Found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GetClientDto> Get(long id)
        {
            var client = _iclient.Get(id);
            return client;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            await _iclient.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetClientDto>> Update(long id, EditClientDto clientDto) 
        {
            var client = await _iclient.Edit(id, clientDto);

            return client;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GetClientDto>> Inactive(int id) 
        {
            var client = await _iclient.Inactive(id);

            return client;
        }
    }
}
