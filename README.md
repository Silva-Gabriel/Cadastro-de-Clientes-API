<h1 align="center">API CADASTRO DE CLIENTES</h1>
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
