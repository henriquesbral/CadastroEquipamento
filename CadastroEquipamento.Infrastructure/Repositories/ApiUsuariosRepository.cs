using CadastroEquipamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CadastroEquipamento.Infrastructure.Repositories
{
    public class ApiUsuariosRepository
    {
        private readonly HttpClient _httpClient;

        public ApiUsuariosRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ApiUser>> ListarUsuariosAsync()
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var usuarios = JsonSerializer.Deserialize<IEnumerable<ApiUser>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var result = usuarios ?? new List<ApiUser>();

            return result;
        }
    }
}
