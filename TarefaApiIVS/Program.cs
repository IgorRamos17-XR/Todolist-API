// Program.cs
using Microsoft.EntityFrameworkCore;
using TarefaApiIVS.Data; // Ajuste o namespace
// using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Não é mais necessário importar ServerVersion diretamente aqui com EF Core 6+


// Aqui criamos o objeto responsável por configurar toda a aplicação ASP.NET Core.
var builder = WebApplication.CreateBuilder(args); // O builder é utilizado para registrar, serviços, Banco de Dados, Swagger etc.


// Add services to the container.


// 1. Configurar CORS (Cross-Origin Resource Sharing) mecanismo de segurança dos navegadores.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";// Variável contendo o nome da política de CORS, (_myAllowSpecificOrigins) 
builder.Services.AddCors(options =>
{

    //options.AddPolicy = Cria uma política de acesso
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy => // Define as regras de acesso.
                      {
                          policy.WithOrigins("http://localhost:8080", // Para o cliente web rodando localmente
                                             "http://127.0.0.1:8080") // Outra forma de localhost
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// 2. Configurar o DbContext para MySQL
// Essa parte aqui faz a leitura do arquivo appsettings.json, para conexão.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Para .NET 6 e superior, ServerVersion.AutoDetect é o padrão se não especificado explicitamente para Pomelo.
// Se precisar de mais controle ou estiver usando uma versão mais antiga do Pomelo, pode ser necessário:
// builder.Services.AddDbContext<TarefaContext>(options =>
//     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
// );

builder.Services.AddDbContext<TarefaContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 27))) // Especifique sua versão do MySQL Server
);


builder.Services.AddControllers();// Habilita o uso de Controllers.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();// Permite que o Swagger descubra automaticamente os endpoints da API.
builder.Services.AddSwaggerGen();

var app = builder.Build();// Cria a aplicação pronta para execução.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())// Verifica se a aplicação está rodando em ambiente de desenvolvimento.
{
    app.UseSwagger();//  Gera o arquivo JSON da documentação.
    app.UseSwaggerUI();// Cria a página visual.
}

app.UseHttpsRedirection();

// 3. Usar CORS - DEVE VIR ANTES DE UseAuthorization e MapControllers
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();