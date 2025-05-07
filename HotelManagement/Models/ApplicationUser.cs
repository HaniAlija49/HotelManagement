using MongoFramework.AspNetCore.Identity;

namespace HotelManagement.Models
{
    public class ApplicationUser : MongoIdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
