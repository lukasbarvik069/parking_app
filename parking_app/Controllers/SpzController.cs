using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using parking_app.Models;
using parking_app.Repositories;
using parking_app.ViewModels;

namespace parking_app.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class SpzController : Controller
    {
        private readonly SpzRepository _repo;
        private readonly UserManager<User> _userManager;

        public SpzController(SpzRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId =  int.Parse(_userManager.GetUserId(User));
            var data = await _repo.GetByUser(userId);
            return View(data);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpzViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = int.Parse(_userManager.GetUserId(User));

            var newSpz = new Spz
            {
                Id = 0,
                UserId = userId,
                Name = model.Name,
            };

            var ok = await _repo.Create(newSpz);
            if (ok != "ok")
            {
                ModelState.AddModelError(String.Empty, ok);
                return View(model);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(_userManager.GetUserId(User));

            var ok = await _repo.Delete(id, userId);

            if (!ok)
            {
                ModelState.AddModelError(String.Empty, "SPZ se nepodařilo smazat");
            }
            
            return RedirectToAction("Index");
        }
    }
}
