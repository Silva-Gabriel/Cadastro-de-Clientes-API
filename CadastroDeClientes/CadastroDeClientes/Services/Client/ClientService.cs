using AutoMapper;
using CadastroDeClientes.Context;
using CadastroDeClientes.Dtos;
using CadastroDeClientes.Models;
using CadastroDeClientes.Responses;
using CadastroDeClientes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Services.Client
{
    public class ClientService : IClient
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActionResult<CreateClientDto>> Create(ClientModel clientModel)
        {
            if (clientModel == null)
            {
                throw new Exception("Preencha todos os dados mínimos!");
            }
            try {
                    
                _context.Add(clientModel);
                await _context.SaveChangesAsync();

            }
            catch
            {
                throw new Exception("other-exception");
            }
            
            var response = _mapper.Map<CreateClientDto>(clientModel);

            return SucessResponse.CreateResponse(response);
        }

        public async Task<ActionResult<ClientModel>> Delete(long id)
        {
            var client = await _context.Clients.Where(c => c.Id == id)
                    .FirstOrDefaultAsync();

            if(client == null) 
            {
                throw new Exception("client-not-found");
            }

            _context.Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<ActionResult<GetClientDto>> Edit(long id, ClientModel clientModel)
        {
            ClientModel client = _context.Clients.Find(id);

            if (client == null)
                throw new Exception("O Usuário não foi encontrado!");

            // Dados a serem modificados
            client.CPF = clientModel.CPF;
            client.Name = clientModel.Name;
            client.LastName = clientModel.LastName;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetClientDto>(clientModel);

            return SucessResponse.OkResponse(response);
        }

        public ActionResult<GetClientDto> Get(long id)
        {
            var client = _context.Clients.Find(id);

            if (client == null)
                return ErrorResponse.EntityNotFoundResponse();

            var response = _mapper.Map<GetClientDto>(client);

            return SucessResponse.OkResponse(response);
        }

        public async Task<ActionResult<List<GetClientDto>>> GetAll()
        {
            var clients = await _context.Clients.ToListAsync();

            var response = _mapper.Map<List<GetClientDto>>(clients);

            return SucessResponse.OkListResponse(response);
        }

        public async Task<ActionResult<GetClientDto>> Inactive(long id)
        {
            ClientModel client = _context.Clients.Find(id);

            if (client == null)
                return ErrorResponse.EntityNotFoundResponse();

            if (client.Status == 0)
                throw new Exception("O cliente já está inativo!");

            client.Status = 0;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetClientDto>(client);

            return SucessResponse.OkResponse(response);
        }
    }
}
