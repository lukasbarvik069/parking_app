using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parking_app.Data;
using parking_app.Models;
using System.Threading.Tasks.Dataflow;

namespace parking_app.Repositories
{
    public class ParkingPlaceRepository
    {
        private readonly AppDbContext _context;

        public ParkingPlaceRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ParkingPlace>> GetAll()
        {
            var now = DateTime.Now;
            return await _context.ParkingPlaces
                .Include(pp => pp.SpzParkingPlaces
                    .Where(spp => spp.Date.Start <= now && spp.Date.End > now))
                .ToListAsync();
        }

        public async Task<IEnumerable<ParkingPlace>> GetAllWithSPZ()
        {
            var now = DateTime.Now;
            return await _context.ParkingPlaces
                .Include(pp => pp.SpzParkingPlaces
                    .Where(pps => pps.Date.Start <= now
                        && pps.Date.End > now))
                    .ThenInclude(pps => pps.Spz)
                .ToListAsync();
        }

        public async Task<string> GetName(int id)
        {
            var data = await _context.ParkingPlaces.FirstOrDefaultAsync(p => p.Id == id);
            if (data == null) return string.Empty;


            return data.Name;
        }

        public async Task<int> GetPrice(int id)
        {
            var data = await _context.ParkingPlaces.FirstOrDefaultAsync(p => p.Id == id);
            if (data == null) return -1;

            return data.PricePerHour;
        }
    }
}
