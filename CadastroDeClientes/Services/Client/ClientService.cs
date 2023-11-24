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
        public async Task<ActionResult<GetClientDto>> Create(CreateClientDto clientDto)
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
            var isValidSequenceOfName = IsValidSequenceOfCaracteres(clientModel.Name);
            // Valida se a sequência de caracteres é válida para o sobrenome
            var isValidSequenceOfLastName = IsValidSequenceOfCaracteres(clientModel.LastName);
            // Valida se a quantidade de caracteres repetidos é válida para o nome
            var isValidRepetitionOfName = IsValidMaximumLettersRepetition(clientModel.Name);
            // Valida se a quantidade de caracteres repetidos é válida para o sobrenome
            var isValidRepetitionOfLastName = IsValidMaximumLettersRepetition(clientModel.LastName);
            
            // Valida a quantidade de palavras no nome
            var isValidNumberOfWordsName = IsValidNumberOfWords(clientModel.Name);
            // Valida a quantidade de palavras no sobrenome
            var isValidNumberOfWordsLastName = IsValidNumberOfWords(clientModel.LastName);

            // Lista para agregar os erros de nome e sobrenome respectivamente
            var name_validation_errors = new List<string>();

            // Verifica se o cliente já existe
            if (clientModel == null)
            {
                return ErrorResponse.EntityNotFoundResponse();
            }

            // Validação de CPF
            if (digits_verifier != digits)
                return ErrorResponse.CPFDigitsNotMatch();

            if (consultCPF.Count >= 1)
                return ErrorResponse.UserAlreadyExists();

            // Coloca o mesmo cpf sem a formatação de pontos e traços no request
            clientModel.CPF = cpf;

            // Validação de nome e sobrenome
            if (isValidSequenceOfName || isValidRepetitionOfName || isValidNumberOfWordsName)
                name_validation_errors.Add("invalid-field-name");

            if (isValidSequenceOfLastName || isValidRepetitionOfLastName || isValidNumberOfWordsLastName)
                name_validation_errors.Add("invalid-field-lastname");

            if(name_validation_errors.Count > 0)
                return ErrorResponse.InvalidNameOrLastName(name_validation_errors);

            // Validação de data
            if (clientModel.Birthdate.Date >= DateTime.Now.Date)
                return ErrorResponse.InvalidBirthdate();

            _context.Add(clientModel);
            await _context.SaveChangesAsync();

            // retorna um objeto de resposta personalizado
            return SucessResponse.CreateResponse(clientModel);
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
            
            var name_validation_errors = new List<string>();

            if (client == null)
                return ErrorResponse.EntityNotFoundResponse();

            // Dados a serem modificados
            if (!string.IsNullOrEmpty(clientMap.Name))
            {
                var isValidSequenceOfName = IsValidSequenceOfCaracteres(clientDto.Name);
                var isValidRepetitionOfName = IsValidMaximumLettersRepetition(clientDto.Name);
                var isValidNumberOfWordsName = IsValidNumberOfWords(clientDto.Name);

                if (isValidSequenceOfName || isValidRepetitionOfName || isValidNumberOfWordsName)
                    name_validation_errors.Add("invalid-field-name");
            }

            if (!string.IsNullOrEmpty(clientMap.LastName))
            {
                var isValidSequenceOfLastName = IsValidSequenceOfCaracteres(clientDto.LastName);
                var isValidRepetitionOfLastName = IsValidMaximumLettersRepetition(clientDto.LastName);
                var isValidNumberOfWordsLastName = IsValidNumberOfWords(clientDto.LastName);

                if (isValidSequenceOfLastName || isValidRepetitionOfLastName || isValidNumberOfWordsLastName)
                    name_validation_errors.Add("invalid-field-lastname");
            }

            if (name_validation_errors.Count > 0)
                return ErrorResponse.InvalidNameOrLastName(name_validation_errors);

            if(clientDto.Name != null)
                client.Name = clientMap.Name;

            if (clientDto.LastName != null)
                client.LastName = clientMap.LastName;

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

        public async Task<ActionResult<List<ClientModel>>> GetAllFullAcess()
        {
            var clients = await _context.Clients.ToListAsync();

            return SucessResponse.OkListFulAccessResponse(clients);
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

        static bool IsValidNumberOfWords(string word)
        {
            var regex_code_two_group = new Regex(@"\b(\w+)\1\b", RegexOptions.IgnoreCase);
            var regex_code_three_group = new Regex(@"\b(\w+)\1\1\b", RegexOptions.IgnoreCase);

            if (regex_code_two_group.IsMatch(word) || regex_code_three_group.IsMatch(word))
                return true;

            return false;
        }
        static bool IsValidSequenceOfCaracteres(string word)
        {
            var sequence_keyboard = "qwertyuiopasdfghjklçzxcvbnm";
            var sequence_alphabet = "abcdefghijklmnopqrstuvwxyzáéíóúàâêôãõüç";

            for (int i = 0; i < word.Length - 3; i++)
            {
                var substring = word.Substring(i, 4).ToLower();

                if (sequence_keyboard.Contains(substring) || sequence_alphabet.Contains(substring))
                {
                    return true;
                }

                Console.WriteLine(i);
            }
            return false;
        }

        static bool IsValidMaximumLettersRepetition(string word) {
            var regex_code = @"(.)\1{2,}";

            if (Regex.IsMatch(word, regex_code))
                return true;

            return false;
        }
    }
}
