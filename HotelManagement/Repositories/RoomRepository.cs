using HotelManagement.Data;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using MongoFramework;
using MongoFramework.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly IdentityDbContext _context;

        public RoomRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Set<Room>().AsNoTracking().ToListAsync();
        }

        public async Task<Room> GetByIdAsync(string id)
        {
            return await _context.Set<Room>().FindAsync(id);
        }

        public async Task<IEnumerable<Room>> GetByHotelIdAsync(string hotelId)
        {
            return await _context.Set<Room>().Where(r => r.HotelId == hotelId).ToListAsync();
        }

        public async Task AddAsync(Room room)
        {
            _context.Set<Room>().Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, Room room)
        {
            var existing = await _context.Set<Room>().FindAsync(id);
            if (existing == null) return;

            room.Id = id;
            _context.Set<Room>().Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var room = await _context.Set<Room>().FindAsync(id);
            if (room != null)
            {
                _context.Set<Room>().Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}