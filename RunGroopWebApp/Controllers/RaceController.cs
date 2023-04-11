using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        // GET: RaceController
        private readonly ApplicationDbContext _context;
        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Races.ToList();
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var model = _context.Races.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(model);
        }


    }
}
