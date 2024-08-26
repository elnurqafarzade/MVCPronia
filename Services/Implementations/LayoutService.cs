using Microsoft.EntityFrameworkCore;
using MVCPronia.DAL;
using MVCPronia.Services.Interfaces;

namespace MVCPronia.Services.Implementations
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetSettings()
        {
            return await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
        }

    }
}
