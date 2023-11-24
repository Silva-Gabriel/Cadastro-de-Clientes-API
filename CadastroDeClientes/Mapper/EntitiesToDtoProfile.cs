using AutoMapper;
using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Models;

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
        }
    }
}
