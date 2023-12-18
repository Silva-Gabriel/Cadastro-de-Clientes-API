using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Services.Client;
using CadastroDeClientesTest.Mock;
using FluentAssertions;

namespace CadastroDeClientesTest.Services.Client
{

    public class ClientServiceTest
    {
        private ClientService service = new ClientService();

        [Fact]
        public void CreateClient_isNotNull_returnFalse()
        {
            // Arrange
            CreateClientDto client = MockObjects.MockClient();
            
            // Act
            var method = service.IsClientNull(client);

            // Assert
            method.Should().BeFalse();
        }

        [Fact]
        public void CreateClient_isNull_returnTrue()
        {
            // Arrange
            CreateClientDto client = null;
            
            // Act
            var method = service.IsClientNull(client);

            // Assert
            method.Should().BeTrue();
        }

        [Theory]
        [InlineData("49045501880")]
        [InlineData("95959861003")]
        [InlineData("17444293082")]
        public void Cpf_IsValid_ReturnTrue(string cpf)
        {
            // Act
            var cpf_verifier = service.CPFDigitValidation(cpf);

            // Assert
            cpf_verifier.Should().BeTrue();
        }

        [Theory]
        [InlineData("49045501881")]
        [InlineData("95959861043")]
        [InlineData("17444293086")]
        public void Cpf_IsNotValid_ReturnFalse(string cpf)
        {
            // Act
            var cpf_verifier = service.CPFDigitValidation(cpf);

            // Assert
            cpf_verifier.Should().BeFalse();
        }
    }
}