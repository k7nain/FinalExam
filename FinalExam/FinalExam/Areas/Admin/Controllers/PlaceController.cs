using FinalExam.Areas.Admin.ViewModels.Place;
using FinalExam.DAL;
using FinalExam.Models;
using FinalExam.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FinalExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Manager")]
    public class PlaceController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public PlaceController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Place> places = await _db.Places
                .Include(p => p.City)
                .ToListAsync();
            return View(places);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.City = await _db.Cities.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlaceVM placeVM)
        {
            ViewBag.City = await _db.Cities.ToListAsync();
            if (placeVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required");
                return View(placeVM);
            }
            else
            {
                if (!placeVM.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be an image");
                    return View(placeVM);
                }
                if (placeVM.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile size must be max 2MB");
                    return View(placeVM);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(placeVM);
            }

            Place place = new Place
            {
                Name = placeVM.Name,
                FullAddress = placeVM.FullAddress,
                Description = placeVM.Description,
                CityId = placeVM.CityId,
                ImageUrl = placeVM.ImageFile.SaveImage(_env, "uploads/places")
            };

            await _db.Places.AddAsync(place);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> HardDelete(int? id)
        {
            if (id is null) return NotFound();

            Place place = await _db.Places.FindAsync(id);

            if (place is null) return NotFound();

            place.ImageUrl.DeleteImage(_env, "uploads/places");

            _db.Places.Remove(place);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            if (id is null) return NotFound();

            Place place = await _db.Places.FindAsync(id);

            if (place is null) return NotFound();

            place.IsDeleted = true;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id is null) return NotFound();

            Place place = await _db.Places.FindAsync(id);

            if (place is null) return NotFound();

            place.IsDeleted = false;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.City = await _db.Cities.ToListAsync();

            if (id is null) return NotFound();

            Place place = await _db.Places.FindAsync(id);

            if (place is null) return NotFound();

            UpdatePlaceVM placeVM = new UpdatePlaceVM
            {
                Name = place.Name,
                FullAddress = place.FullAddress,
                Description = place.Description,
                CityId = place.CityId,
                ImageUrl = place.ImageUrl,
            };

            return View(placeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePlaceVM placeVM)
        {
            ViewBag.City = await _db.Cities.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(placeVM);
            }

            Place place = await _db.Places.FindAsync(placeVM.Id);

            if (place is null) return NotFound();

            place.Name = placeVM.Name;
            place.FullAddress = placeVM.FullAddress;
            place.Description = placeVM.Description;
            place.CityId = placeVM.CityId;

            if (placeVM.ImageFile is not null)
            {

                if (!placeVM.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be an image");
                    return View(placeVM);
                }
                if (placeVM.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile size must be max 2MB");
                    return View(placeVM);
                }

                place.ImageUrl.DeleteImage(_env, "uploads/places");
                place.ImageUrl = placeVM.ImageFile.SaveImage(_env, "uploads/places");
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
