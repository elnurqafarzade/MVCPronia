using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPronia.DAL;
using MVCPronia.Models;
using MVCPronia.ViewModels;
using System;

namespace MVCPronia.Controllers
{
	public class ProductController : Controller
	{
		private readonly AppDbContext _context;
		public ProductController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id == null || id <= 0)
			{
				return BadRequest();
			}
			Product? product = await _context.Products
			.Include(p => p.Category)
			.Include(p => p.ProductImages.OrderByDescending(x => x.IsPrimary))
			.FirstOrDefaultAsync(p => p.Id == id);

			if (product is null)
			{
				return NotFound();
			}


			DetailVM detailVM = new DetailVM
			{
				Product = product,
				Products = await _context.Products.Where(p => p.CategoryId == product.CategoryId && p.Id != id)
				.Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null))
				.ToListAsync(),


			};

			return View(detailVM);
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
