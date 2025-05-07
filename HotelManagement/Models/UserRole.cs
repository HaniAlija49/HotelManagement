using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace HotelBooking.Api.Identity
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
    }
    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
    }
}
