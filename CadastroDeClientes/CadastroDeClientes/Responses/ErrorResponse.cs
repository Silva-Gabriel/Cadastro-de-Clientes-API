using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Responses
{
    public class ErrorResponse
    {
        public static ObjectResult EntityNotFoundResponse()
        {
            var errorMessage = "entity-not-found";
            var statusCode = 404;
            var response = new ObjectResult(new { errorMessage, statusCode });
            response.StatusCode = statusCode;

            return response;
        }
    }
}
