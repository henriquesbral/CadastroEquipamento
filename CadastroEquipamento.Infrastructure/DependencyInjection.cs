using CadastroEquipamento.Infrastructure.DataAccess;
using CadastroEquipamento.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroEquipamento.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<DbConnectionFactory>();
            services.AddTransient<EquipamentoRepository>();
            services.AddTransient<UsuarioRepository>();
            services.AddTransient<VinculoRepository>();
            return services;
        }
    }
}