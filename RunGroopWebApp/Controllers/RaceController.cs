using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using RunGroopWebApp.Services;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        // GET: RaceController
        private readonly ApplicationDbContext _context;
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;

        public RaceController(ApplicationDbContext context, 
            IRaceRepository raceRepository,
            IPhotoService photoService
            )
        {
            _context = context;
            this._raceRepository = raceRepository;
            this._photoService = photoService;
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
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    //AppUserId = raceVM.AppUserId,
                    RaceCategory = raceVM.RaceCategory,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(raceVM);
        }

    }
}
