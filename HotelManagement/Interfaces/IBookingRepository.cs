using HotelManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(string id);
        Task<IEnumerable<Booking>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Booking>> GetByRoomIdAsync(string roomId);
        Task AddAsync(Booking booking);
        Task UpdateAsync(string id, Booking booking);
        Task DeleteAsync(string id);
    }
}