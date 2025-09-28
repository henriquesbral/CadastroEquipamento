using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Domain.Entities;
using CadastroEquipamento.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class EmailController : Controller
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        var logs = _emailService.ObterTodos();

        var viewModel = logs.Select(e => new LogEmailVinculoViewModel
        {
            NomeUsuario = e.NomeUsuario,
            NomeEquipamento = e.NomeEquipamento,
            TipoVinculo = e.Tipo,
            DataEnvioEmail = e.DataEnvioEmail
        });

        return View(viewModel);
    }
}
