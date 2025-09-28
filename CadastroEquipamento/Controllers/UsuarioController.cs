using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CadastroEquipamento.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService service)
        {
            _usuarioService = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var usuarios = _usuarioService.ObterTodos();

                var viewModel = usuarios.Select(e => new UsuarioViewModel
                {
                    CodUsuario = e.CodUsuario,
                    Nome = e.Nome,
                    Email = e.Email,
                    Departamento = e.Departamento
                });

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Erro ao carregar usuarios: {ex.Message}";
                return View(Enumerable.Empty<UsuarioViewModel>());
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] UsuarioViewModel usuarios)
        {
            try
            {
                bool validarDados = ValidarUsuarios(usuarios);
                if (validarDados) 
                {
                    var usuario = new Usuario
                    {
                        Nome = usuarios.Nome,
                        Email = usuarios.Email,
                        Departamento = usuarios.Departamento
                    };

                    _usuarioService.Adicionar(usuario);

                    return Json(new { success = true, message = "Usuario criado com sucesso!" });
                }
                else
                {
                    return Json(new { success = false, message = "Erro ao criar Usuario" });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro ao criar Usuario: {ex.Message}" });
            }
        }

        [HttpPatch]
        public IActionResult Edit([FromBody] UsuarioViewModel usuarios)
        {
            try
            {
                var usuarioExistente = _usuarioService.ObterPorId(usuarios.CodUsuario);
                if (usuarioExistente == null)
                    return Json(new { success = false, message = "Usuario não encontrado!" });

                usuarioExistente.Nome = usuarios.Nome;
                usuarioExistente.Email = usuarios.Email;
                usuarioExistente.Departamento = usuarios.Departamento;                

                _usuarioService.Atualizar(usuarioExistente);

                return Json(new { success = true, message = "Usuario atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro ao atualizar Usuario: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Delete([FromBody] int id)
        {
            try
            {
                _usuarioService.Remover(id);
                return Json(new { success = true, message = "Usuário excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro ao excluir: {ex.Message}" });
            }
        }

        private bool ValidarUsuarios(UsuarioViewModel usuario)
        {
            var retorno = true;
            var regexEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if(!regexEmail.IsMatch(usuario.Email))
                retorno = false;

            if (usuario == null
                || string.IsNullOrEmpty(usuario.Nome)
                || string.IsNullOrEmpty(usuario.Email)
                || string.IsNullOrEmpty(usuario.Departamento)
                ) retorno = false;

            return retorno;
        }
    }
}
