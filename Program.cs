using Microsoft.AspNetCore.Authentication.JwtBearer;
using Middleware.MiddlewareVerifyToken;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Registra os serviços necessários para Controllers
builder.Services.AddControllers();

// Adiciona o Swagger (opcional, útil para testes)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();

var app = builder.Build();

// Middleware de Swagger (opcional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ativa o roteamento para controllers
app.UseMiddleware<MiddlewareVerifyToken>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.UseOcelot();
app.Run();