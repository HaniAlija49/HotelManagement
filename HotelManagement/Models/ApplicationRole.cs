using MongoFramework.AspNetCore.Identity;

namespace HotelManagement.Models
{
    public class ApplicationRole : MongoIdentityRole
    {
    }

    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
    }
}
