using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroEquipamento.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IEquipamentoService, EquipamentoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IVinculoService, VinculoService>();
            return services;
        }
    }
}
