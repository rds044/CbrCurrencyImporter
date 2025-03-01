using CbrCurrencyImporter.Api.Middleware;
using CbrCurrencyImporter.Application;
using CbrCurrencyImporter.Application.Internal;
using Microsoft.EntityFrameworkCore;
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ICbrCurrencyParser, CbrCurrencyParser>();
builder.Services.AddControllers();

builder.Services.AddDbContext<CurrencyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();
app.MapControllers();

var webAppTask = app.RunAsync();

Console.WriteLine("Web server is running. Press 'exit' to stop.");
while (true)
{
    var input = Console.ReadLine();
    if (input?.ToLower() == "exit")
    {
        break;
    }
}

await webAppTask;