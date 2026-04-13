using Microsoft.EntityFrameworkCore;
using parking_app.Data;
using parking_app.Models;

namespace parking_app.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Spzs)
                    .ThenInclude(s => s.SpzParkingPlaces)
                        .ThenInclude(spp => spp.ParkingPlace)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                foreach(var s in user.Spzs)
                {
                    s.SpzParkingPlaces = s.SpzParkingPlaces
                        .OrderByDescending(spp => spp.Date.Start)
                        .Take(5)
                        .ToList();
                }
            }
            
            return user;
        }
    }
}
