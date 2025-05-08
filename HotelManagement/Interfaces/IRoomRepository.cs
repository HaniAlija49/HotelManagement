using HotelManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(string id);
        Task<IEnumerable<Room>> GetByHotelIdAsync(string hotelId);
        Task AddAsync(Room room);
        Task UpdateAsync(string id, Room room);
        Task DeleteAsync(string id);
    }
}