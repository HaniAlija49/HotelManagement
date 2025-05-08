using HotelManagement.Models;

namespace HotelManagement.Contracts
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<List<Review>> GetByHotelIdAsync(string hotelId);
        Task<Review?> GetByIdAsync(string id);
        Task CreateAsync(Review review);
        Task DeleteAsync(string id);
    }
}