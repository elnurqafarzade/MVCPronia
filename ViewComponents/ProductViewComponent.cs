using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPronia.DAL;
using MVCPronia.Models;
using MVCPronia.Utilities.Enums;

namespace MVCPronia.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(SortType type)
        {
            IQueryable<Product> query = _context.Products.Where(p => p.IsDeleted == false);
            switch (type)
            {
                case SortType.Name:
                    query = query.OrderBy(p => p.Name);
                    break;
                case SortType.Price:
                    query = query.OrderByDescending(q => q.Price);
                    break;
                case SortType.Newest:
                    query = query.OrderByDescending(q => q.CreatedAt);
                    break;
            }
            query = query.Take(8).Include(p => p.ProductImages.Where(pi => pi.IsPrimary != null));

            return View(await query.ToListAsync());
        }
    }
}
