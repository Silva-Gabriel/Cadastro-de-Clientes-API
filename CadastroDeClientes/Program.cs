using CadastroDeClientes.Context;
using CadastroDeClientes.Dtos.Client;
using CadastroDeClientes.Mapper;
using CadastroDeClientes.Models.SubModelCliente;
using CadastroDeClientes.Service.Interfaces;
using CadastroDeClientes.Services.AlternativeEmail;
using CadastroDeClientes.Services.Client;
using CadastroDeClientes.Services.Email;
using CadastroDeClientes.Services.Interfaces;
using CadastroDeClientes.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cadastro de Clientes API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "GSS Dev",
            Email = "gssdev@email.com"
        }
    });

    c.OperationFilter<ExampleOperationFilter>();

    var xmlFile = "CadastroDeClientes.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Configuração de injeção de dependência do IClient na ClientService
builder.Services.AddScoped<IClient, ClientService>();
builder.Services.AddScoped<IEmail, EmailService>();
builder.Services.AddScoped<IAlternativeEmail, AlternativeEmailService>();

// Configuração AutoMapper
builder.Services.AddAutoMapper(typeof(EntitiesToDtoProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
