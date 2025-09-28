using CadastroEquipamento.Infrastructure.Config;
using CadastroEquipamento.Infrastructure.DataAccess;
using CadastroEquipamento.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddTransient<DbConnectionFactory>();
        services.AddTransient<EquipamentoRepository>();
        services.AddTransient<UsuarioRepository>();
        services.AddTransient<VinculoRepository>();
        services.AddTransient<ApiUsuariosRepository>();

        services.Configure<EmailLogSettings>(configuration.GetSection("EmailLog"));
        services.AddSingleton<EmailRepository>();

        return services;
    }
}
