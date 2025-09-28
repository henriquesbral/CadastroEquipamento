using CadastroEquipamento.Application;
using CadastroEquipamento.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Carrega configuração antes dos serviços
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Teste da connection string
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(conn))
    throw new Exception("Connection string 'DefaultConnection' não encontrada!");

Console.WriteLine($"Connection String carregada: {conn}");

// Serviços
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
