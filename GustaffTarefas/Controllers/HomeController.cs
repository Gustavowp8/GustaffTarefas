using System.Diagnostics;
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
                .OrderBy(t => t.Vencimento)
                .ToList();

            return View(tarefas);
        }

        [HttpGet]
        public IActionResult Adiciona()
        {
            return View();
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

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
