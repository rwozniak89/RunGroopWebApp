using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        // GET: RaceController
        private readonly ApplicationDbContext _context;
        private readonly IRaceRepository _raceRepository;

        public RaceController(ApplicationDbContext context, IRaceRepository raceRepository)
        {
            _context = context;
            this._raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _raceRepository.GetAll();
            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _raceRepository.GetByIdAsync(id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            _raceRepository.Add(race);
            return RedirectToAction("Index");

        }
    }
}
