using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPronia.Areas.ProniaAdmin.ViewModels;
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
        public async Task<IActionResult> Create(CreateSlideVM slideVM)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!slideVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "File type is not correct");
                return View();
            }

            if (!slideVM.Photo.ValidateSize(FileSize.MB, 2))
            {
                ModelState.AddModelError("Photo", "File size is not correct");
                return View();
            }


            string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
            Slide slide = new Slide
            {
                Title = slideVM.Title,
                SubTitle = slideVM.SubTitle,
                Description = slideVM.Description,
                Order = slideVM.Order,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                ImageURL = fileName
            };

            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || id <= 0) return BadRequest();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) return NotFound();

            slide.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);

            if (slide == null) return NotFound();

            UpdateSlideVM updateSlideVM = new UpdateSlideVM
            {
                Title = slide.Title,
                SubTitle = slide.SubTitle,
                Description = slide.Description,
                ImageURL = slide.ImageURL,
                Order = slide.Order
            };

            return View(updateSlideVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateSlideVM updateSlideVM)
        {
            if (!ModelState.IsValid) return View(updateSlideVM);

            Slide existed = await _context.Slides.FirstOrDefaultAsync(c => c.Id == id);

            if (existed == null) return NotFound();

            if (updateSlideVM.Photo != null)
            {
                if (!updateSlideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(updateSlideVM.Photo), "Not a valid type");

                    return View(updateSlideVM);
                }
                if (!updateSlideVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(updateSlideVM.Photo), "Not a valid size");

                    return View(updateSlideVM);
                }
                string fileName = await updateSlideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
                existed.ImageURL.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
                existed.ImageURL = fileName;
            }

            existed.Title = updateSlideVM.Title;
            existed.Description = updateSlideVM.Description;
            existed.SubTitle = updateSlideVM.SubTitle;
            existed.Order = updateSlideVM.Order;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
