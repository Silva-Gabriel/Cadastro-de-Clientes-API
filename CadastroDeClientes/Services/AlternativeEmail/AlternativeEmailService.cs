using AutoMapper;
using CadastroDeClientes.Context;
using CadastroDeClientes.Models.SubModelCliente;
using CadastroDeClientes.Responses;
using CadastroDeClientes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Services.AlternativeEmail
{
    public class AlternativeEmailService : IAlternativeEmailService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AlternativeEmailService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActionResult<List<AlternativeEmailModel>>> Get(long id)
        {
            var response = await _context.AlternativeEmails.Where(c => c.Id == id).ToListAsync();

            if (response == null)
                return ErrorResponse.EntityNotFoundResponse();

            return response;

        }
    }
}
