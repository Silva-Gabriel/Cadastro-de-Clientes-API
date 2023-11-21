using CadastroDeClientes.Dtos;
using CadastroDeClientes.Models;
using CadastroDeClientes.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Responses
{
    public class SucessResponse
    {
        public static ObjectResult OkResponse(GetClientDto entity)
        {
            var description = "descrição qualquer";
            var response = new ObjectResult(new OkModel(entity, description));
            response.StatusCode = 200;

            return response;
        }

        public static ObjectResult OkListResponse(List<GetClientDto> entity)
        {
            var description = "descrição qualquer";
            var response = new ObjectResult(new OkListModel(entity, description));
            response.StatusCode = 200;

            return response;
        }

        public static ObjectResult CreateResponse(CreateClientDto entity) 
        {
            var description = "descrição qualquer";
            var response = new ObjectResult(new CreateModel(entity, description));
            response.StatusCode = 201;

            return response;
        }
    }
}
