using HotelManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllAsync();
        Task<Report> GetByIdAsync(string id);
        Task<IEnumerable<Report>> GetByHotelIdAsync(string hotelId);
        Task AddAsync(Report report);
        Task UpdateAsync(string id, Report report);
        Task DeleteAsync(string id);
    }
}