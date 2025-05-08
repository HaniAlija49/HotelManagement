using HotelManagement.Data;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using MongoFramework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoFramework.Linq;

namespace HotelManagement.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IdentityDbContext _context;

        public BookingRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Set<Booking>().AsNoTracking().ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(string id)
        {
            return await _context.Set<Booking>().FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(string userId)
        {
            return await _context.Set<Booking>().Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByRoomIdAsync(string roomId)
        {
            return await _context.Set<Booking>().Where(b => b.RoomId == roomId).ToListAsync();
        }

        public async Task AddAsync(Booking booking)
        {
            _context.Set<Booking>().Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, Booking booking)
        {
            var existing = await _context.Set<Booking>().FindAsync(id);
            if (existing == null) return;

            booking.Id = id;
            _context.Set<Booking>().Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var booking = await _context.Set<Booking>().FindAsync(id);
            if (booking != null)
            {
                _context.Set<Booking>().Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}