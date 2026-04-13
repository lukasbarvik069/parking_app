using Microsoft.EntityFrameworkCore;
using parking_app.Data;
using parking_app.Models;
using System.ComponentModel.DataAnnotations;

namespace parking_app.Repositories
{
    public class SpzParkingPlaceRepository
    {
        private readonly AppDbContext _context;

        public SpzParkingPlaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Create(SpzParkingPlace data)
        {
            try
            {
                var spz = await _context.Spz.FirstOrDefaultAsync(s => s.Id == data.SpzId);
                if (spz == null) return "SPZ nenalezena";

                var pp = await _context.ParkingPlaces.FirstOrDefaultAsync(pp => pp.Id == data.ParkingPlaceId);
                if (pp == null) return "Parkoviště nenalezeno";


                var pps = await _context.SpzParkingPlaces
                    .AnyAsync(spp => spp.SpzId == spz.Id
                        && spp.Date.Start <= data.Date.Start
                        && spp.Date.End > data.Date.Start);

                if (pps) return "Auto je již zaparkované";

                var count = await _context.SpzParkingPlaces
                    .Where(spp => spp.ParkingPlaceId == data.ParkingPlaceId 
                        && spp.Date.Start <= data.Date.Start
                        && spp.Date.End > data.Date.Start)
                    .CountAsync();

                if (count >= pp.Capacity) return "Parkoviště je plné";


                _context.SpzParkingPlaces.Add(data);
                await _context.SaveChangesAsync();
                return "ok";

            }
            catch (Exception ex)
            {
                return $"Chyba při vytváření: {ex.Message}";
            }
        }
    }
}
