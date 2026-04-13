using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using parking_app.Data;
using parking_app.Models;

namespace parking_app.Repositories
{
    public class SpzRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;


        public SpzRepository(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Spz>> GetAll()
        {
            return await _context.Spz.ToListAsync();
        }

        public async Task<IEnumerable<Spz>> GetByUser(int userId)
        {
            return await _context.Spz
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Spz>> GetFreeByUser(int userId)
        {
            var now = DateTime.Now;
            return await _context.Spz
                .Where (s => s.UserId == userId)
                .Where(s => !s.SpzParkingPlaces
                    .Any(spp => spp.Date.Start <= now
                        && spp.Date.End > now))
                .ToListAsync();
        }

        public async Task<string> Create(Spz spz)
        {
            try
            {
                if (string.IsNullOrEmpty(spz.Name)) return "Název SPZ nesmí být prázdný";
                spz.Name = spz.Name.Trim().ToUpper();
                var exist = await _context.Spz.AnyAsync(s => s.Name  == spz.Name);
                if (exist) return "Tato SPZ již existuje";

                _context.Spz.Add(spz);

                await _context.SaveChangesAsync();

                return "ok";
            }
            catch (Exception ex)
            {
                return $"Chyba při ukládání: {ex.Message}";
            }
        }

        public async Task<bool> Delete(int spzId, int userId)
        {

            try
            {
                var spz = await _context.Spz
                .Where(s => s.UserId == userId)
                .FirstOrDefaultAsync(s => s.Id == spzId);

                if (spz == null) return false;


                _context.Spz.Remove(spz);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
