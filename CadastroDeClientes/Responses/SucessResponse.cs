using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Responses
{
    public class SucessResponse
    {
        public static ObjectResult OkResponse(GetClientDto entity)
        {
            var response = new ObjectResult(new OkModel(entity));
            response.StatusCode = 200;

            return response;
        }

        public static ObjectResult OkListResponse(List<GetClientDto> entity)
        {
            var response = new ObjectResult(new OkListModel(entity));
            response.StatusCode = 200;

            return response;
        }

        public static ObjectResult CreateResponse(CreateClientDto entity) 
        {
            var response = new ObjectResult(new CreateModel(entity));
            response.StatusCode = 201;

            return response;
        }
    }
}
