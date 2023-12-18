using CadastroDeClientes.Dtos.Client;

namespace CadastroDeClientesTest.Mock
{
    public class MockObjects
    {
        public static CreateClientDto MockClient() 
        {
            return new CreateClientDto
            {
                Name = "Josefa",
                LastName = "Das Colves",
                CPF = "644.541.110-06",
                Birthdate = DateTime.Now
            };
        }
    }
}
