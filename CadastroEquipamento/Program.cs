using CadastroEquipamento.Application;
using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Application.Services;
using CadastroEquipamento.Infrastructure;
using CadastroEquipamento.Infrastructure.Config;
using CadastroEquipamento.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Carrega configuração antes dos serviços
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Serviços
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHttpClient<ApiUsuariosRepository>();
builder.Services.AddScoped<ApiUsuariosService>();
builder.Services.Configure<EmailLogSettings>(builder.Configuration.GetSection("EmailLog"));

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
