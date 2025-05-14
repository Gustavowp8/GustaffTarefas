using System.Diagnostics;
using System.Security.Claims;
using GustaffTarefas.Data;
using GustaffTarefas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GustaffTarefas.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Context db;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(Context db, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var tarefas = db.Tarefas
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Prioridade) // <- Ordena por prioridade (Alta = 2 primeiro)
            .ThenBy(t => t.Vencimento)           // <- Depois por vencimento
            .ToList();

            return View(tarefas);
        }

        [HttpGet]
        public IActionResult Adiciona()
        {
            return View(new TarefaModel());
        }

        [HttpPost]
        public async Task<IActionResult> Adiciona(TarefaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Mostra os erros na mesma tela
            }

            try
            {
                model.UserId = _userManager.GetUserId(User); // ID do usuário logado
                db.Tarefas.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Pode exibir erro ou logar se quiser
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edita(int id)
        {
            var tarefa = db.Tarefas.FirstOrDefault(t => t.TarefaId == id);

            if (tarefa == null)
            {
                return NotFound();
            }

            // Garante que o usuário só edita suas próprias tarefas
            if (tarefa.UserId != _userManager.GetUserId(User))
            {
                return Unauthorized();
            }

            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Edita(TarefaModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var tarefaNoBanco = db.Tarefas.FirstOrDefault(t => t.TarefaId == model.TarefaId);

                if (tarefaNoBanco == null)
                {
                    return NotFound();
                }

                // Garante que o usuário só edita suas próprias tarefas
                if (tarefaNoBanco.UserId != _userManager.GetUserId(User))
                {
                    return Unauthorized();
                }

                // Atualiza os campos
                tarefaNoBanco.TarefaNome = model.TarefaNome;
                tarefaNoBanco.Descricao = model.Descricao;
                tarefaNoBanco.Vencimento = model.Vencimento;
                tarefaNoBanco.Prioridade = model.Prioridade;
                tarefaNoBanco.Feito = model.Feito;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Você pode logar o erro aqui se quiser
                ModelState.AddModelError("", "Erro ao editar a tarefa.");
                return View(model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
