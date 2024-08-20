using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPronia.DAL;
using MVCPronia.Models;
using MVCPronia.ViewModel;
using System;
using System.Diagnostics;

namespace MVCPronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var sli = await _context.Slides.OrderBy(s => s.Order).Take(2).ToListAsync();
            var pro = await _context.Products
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
                .ToListAsync();
            
        HomeVM homeVM = new HomeVM
            {
                
                Slides = await _context.Slides.OrderBy(s => s.Order).Take(2).ToListAsync(),
                Products = await _context.Products
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
                .ToListAsync()
            };
            return View(homeVM);
        }
    }
}