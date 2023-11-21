using Azure;
using CadastroDeClientes.Dtos;
using CadastroDeClientes.Models;
using CadastroDeClientes.Responses;
using CadastroDeClientes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<CreateClientDto>> Create(ClientModel clientModel)
        {
            var client = await _iclient.Create(clientModel);
            return client;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetClientDto>>> GetAll() {
            var clients = await _iclient.GetAll();
            return clients;
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult<GetClientDto>> Update(long id, ClientModel clientModel) 
        {
            var client = await _iclient.Edit(id, clientModel);

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
