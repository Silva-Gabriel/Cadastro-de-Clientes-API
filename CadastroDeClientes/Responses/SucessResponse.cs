using Azure;
using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Models;
using CadastroDeClientes.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Responses
{
    public class SucessResponse<T> : ResultResponse
    {
        public static ObjectResult Ok(T entity)
        {
            IsSucess = true;
            var response = new ObjectResult(new { IsSucess, entity, StatusCode = StatusCodes.Status200OK });
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }

        public static ObjectResult Created(T entity) 
        {
            IsSucess = true;
            var response = new ObjectResult(new { IsSucess, entity, StatusCode = StatusCodes.Status201Created });
            response.StatusCode = StatusCodes.Status201Created;

            return response;
        }
    }
}
