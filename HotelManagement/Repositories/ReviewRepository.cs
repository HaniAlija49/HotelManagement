using HotelManagement.Contracts;
using HotelManagement.Models;
using MongoFramework;
using MongoFramework.Linq;

namespace HotelManagement.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MongoDbContext _context;

        public ReviewRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Set<Review>().ToListAsync();
        }

        public async Task<List<Review>> GetByHotelIdAsync(string hotelId)
        {
            return await _context.Set<Review>().Where(r => r.HotelId == hotelId).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(string id)
        {
            return await _context.Set<Review>().FindAsync(id);
        }

        public async Task CreateAsync(Review review)
        {
            _context.Set<Review>().Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var review = await GetByIdAsync(id);
            if (review != null)
            {
                _context.Set<Review>().Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}