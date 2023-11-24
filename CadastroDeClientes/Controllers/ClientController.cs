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
        /// <response code="409">Conflict</response>>
        /// <response code="400">BadRequest</response>>
        [HttpPost]
        [SwaggerResponse(201, "Success", typeof(GetClientDto))]
        public async Task<ActionResult<GetClientDto>> Create(CreateClientDto clientDto)
        {
            var client = await _iclient.Create(clientDto);
            return client;
        }

        /// <summary>
        /// Obter lista de clientes completa
        /// </summary>
        /// <remarks>
        /// <h1 align="center">Este endpoint retorna todos os dados de todos os clientes cadastrados</h1>
        /// </remarks>
        /// <returns>Lista de clientes completa</returns>
        [HttpGet("Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ClientModel>>> GetAllFullAcess()
        {
            var clients = await _iclient.GetAllFullAcess();
            return clients;
        }

        /// <summary>
        /// Obter lista de clientes 
        /// </summary>
        /// <remarks>
        /// <h1 align="center">Este endpoint retorna todos os clientes da base de dados</h1>
        /// </remarks>
        /// <returns>Lista de clientes</returns>
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
        /// <remarks>
        /// <h1 align="center">Este endpoint retorna um cliente específico a partir do id informado no request</h1>
        /// </remarks>
        /// <returns>Cliente específico</returns>
        /// <response code="404">NotFound</response>
        [HttpGet("{id}")]
        public ActionResult<GetClientDto> Get(long id)
        {
            var client = _iclient.Get(id);
            return client;
        }

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <remarks>
        /// <h1 align="center">Este endpoint deleta um cliente a partir do id informado no request</h1>
        /// </remarks>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Cliente Recém-criado</returns>
        /// <response code="204">NoContent</response>>
        /// <response code="404">BadRequest</response>>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            await _iclient.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Atualizar um cliente
        /// </summary>
        /// <remarks>
        /// <h1 align="center">Este endpoint atualiza um cliente existente a partir do id informado no request</h1>
        /// </remarks>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="clientDto">Identificador do cliente</param>
        /// <returns>Cliente Recém-criado</returns>
        /// <response code="409">Conflict</response>>
        /// <response code="400">BadRequest</response>>
        /// <response code="404">NotFound</response>>
        [HttpPut("{id}")]
        [SwaggerResponse(200, "Success", typeof(GetClientDto))]
        public async Task<ActionResult<GetClientDto>> Update(long id, EditClientDto clientDto) 
        {
            var client = await _iclient.Edit(id, clientDto);

            return client;
        }

        /// <summary>
        /// Inativar um cliente
        /// </summary>
        /// <remarks>
        /// <h1 align="center">Este endpoint inativa um cliente existente e ativo a partir do id informado no request</h1>
        /// </remarks>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Cliente Recém-criado</returns>
        /// <response code="404">NotFound</response>>
        /// <response code="409">Conflict</response>>
        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Success", typeof(GetClientDto))]
        public async Task<ActionResult<GetClientDto>> Inactive(int id) 
        {
            var client = await _iclient.Inactive(id);

            return client;
        }
    }
}
