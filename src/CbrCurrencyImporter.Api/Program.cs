using Microsoft.AspNetCore.Builder;
using CbrCurrencyImporter.Api.Middleware;
using CbrCurrencyImporter.Application;
using CbrCurrencyImporter.Application.Internal;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервисы
builder.Services.AddHttpClient<ICbrCurrencyParser, CbrCurrencyParser>();
builder.Services.AddControllers();

var app = builder.Build();

// Добавляем middleware для обработки исключений
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();