using Microsoft.AspNetCore.Mvc;
using CadastroEquipamento.Application.Interfaces;
using CadastroEquipamento.Web.Models;
using CadastroEquipamento.Domain.Entities;

namespace CadastroEquipamento.Web.Controllers
{
    public class EquipamentoController : Controller
    {
        private readonly IEquipamentoService _service;

        public EquipamentoController(IEquipamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var equipamentos = _service.ObterTodos();

                var viewModel = equipamentos.Select(e => new EquipamentoViewModel
                {
                    CodEquipamento = e.CodEquipamento,
                    Nome = e.Nome,
                    NumeroSerie = e.NumeroSerie,
                    Tipo = e.Tipo,
                    DataAquisicao = e.DataAquisicao,
                    Status = e.Status
                });

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = $"Erro ao carregar equipamentos: {ex.Message}";
                return View(Enumerable.Empty<EquipamentoViewModel>());
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] EquipamentoViewModel equipamentos)
        {
            try
            {
                var equipamento = new Equipamento
                {
                    Nome = equipamentos.Nome,
                    NumeroSerie = equipamentos.NumeroSerie,
                    Tipo = equipamentos.Tipo,
                    DataAquisicao = equipamentos.DataAquisicao,
                    Status = equipamentos.Status
                };

                _service.Adicionar(equipamento);

                return Json(new { success = true, message = "Equipamento criado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro ao criar equipamento: {ex.Message}" });
            }
        }

        [HttpPatch]
        public IActionResult Edit([FromBody] EquipamentoViewModel equipamentos)
        {
            try
            {
                var equipamentoExistente = _service.ObterPorId(equipamentos.CodEquipamento);
                if (equipamentoExistente == null)
                    return Json(new { success = false, message = "Equipamento não encontrado!" });

                equipamentoExistente.Nome = equipamentos.Nome;
                equipamentoExistente.NumeroSerie = equipamentos.NumeroSerie;
                equipamentoExistente.Tipo = equipamentos.Tipo;
                equipamentoExistente.DataAquisicao = equipamentos.DataAquisicao;
                equipamentoExistente.Status = equipamentos.Status;

                _service.Atualizar(equipamentoExistente);

                return Json(new { success = true, message = "Equipamento atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro ao atualizar equipamento: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult Delete([FromBody] DeleteViewlModel request)
        {
            try
            {
                _service.Remover(request.codEquipamento);
                return Json(new { success = true, message = "Equipamento excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro ao excluir: {ex.Message}" });
            }
        }

    }
}
