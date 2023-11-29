using AutoMapper;
using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Dtos.Email;
using CadastroDeClientes.Dtos.Email.AlternativeEmail;
using CadastroDeClientes.Models;
using CadastroDeClientes.Models.SubModelCliente;
using CadastroDeClientes.Models.SubModels;

namespace CadastroDeClientes.Mapper
{
    public class EntitiesToDtoProfile : Profile
    {
        public EntitiesToDtoProfile() {

            CreateMap<ClientModel, GetClientDto>()
                .ReverseMap();

            CreateMap<ClientModel, GetClientDto>()
                .ForMember(member => member.FullName, map => map.MapFrom(src => $"{src.Name} {src.LastName}"));

            CreateMap<ClientModel, CreateClientDto>()
                .ReverseMap();

            CreateMap<ClientModel, EditClientDto>()
                .ReverseMap();

            CreateMap<EmailModel, EmailModelDto>()
                .ReverseMap();

            CreateMap<AlternativeEmailModel, AlternativeEmailModelDto>()
                .ReverseMap();
        }
    }
}
