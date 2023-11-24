using AutoMapper;
using CadastroDeClientes.Context;
using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Models;
using CadastroDeClientes.Responses;
using CadastroDeClientes.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CadastroDeClientes.Services.Client
{
    public class ClientService : IClient
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActionResult<ClientModel>> Create(CreateClientDto clientDto)
        {
            // Mapeia a entidade clientDto para ClientModel que é a classe principal com todos os campos
            var clientModel = _mapper.Map<ClientModel>(clientDto);

            // Retira os pontos e traços do CPF enviado no request
            var cpf = Regex.Replace(clientModel.CPF, "[.-]", "");
            // Faz a consulta do cpf na base para verificar se existem clientes cadastrados com ele
            var consultCPF = _context.Clients.Where(c => c.CPF == cpf).ToList();

            // Faz a validação do primeiro digito verificador
            var firstDigit = CPFDigitValidation(cpf.Substring(0, 9), 10);
            // Faz a validação do segundo digito verificador
            var secondDigit = CPFDigitValidation((cpf.Substring(0, 9) + firstDigit), 11);
            // Pega os dois digitos verificadores que vieram do request
            var digits_verifier = cpf.Substring(9, 2);
            // Pega os dois digitos verificadores gerados na validação
            var digits = (firstDigit + secondDigit).ToString();

            // Valida se a sequência de caracteres é válida para o nome
            var isValidSequenceOfName = isValidSequenceOfCaracteres(clientModel.Name);
            // Valida se a sequência de caracteres é válida para o sobrenome
            var isValidSequenceOfLastName = isValidSequenceOfCaracteres(clientModel.LastName);
            // Valida se a quantidade de caracteres repetidos é válida para o nome
            var isValidRepetitionOfName = isValidMaximumLettersRepetition(clientModel.Name);
            // Valida se a quantidade de caracteres repetidos é válida para o sobrenome
            var isValidRepetitionOfLastName = isValidMaximumLettersRepetition(clientModel.LastName);

            // Lista para agregar os erros de nome e sobrenome respectivamente
            var name_validation_errors = new List<string>();

            // Verifica se o cliente já existe
            if (clientModel == null)
            {
                return ErrorResponse.EntityNotFoundResponse();
            }

            // Validação de CPF
            if (digits_verifier != digits)
                throw new Exception("Os digitos verificadores não batem! Por favor, entre com um CPF válido!");

            if (consultCPF.Count >= 1)
                return ErrorResponse.UserAlreadyExists();

            // Coloca o mesmo cpf sem a formatação de pontos e traços no request
            clientModel.CPF = cpf;

            // Validação de nome e sobrenome
            if (isValidSequenceOfName || isValidRepetitionOfName)
                name_validation_errors.Add("invalid-field-name");

            if (isValidSequenceOfLastName || isValidRepetitionOfLastName)
                name_validation_errors.Add("invalid-field-lastname");

            if(name_validation_errors.Count > 0)
                return ErrorResponse.InvalidNameOrLastName(name_validation_errors);

            // Validação de data
            if (clientModel.Birthdate.Date >= DateTime.Now.Date)
                return ErrorResponse.InvalidBirthdate();

            _context.Add(clientModel);
            await _context.SaveChangesAsync();

            // Mapeia a entidade clientModel para um modelo de resposta CreateClientDto
            var response = _mapper.Map<CreateClientDto>(clientModel);

            // retorna um objeto de resposta personalizado
            return SucessResponse.CreateResponse(response);
        }

        public async Task<ActionResult<ClientModel>> Delete(long id)
        {
            var client = await _context.Clients.Where(c => c.Id == id)
                    .FirstOrDefaultAsync();

            if (client == null)
            {
                return ErrorResponse.EntityNotFoundResponse();
            }

            _context.Remove(client);
            await _context.SaveChangesAsync();

            return client;
        }

        public async Task<ActionResult<GetClientDto>> Edit(long id, EditClientDto clientDto)
        {
            var client = _context.Clients.Find(id);
            var clientMap = _mapper.Map<ClientModel>(clientDto);

            var isValidSequenceOfName = isValidSequenceOfCaracteres(clientDto.Name);
            var isValidSequenceOfLastName = isValidSequenceOfCaracteres(clientDto.LastName);
            var isValidRepetitionOfName = isValidMaximumLettersRepetition(clientDto.Name);
            var isValidRepetitionOfLastName = isValidMaximumLettersRepetition(clientDto.LastName);
            var name_validation_errors = new List<string>();

            if (client == null)
                return ErrorResponse.EntityNotFoundResponse();

            // Dados a serem modificados
            if (!string.IsNullOrEmpty(clientMap.Name))
            {
                if (isValidSequenceOfName || isValidRepetitionOfName)
                    name_validation_errors.Add("invalid-field-name");
            }

            if (!string.IsNullOrEmpty(clientMap.LastName))
            {
                if (isValidSequenceOfLastName || isValidRepetitionOfLastName)
                    name_validation_errors.Add("invalid-field-lastname");
            }

            if (name_validation_errors.Count > 0)
                return ErrorResponse.InvalidNameOrLastName(name_validation_errors);

            client.Name = clientMap.Name;
            client.LastName = clientMap.LastName;

            if (clientMap.Birthdate != DateTime.MinValue)
            {
                if (clientDto.Birthdate.Date >= DateTime.Now.Date)
                    return ErrorResponse.InvalidBirthdate();

                client.Birthdate = clientMap.Birthdate;
            }

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetClientDto>(client);

            return SucessResponse.OkResponse(response);
        }

        public ActionResult<GetClientDto> Get(long id)
        {
            var client = _context.Clients.Find(id);

            if (client == null)
                return ErrorResponse.EntityNotFoundResponse();

            var response = _mapper.Map<GetClientDto>(client);

            return SucessResponse.OkResponse(response);
        }

        public async Task<ActionResult<List<GetClientDto>>> GetAll()
        {
            var clients = await _context.Clients.ToListAsync();

            var response = _mapper.Map<List<GetClientDto>>(clients);

            return SucessResponse.OkListResponse(response);
        }

        public async Task<ActionResult<GetClientDto>> Inactive(long id)
        {
            ClientModel client = _context.Clients.Find(id);

            if (client == null)
                return ErrorResponse.EntityNotFoundResponse();

            if (client.Status == 0)
                return ErrorResponse.EntityIsAlreadyInactive();

            client.Status = 0;

            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<GetClientDto>(client);

            return SucessResponse.OkResponse(response);
        }

        static string CPFDigitValidation(string cpf, int number)
        {
            var cpf_validation = cpf.ToArray();

            var digits = CharToInt(cpf_validation);
            var result = 0;

            foreach (var digit in digits)
            {
                result = result + digit * number;
                Console.WriteLine($"{number} x {digit} = {number * digit} > {result}");
                number--;
            }

            Console.WriteLine("Verificação Primeiro Digito: " + result);
            result = result % 11;

            if (result == 1 || result == 0)
                return "0";
            else
                result = 11 - result;

            return result.ToString();
        }

        static List<int> CharToInt(char[] charArray)
        {
            var digits = new List<int>();
            int validDigit = 0;

            foreach (char digit in charArray)
            {
                if (char.IsDigit(digit))
                {

                    validDigit = digit - '0';
                }

                digits.Add(validDigit);
            }

            return digits;
        }

        static bool isValidNumberOfWords(string word)
        {
            var regex_code_two_group = @"\b(\w+)\1\b";
            var regex_code_three_group = @"\b(\w+)\1\1\b";

            if (Regex.IsMatch(word, regex_code_two_group))
            {
                return false;
            }
            else if (Regex.IsMatch(word, regex_code_three_group))
            {
                return false;
            }

            return true;
        }
        static bool isValidSequenceOfCaracteres(string word)
        {
            var sequence_keyboard = "qwertyuiopasdfghjklçzxcvbnmQWERTYUIOPASDFGHJKLÇZXCVBNM";
            var sequence_alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZáéíóúàâêôãõüçÁÉÍÓÚÀÂÊÔÃÕÜÇ";

            Console.WriteLine(word.Length);
            for (int i = 0; i < word.Length - 2; i++)
            {
                var substring = word.Substring(i, 3);

                if (sequence_keyboard.Contains(substring) || sequence_alphabet.Contains(substring))
                {
                    return true;
                }

                Console.WriteLine(i);
            }
            return false;
        }

        static bool isValidMaximumLettersRepetition(string word) {
            var regex_code = @"(.)\1{2,}";

            if (Regex.IsMatch(word, regex_code))
                return true;

            return false;
        }
    }
}
