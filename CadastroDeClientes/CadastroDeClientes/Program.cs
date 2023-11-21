using CadastroDeClientes.Context;
using CadastroDeClientes.Mapper;
using CadastroDeClientes.Service.Interfaces;
using CadastroDeClientes.Services.Client;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração de injeção de dependência do IClient na ClientService
builder.Services.AddScoped<IClient, ClientService>();

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
