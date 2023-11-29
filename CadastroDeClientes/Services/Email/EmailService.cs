using AutoMapper;
using CadastroDeClientes.Context;
using CadastroDeClientes.Dtos.Email;
using CadastroDeClientes.Models.SubModelCliente;
using CadastroDeClientes.Models.SubModels;
using CadastroDeClientes.Responses;
using CadastroDeClientes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Services.Email
{
    public class EmailService : IEmail
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EmailService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<EmailModel>> Create(long id, EmailModelDto email)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                throw new ArgumentException("Client not exists");

            if (client.Status == 0)
                throw new ArgumentException("Inactive client");

            var response = _mapper.Map<EmailModel>(email);

            response.ClientModelId = id;
            response.Client = client;
            response.AlternativeEmails = _mapper.Map<List<AlternativeEmailModel>>(response.AlternativeEmails);

            _context.Add(response);
            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ActionResult<EmailModel>> Get(long id) 
        {
            var response = await _context.Emails.FindAsync(id);

            if (response == null)
                return ErrorResponse.EntityNotFoundResponse();

            return response;
        }
    }
}
