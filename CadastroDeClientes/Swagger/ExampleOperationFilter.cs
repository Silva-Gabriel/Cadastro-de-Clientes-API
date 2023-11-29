using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CadastroDeClientes.Swagger
{
    public class ExampleOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if(context.ApiDescription.HttpMethod.ToUpper() == "POST" && context.ApiDescription.RelativePath == "api/Client")
            {

                if (operation.RequestBody?.Content == null || context.MethodInfo == null)
                {
                    return;
                }

                var example = new Dictionary<string, object>
                {
                    { "CPF", "395.259.438-52" },
                    { "Name", "Jean" },
                    { "LastName", "Carlos" },
                    { "Birthdate", new DateTime(2000, 7, 8)}
                };

                var mediaType = operation.RequestBody.Content.FirstOrDefault().Value;
                if (mediaType != null)
                {
                    mediaType.Example = new OpenApiObject
                    {
                        ["CPF"] = new OpenApiString((string)example["CPF"]),
                        ["Name"] = new OpenApiString((string)example["Name"]),
                        ["LastName"] = new OpenApiString((string)example["LastName"]),
                        ["Birthdate"] = new OpenApiDateTime((DateTime)example["Birthdate"])
                    };
                }
            }

            if (context.ApiDescription.HttpMethod.ToUpper() == "PUT" && context.ApiDescription.RelativePath == "api/Client/{id}")
            {

                if (operation.RequestBody?.Content == null || context.MethodInfo == null)
                {
                    return;
                }

                var example = new Dictionary<string, object>
            {
                { "Name", "Jean" },
                { "LastName", "Carlos" }
            };

                var mediaType = operation.RequestBody.Content.FirstOrDefault().Value;
                if (mediaType != null)
                {
                    mediaType.Example = new OpenApiObject
                    {
                        ["Name"] = new OpenApiString((string)example["Name"]),
                        ["LastName"] = new OpenApiString((string)example["LastName"])
                    };
                }
            }
        }
    }
}
