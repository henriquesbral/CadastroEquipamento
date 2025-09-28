using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Web.Models;
using Microsoft.AspNetCore.Mvc;

public class VinculoController : Controller
{
    private readonly IVinculoService _vinculoService;
    private readonly IUsuarioService _usuarioService;
    private readonly IEquipamentoService _equipamentoService;
    private readonly IEmailService _emailService;

    public VinculoController(
        IVinculoService vinculoService,
        IUsuarioService usuarioService,
        IEquipamentoService equipamentoService,
        IEmailService emailService)
    {
        _vinculoService = vinculoService;
        _usuarioService = usuarioService;
        _equipamentoService = equipamentoService;
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            var vinculosDomain = _vinculoService.ObterTodos();
            var usuarios = _usuarioService.ObterUsuarioSemVinculo();
            var equipamentos = _equipamentoService.ObterEquipamentoSemVinculo();

            var vinculos = vinculosDomain.Select(v => new VinculoViewModel
            {
                EquipamentoId = v.CodEquipamento,
                UsuarioId = v.CodUsuario,
                NomeEquipamento = v.NomeEquipamento,
                NomeUsuario = v.NomeUsuario,
                DataVinculo = v.DataVinculo,
                UsuariosDisponiveis = usuarios.Select(u => new UsuarioDropdownViewModel
                {
                    CodUsuario = u.CodUsuario,
                    Nome = u.Nome
                }),
                EquipamentoDisponiveis = equipamentos.Select(e => new EquipamentoDropdownViewModel
                {
                    CodEquipamento = e.CodEquipamento,
                    Nome = e.Nome
                })
            }).ToList();

            return View(vinculos);
        }
        catch (Exception ex)
        {
            ViewData["Error"] = $"Erro ao carregar vínculos: {ex.Message}";
            return View(Enumerable.Empty<VinculoViewModel>());
        }
    }

    [HttpGet]
    public IActionResult UsuariosDisponiveis(int equipamentoId)
    {
        try
        {
            var usuarios = _usuarioService.ObterUsuarioSemVinculo();

            var result = usuarios.Select(u => new
            {
                codUsuario = u.CodUsuario,
                nome = u.Nome
            });

            return Json(new { success = true, data = result });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Erro ao buscar usuários: {ex.Message}" });
        }
    }

    [HttpPost]
    public IActionResult Vincular([FromBody] Vinculo vinculo)
    {
        try
        {
            var equipamento = _equipamentoService.ObterPorId(vinculo.CodEquipamento);
            var usuario = _usuarioService.ObterPorId(vinculo.CodUsuario);

            if (equipamento == null || usuario == null)
                return Json(new { success = false, message = "Usuário ou equipamento não encontrado." });

            _vinculoService.Vincular(new Vinculo
            {
                CodEquipamento = vinculo.CodEquipamento,
                CodUsuario = vinculo.CodUsuario,
                DataVinculo = DateTime.Now
            });

            _emailService.EnviarEmailVinculo(
                usuario.Email,
                usuario.Nome,
                equipamento.Nome,
                DateTime.Now
            );

            return Json(new
            {
                success = true,
                message = $"O usuário {usuario.Nome} foi vinculado ao equipamento {equipamento.Nome} com sucesso!"
            });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Erro ao vincular: {ex.Message}" });
        }
    }

    [HttpPost]
    public IActionResult Desvincular([FromBody] Vinculo vinculo)
    {
        try
        {
            var equipamento = _equipamentoService.ObterPorId(vinculo.CodEquipamento);
            var usuario = _usuarioService.ObterPorId(vinculo.CodUsuario);

            if (equipamento == null || usuario == null)
                return Json(new { success = false, message = "Usuário ou equipamento não encontrado." });

            _vinculoService.Desvincular(vinculo.CodEquipamento);

            return Json(new
            {
                success = true,
                message = $"O usuário {usuario.Nome} foi desvinculado do equipamento {equipamento.Nome} com sucesso!"
            });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Erro ao desvincular: {ex.Message}" });
        }
    }
}
