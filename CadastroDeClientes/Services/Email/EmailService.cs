using AutoMapper;
using CadastroDeClientes.Context;
using CadastroDeClientes.Dtos.Email;
using CadastroDeClientes.Models.SubModelCliente;
using CadastroDeClientes.Models.SubModels;
using CadastroDeClientes.Responses;
using CadastroDeClientes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Services.Email
{
    public class EmailService : IEmailService
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
            var emailsByUser = await _context.Emails.Where(c => c.ClientModelId == id).ToListAsync();
            var existingEmailByUser = await _context.Emails.FirstOrDefaultAsync(c => c.Email == email.Email);

            if (client == null)
                throw new ArgumentException("Client not exists");

            if (client.Status == 0)
                throw new ArgumentException("Inactive client");

            if (emailsByUser.Count >= 1)
                throw new ArgumentException("The customer already has a registered primary email");

            if (existingEmailByUser != null)
                throw new Exception("There is already a main email registered with this address!");

            var response = _mapper.Map<EmailModel>(email);

            response.ClientModelId = id;
            response.Client = client;
            response.AlternativeEmails = _mapper.Map<List<AlternativeEmailModel>>(response.AlternativeEmails);

            _context.Add(response);
            await _context.SaveChangesAsync();

            return SucessResponse<EmailModel>.Created(response);
        }

        public async Task<ActionResult<List<GetEmailModelDto>>> GetAllAdmin() 
        {
            var emails = await _context.Emails.ToListAsync();
            var response = _mapper.Map<List<GetEmailModelDto>>(emails);
            return response;
        }

        public async Task<ActionResult<GetEmailModelDto>> Get(long id) 
        {
            var emails = await _context.Emails.FirstOrDefaultAsync(c => c.ClientModelId == id);

            if (emails == null)
                return ErrorResponse.EntityNotFoundResponse();

            var response = _mapper.Map<GetEmailModelDto>(emails);

            return response;
        }

        public async Task<ActionResult<GetEmailModelDto>> Update(long id, EmailModelDto emailModel)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(c => c.ClientModelId == id);
            email.Email = emailModel.Email;

            _context.Update(email);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetEmailModelDto>(email);

            return response;
        }

        public async Task<IActionResult> Delete(long id)
        {
            var email = await _context.Emails.FindAsync(id);

            if(email == null)
                return ErrorResponse.EntityNotFoundResponse();

            _context.Remove(email);
            await _context.SaveChangesAsync();

            return ErrorResponse.EntityNotFoundResponse();
        }
    }
}
