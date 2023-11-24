using CadastroDeClientes.Dtos.Client;
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

        public static ObjectResult UserAlreadyExists()
        {
            var errorMessage = "user-already-exists";
            var statusCode = 409;
            var response = new ObjectResult(new { errorMessage, statusCode });
            response.StatusCode = statusCode;

            return response;
        }

        public static ObjectResult InvalidNameAndLastName(string errorMessage)
        {
            string[] errors = new string[2];
            var message_split = errorMessage.Split(' ');
            errors[0] = message_split[0];
            errors[1] = message_split[1];
            var statusCode = 409;
            var response = new ObjectResult(new { errors, statusCode });
            response.StatusCode = statusCode;

            return response;
        }

        public static ObjectResult InvalidNameOrLastName(List<string> errorMessage)
        {
            var statusCode = 409;
            var response = new ObjectResult(new { errorMessage, statusCode });
            response.StatusCode = statusCode;

            return response;
        }

        public static ObjectResult InvalidBirthdate()
        {
            var errorMessage = "invalid-birthdate";
            var statusCode = 400;
            var response = new ObjectResult(new { errorMessage, statusCode });
            response.StatusCode = statusCode;

            return response;
        }

        internal static ActionResult<GetClientDto> EntityIsAlreadyInactive()
        {
            var errorMessage = "entity-already-inactive";
            var statusCode = 400;
            var response = new ObjectResult(new { errorMessage, statusCode });
            response.StatusCode = statusCode;

            return response;
        }
    }
}
