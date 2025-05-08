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
    public class ReportRepository : IReportRepository
    {
        private readonly IdentityDbContext _context;

        public ReportRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Set<Report>().AsNoTracking().ToListAsync();
        }

        public async Task<Report> GetByIdAsync(string id)
        {
            return await _context.Set<Report>().FindAsync(id);
        }

        public async Task<IEnumerable<Report>> GetByHotelIdAsync(string hotelId)
        {
            return await _context.Set<Report>().Where(r => r.HotelId == hotelId).ToListAsync();
        }

        public async Task AddAsync(Report report)
        {
            _context.Set<Report>().Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, Report report)
        {
            var existing = await _context.Set<Report>().FindAsync(id);
            if (existing == null) return;

            report.Id = id;
            _context.Set<Report>().Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var report = await _context.Set<Report>().FindAsync(id);
            if (report != null)
            {
                _context.Set<Report>().Remove(report);
                await _context.SaveChangesAsync();
            }
        }
    }
}