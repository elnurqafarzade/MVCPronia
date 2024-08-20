using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPronia.DAL;
using MVCPronia.Models;

namespace MVCPronia.Areas.ProniaAdmin.Controllers
{

    [Area("ProniaAdmin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Tag> categories = await _context.Tags.ToListAsync();
            return View(categories);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool result = await _context.Tags.AnyAsync(c => c.Name.Trim() == tag.Name.Trim());

            if (result)
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View();
            }

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

    }
}
