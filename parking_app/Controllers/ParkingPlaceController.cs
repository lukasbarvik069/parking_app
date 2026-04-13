using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using parking_app.Models;
using parking_app.Models.ValueObjects;
using parking_app.Repositories;
using parking_app.ViewModels;

namespace parking_app.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ParkingPlaceController : Controller
    {
        private readonly ParkingPlaceRepository _ppRepo;
        private readonly SpzRepository _spzRepo;
        private readonly SpzParkingPlaceRepository _sppRepo;
        private readonly UserRepository _uRepo;
        private readonly UserManager<User> _userManager;

        public ParkingPlaceController(ParkingPlaceRepository ppRepo,
            SpzRepository spzRepo,
            SpzParkingPlaceRepository spzParkingPlaceRepository,
            UserRepository uRepo,
            UserManager<User> userManager)
        {
            _ppRepo = ppRepo;
            _spzRepo = spzRepo;
            _userManager = userManager;
            _uRepo = uRepo;
            _sppRepo = spzParkingPlaceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var places = await _ppRepo.GetAll();
            return View(places);
        }

        [HttpGet]
        public async Task<IActionResult> BuySpot(int id)
        {
            await PrepareViewBag(id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuySpot(int id, BuySpotViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PrepareViewBag(id);
                return View(model);
            }

            var date = Date.Create(DateTime.Now, DateTime.Now.AddHours(model.Duration));
            if (date == null)
            {
                ModelState.AddModelError(String.Empty, "Začátek musí být dřív než konec");
                return View(model);
            }

            var spzParkingPlace = new SpzParkingPlace
            {
                Id = 0,
                ParkingPlaceId = id,
                SpzId = model.SpzId,
                Date = date,
                Price = model.Price,
            };

            var ok = await _sppRepo.Create(spzParkingPlace);

            if (ok != "ok")
            {
                ModelState.AddModelError(String.Empty, ok);
                await PrepareViewBag(id);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        private async Task PrepareViewBag(int ppId)
        {
            var userId = int.Parse(_userManager.GetUserId(User));
            ViewBag.spzs = await _spzRepo.GetFreeByUser(userId);
            ViewBag.ppName = await _ppRepo.GetName(ppId);
            ViewBag.ppPrice = await _ppRepo.GetPrice(ppId);
        }

        public async Task<IActionResult> MyParking()
        {
            var userId = int.Parse(_userManager.GetUserId(User));

            var user = await _uRepo.GetById(userId);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ParkingPlaces()
        {
            var pp = await _ppRepo.GetAllWithSPZ();

            return View(pp);
        }
    }
}
