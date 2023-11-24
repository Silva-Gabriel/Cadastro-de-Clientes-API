<h1 align="center">API CADASTRO DE CLIENTES</h1>
<p>! No momento alguns itens do checklist não estão em ordem correta</p>
<pre>
    ✅ - Criar as classes Service, Models e AppDbContext
    ✅ - Criar classe Enum para um atributo da Model Client "StatusClient"
    ✅ - Validação de Data Annotations
    ✅ - Adicionar configurações do DbContext na classe Program
    ✅ - Criar o migration inicial
    ✅ - Criar a regra de negócio básica para funcionamento dos endpoints na classe Service
    ✅ - Chamar a classe Service a partir da classe Controller para controle das requisições
    ✅ - Testar as chamadas via Insomnia/Swagger
    ✅ - Criar uma classe Mapper para fazer a conversão dos modelos de resposta padrão para dtos personalizados
    ✅ - Criar os Dtos
    ✅ - Criar uma classe SucessResponse e ErrorResponse para lidar com os retornos das chamadas personalizados
    ✅ - Validação de digitos verificadores no CPF
    ✅ - Validação de dados obrigatórios
    ✅ - Validar se o cliente já existe na base a partir do cpf
    ✅ - Validar caracteres consecutivos ex: alesssandro (3 "s"), annna (3 "n") etc.
    ✅ - Validatar palavras repetidas ex: gabrielgabriel, gabrielgabrielgabriel, testtest, marcomarcomarcos
    ✅ - Validar sequencia de caracteres: alfabeto ex: abc, defg, gfed, xyz, mnopqrs, e para letras do teclado: qwer,asdfg,zxcvb, ewq, fdsa, cxzerty,hjklmnbv, se aparecer um sequencial desses caracteres em 3 seguidos em qualquer parte da string, ela será validada.
    ✘ - Criação dos dados históricos de inclusão(id, usuário,datahora,etapa, objeto dos dados)
    ✘ - Chamadas de API's externas para fazer validações nos dados;
    ✘ - Configuração IBM MQ no .NET 6
    ✘ - Conexão e envio de chamadas para o IBM MQ
    ✘ - Criação de um worker para ouvir o que chega no IBM MQ e enviar para o banco de dados
</pre>

<h3>API Swagger</h3>
<img width="816" alt="image" src="https://github.com/Silva-Gabriel/Cadastro-de-Clientes-API/assets/69408374/91fcd708-07de-4bd7-84b8-6df26fbac69d">
<h3>API Schemas</h3>
<img width="806" alt="image" src="https://github.com/Silva-Gabriel/Cadastro-de-Clientes-API/assets/69408374/247edb0c-07c8-42ca-94d3-60bc699b5d26">
<h4>Main Endpoint: <a href="">http://localhost:xxxx/api/Client</a></h4>
<h2>Exemplos de chamadas</h2>
<h3 align="center">Create</h3>
<pre>
    <code>
        {
            "cpf": "xxxxxxxxxxx",
            "name": "GSS",
            "lastName": "Dev",
            "birthdate": "2000-07-08",
            "status": 1,
            "phones": [
              {
                "phoneNumber": "xxxxxxxxx",
                "ddd": "11",
                "countryCode": "55"
              }
            ],
            "emails": [
              {
                "mainEmailAddress": "gabriel@gmail.com",
                "alternativeEmailAddress": "gssdev@gmail.com"
              }
            ],
            "addresses": [
              {
                "country": "Brasil",
                "region": "São Paulo",
                "city": "Guarulhos",
                "postalCode": "07144575"
              },
             {
                "country": "Brasil",
                "region": "São Paulo Litoral",
                "city": "Itanhaém",
                "postalCode": "xxxxxxxx"
             }
            ]
          }
    </code>
</pre>
