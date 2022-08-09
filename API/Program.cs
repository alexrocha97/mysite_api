using API.Application.Applications;
using API.Application.Interfaces;
using API.Domain.Interfaces;
using API.Domain.Interfaces.Generics;
using API.Domain.Interfaces.InterfacesServices;
using API.Domain.Models;
using API.Domain.Service;
using API.Infra.Configuration;
using API.Infra.Repository;
using API.Infra.Repository.Generics;
using API.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ADICIONANDO O CORS
builder.Services.AddCors();

builder.Services
    .AddDbContext<Contexto>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.
    AddDefaultIdentity<ApplicationUser>
        (options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<Contexto>();

// INTERFACE E REPOSITORIO
builder.Services.AddSingleton(typeof(IGenerics<>), (typeof(RepositoryGenerics<>)));
builder.Services.AddSingleton<INoticia, RepositoryNoticia>();
builder.Services.AddSingleton<IUsuario, RepositoryUsuario>();

// SERVIÇO DOMINIO
builder.Services.AddSingleton<INoticiaServico, NoticiaService>();


// INTERFACE APLICAÇÃO
builder.Services.AddSingleton<IApplicationNoticia, ApplicationNoticia>();
builder.Services.AddSingleton<IApplicationUsuario, ApplicationUsuario>();

// CONFIGURATION JWT

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "Teste.Securiry.Bearer",
        ValidAudience = "Teste.Securiry.Bearer",
        IssuerSigningKey = JsonWebTokenSecurity.Create("Secret_Key-12345678")
    };

    option.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
            return Task.CompletedTask;
        }
    };
});

// FIM

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DANDO ACESSO AO CLIENTE A ACESSAR API OU IMPLEMENTAR O FRONT
var Cliente01 = "https://google.com.br"; // EXEMPLO

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(b => b.WithOrigins(Cliente01));

app.UseHttpsRedirection();

// ADICIONANDO PARA USAR A AUTORIZAÇÃO
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
