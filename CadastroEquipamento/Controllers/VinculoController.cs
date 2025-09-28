using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Application.Services;
using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Web.Models;
using Microsoft.AspNetCore.Mvc;

public class VinculoController : Controller
{
    private readonly IVinculoService _vinculoService;
    private readonly IUsuarioService _usuarioService;
    private readonly IEquipamentoService _equipamentoService;

    public VinculoController(IVinculoService vinculoService, IUsuarioService usuarioService, IEquipamentoService equipamentoService)
    {
        _vinculoService = vinculoService;
        _usuarioService = usuarioService;
        _equipamentoService = equipamentoService;
    }

    public IActionResult Index()
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

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json(new List<object>());
        }
    }

    [HttpPost]
    public IActionResult Vincular([FromBody] Vinculo vinculo)
    {
        _vinculoService.Vincular(vinculo.CodEquipamento, vinculo.CodUsuario);
        //_emailService.EnviarEmail();
        return Ok();
    }

    [HttpPost]
    public IActionResult Desvincular([FromBody] Vinculo vinculo)
    {
        _vinculoService.Desvincular(vinculo.CodEquipamento);
        return Ok();
    }
}
