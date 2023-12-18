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
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ClientService(){}
        public async Task<ActionResult<GetClientDto>> Create(CreateClientDto clientDto)
        {
            // Verifica se o cliente está nulo
            if (IsClientNull(clientDto))
                return ErrorResponse.EntityNotFoundResponse();
            
            // Mapeia a entidade clientDto para ClientModel que é a classe principal com todos os campos
            var clientModel = _mapper.Map<ClientModel>(clientDto);

            // Retira os pontos e traços do CPF enviado no request
            var cpf = Regex.Replace(clientModel.CPF, "[.-]", "");
            // Faz a consulta do cpf na base para verificar se existem clientes cadastrados com ele
            var consultCPF =  await _context.Clients.Where(c => c.CPF == cpf).ToListAsync();

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

            // Validação de CPF
            if (!CPFDigitValidation(cpf))
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
            return SucessResponse<ClientModel>.Created(clientModel);
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

            return SucessResponse<GetClientDto>.Ok(response);
        }

        public ActionResult<GetClientDto> Get(long id)
        {
            var client = _context.Clients.Find(id);

            if (client == null)
                return ErrorResponse.EntityNotFoundResponse();

            var response = _mapper.Map<GetClientDto>(client);

            return SucessResponse<GetClientDto>.Ok(response);
        }

        public async Task<ActionResult<List<GetClientDto>>> GetAll()
        {
            var clients = await _context.Clients.ToListAsync();

            var response = _mapper.Map<List<GetClientDto>>(clients);

            return SucessResponse<List<GetClientDto>>.Ok(response);
        }

        public async Task<ActionResult<List<ClientModelDto>>> GetAllFullAcess()
        {
            var clients = await _context.Clients.ToListAsync();
            var response = _mapper.Map<List<ClientModelDto>>(clients);

            return SucessResponse<List<ClientModelDto>>.Ok(response);
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

            return SucessResponse<GetClientDto>.Ok(response);
        }

        public bool IsClientNull(CreateClientDto clientDto)
        {
            return clientDto == null;
        }

        public bool CPFDigitValidation(string cpf)
        {
            var digits_verifier = cpf.Substring(9, 2);
            string result = "";

            for(int i = 1; i <= 2; i++)
            {
                if(i == 1)
                {
                    var cpfWithoutCheckDigitsInt = CharToInt(cpf[..9].ToArray());
                    var cpfWithoutCheckFirstDigit = cpf[..9].ToArray().Count() + 1;
                    var total = 0;

                    foreach (var digit in cpfWithoutCheckDigitsInt)
                    {
                        total = total + digit * cpfWithoutCheckFirstDigit;
                        cpfWithoutCheckFirstDigit--;
                    }

                    total = total % 11;

                    if (total == 1 || total == 0)
                        total = 0;
                    else
                        total = 11 - total;
                        
                    result = result + total.ToString();

                } 
                else{
                    var cpfWithoutCheckDigitsInt = CharToInt((cpf[..9] + result).ToArray());
                    var cpfWithoutCheckSecondDigit = (cpf[..9] + result).ToArray().Count() + 1;
                    var total = 0;

                    foreach (var digit in cpfWithoutCheckDigitsInt)
                    {
                        total = total + digit * cpfWithoutCheckSecondDigit;
                        cpfWithoutCheckSecondDigit--;
                    }

                    total = total % 11;

                    if (total == 1 || total == 0)
                        total = 0;
                    else
                        total = 11 - total;
                        
                    result = result + total.ToString();
                }
            }
            return result == digits_verifier;
        }

        public static List<int> CharToInt(char[] charArray)
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

        public static bool IsValidNumberOfWords(string word)
        {
            var regex_code_two_group = new Regex(@"\b(\w+)\1\b", RegexOptions.IgnoreCase);
            var regex_code_three_group = new Regex(@"\b(\w+)\1\1\b", RegexOptions.IgnoreCase);

            if (regex_code_two_group.IsMatch(word) || regex_code_three_group.IsMatch(word))
                return true;

            return false;
        }
        public static bool IsValidSequenceOfCaracteres(string word)
        {
            const string sequence_keyboard = "qwertyuiopasdfghjklçzxcvbnm";
            const string sequence_alphabet = "abcdefghijklmnopqrstuvwxyzáéíóúàâêôãõüç";

            for (int i = 0; i < word.Length - 3; i++)
            {
                var substring = word.Substring(i, 4).ToLower();

                if (sequence_keyboard.Contains(substring) || sequence_alphabet.Contains(substring))
                {
                    return true;
                }

                // Console.WriteLine(i);
            }
            return false;
        }

        public static bool IsValidMaximumLettersRepetition(string word) {
            var regex_code = @"(.)\1{2,}";

            if (Regex.IsMatch(word, regex_code))
                return true;

            return false;
        }
    }
}
