// Program.cs
using Microsoft.EntityFrameworkCore;
using ApiEmpresas.Data; // Referência ao nosso DbContext
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configuração dos Serviços (Service Registration) ---

// Configuração da String de Conexão do MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registro do serviço ApplicationDbContext para Injeção de Dependência
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mysqlOptions =>
        {
            // Especifica a assembly onde as classes de migração estão localizadas
            mysqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        }
    )
);

// Adiciona o suporte a Controllers e views
builder.Services.AddControllers();

// ** NOTA: O Swagger foi removido para evitar erros de dependência. 
// O teste será feito via Postman conforme solicitado. **


// --- 2. Configuração do Pipeline de Requisições (Middleware) ---

var app = builder.Build();

// Força o uso de HTTPS (Boa prática de segurança)
app.UseHttpsRedirection();

// Adiciona o middleware de Autorização (Necessário para segurança futura)
app.UseAuthorization();

// Mapeia os Controllers para as rotas da API (ex: /api/Empresas)
app.MapControllers();

app.Run();