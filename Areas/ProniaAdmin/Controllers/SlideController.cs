using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPronia.DAL;
using MVCPronia.Models;
using MVCPronia.Utilities.Enums;
using MVCPronia.Utilities.Extension;

namespace MVCPronia.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SlideController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.Where(s => !s.IsDeleted).ToListAsync();
            return View(slides);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!slide.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is not correct");
                return View();
            }

            if (!slide.Photo.ValidateSize(FileSize.MB, 2))
            {
                ModelState.AddModelError("Photo", "File size is not correct");
                return View();
            }


            slide.ImageURL = await slide.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
            slide.CreatedAt = DateTime.Now;

            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
