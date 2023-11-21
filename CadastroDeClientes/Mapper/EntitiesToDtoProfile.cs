using AutoMapper;
using CadastroDeClientes.Dtos;
using CadastroDeClientes.Models;

namespace CadastroDeClientes.Mapper
{
    public class EntitiesToDtoProfile : Profile
    {
        public EntitiesToDtoProfile() {

            CreateMap<ClientModel, GetClientDto>()
                .ReverseMap();

            CreateMap<ClientModel, CreateClientDto>()
                .ForMember(member => member.FullName, map => map.MapFrom(src => $"{src.Name} {src.LastName}"));
        }
    }
}
