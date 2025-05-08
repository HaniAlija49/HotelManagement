using HotelManagement.Models;
using Microsoft.AspNetCore.Identity;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetByIdAsync(string id);
    Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
    Task AddToRoleAsync(ApplicationUser user, string role);
    Task<IList<string>> GetRolesAsync(ApplicationUser user);
}
