using HotelManagement.Data;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using MongoFramework;
using MongoFramework.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly IdentityDbContext _context;

        public HotelRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Set<Hotel>().AsNoTracking().ToListAsync();
        }

        public async Task<Hotel> GetByIdAsync(string id)
        {
            return await _context.Set<Hotel>().FindAsync(id);
        }

        public async Task AddAsync(Hotel hotel)
        {
            _context.Set<Hotel>().Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, Hotel hotel)
        {
            var existing = await _context.Set<Hotel>().FindAsync(id);
            if (existing == null) return;

            hotel.Id = id;
            _context.Set<Hotel>().Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var hotel = await _context.Set<Hotel>().FindAsync(id);
            if (hotel != null)
            {
                _context.Set<Hotel>().Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
