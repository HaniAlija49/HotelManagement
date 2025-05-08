using HotelManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllAsync();
        Task<Hotel> GetByIdAsync(string id);
        Task AddAsync(Hotel hotel);
        Task UpdateAsync(string id, Hotel hotel);
        Task DeleteAsync(string id);
    }
}
