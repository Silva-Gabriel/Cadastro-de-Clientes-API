using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Models;
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

        public static ObjectResult OkListFulAccessResponse(List<ClientModel> entity)
        {
            var response = new ObjectResult(entity);
            response.StatusCode = 200;

            return response;
        }

        public static ObjectResult CreateResponse(ClientModel entity) 
        {
            var response = new ObjectResult(new GetClientDto(entity));
            response.StatusCode = 201;

            return response;
        }
    }
}
