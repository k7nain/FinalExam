using FinalExam.Areas.Admin.ViewModels.City;
using FinalExam.DAL;
using FinalExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Manager")]
    public class CityController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public CityController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<City> Cities = await _db.Cities.ToListAsync();
            return View(Cities);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCityVM cityVM)
        {
            if (!ModelState.IsValid)
            {
                return View(cityVM);
            }

            City city = new City
            {
                Name = cityVM.Name,
            };

            await _db.Cities.AddAsync(city);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> HardDelete(int? id)
        {
            if (id is null) return NotFound();

            City city = await _db.Cities.FindAsync(id);

            if (city is null) return NotFound();


            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id is null) return NotFound();

            City city = await _db.Cities.FindAsync(id);

            if (city is null) return NotFound();

            city.IsDeleted = true;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id is null) return NotFound();

            City city = await _db.Cities.FindAsync(id);

            if (city is null) return NotFound();

            city.IsDeleted = false;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return NotFound();

            City city = await _db.Cities.FindAsync(id);

            if (city is null) return NotFound();

            UpdateCityVM cityVM = new UpdateCityVM
            {
                Name = city.Name,
            };

            return View(cityVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCityVM cityVM)
        {

            if (!ModelState.IsValid)
            {
                return View(cityVM);
            }

            City City = await _db.Cities.FindAsync(cityVM.Id);

            if (City is null) return NotFound();

            City.Name = cityVM.Name;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
