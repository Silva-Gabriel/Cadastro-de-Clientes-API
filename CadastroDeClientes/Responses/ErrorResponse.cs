using Azure;
using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Responses
{
    public class ErrorResponse : ResultResponse
    {
        
        public static ObjectResult EntityNotFoundResponse()
        {
            IsSucess = false;
            var response = new ObjectResult(new { IsSucess, ErrorMessage = "entity-not-found", StatusCode = StatusCodes.Status404NotFound });
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;
        }

        public static ObjectResult UserAlreadyExists()
        {
            IsSucess = false;
            var response = new ObjectResult(new { IsSucess, ErrorMessage = "user-already-exists", StatusCode = StatusCodes.Status409Conflict });
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;

        }

        // TODO: Refatorar
        public static ObjectResult InvalidNameAndLastName(string errorMessage)
        {
            var errors = new string[2];
            var message_split = errorMessage.Split(' ');
            errors[0] = message_split[0];
            errors[1] = message_split[1];

            var statusCode = 400;
            var response = new ObjectResult(new { errors, statusCode });

            response.StatusCode = statusCode;

            return response;
        }

        // TODO: Refatorar
        public static ObjectResult InvalidNameOrLastName(List<string> errorMessage)
        {
            IsSucess = false;
            var response = new ObjectResult(new { IsSucess, errorMessage, StatusCode = StatusCodes.Status400BadRequest });
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;
        }

        public static ObjectResult InvalidBirthdate()
        {
            IsSucess = false;
            var response = new ObjectResult(new { IsSucess, ErrorMessage = "invalid-birthdate", StatusCode = StatusCodes.Status400BadRequest });
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;
        }

        public static ObjectResult EntityIsAlreadyInactive()
        {
            IsSucess = false;
            var response = new ObjectResult(new { IsSucess, ErrorMessage = "entity-is-inactive", StatusCode = StatusCodes.Status409Conflict });
            response.StatusCode = StatusCodes.Status409Conflict;

            return response;
        }
        public static ObjectResult CPFDigitsNotMatch()
        {
            IsSucess = false;
            var response = new ObjectResult(new { IsSucess, ErrorMessage = "cpf-check-digits-not-match", StatusCode = StatusCodes.Status400BadRequest });
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;
        }
    }
}
