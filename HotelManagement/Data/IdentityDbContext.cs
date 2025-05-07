using HotelManagement.Models;
using MongoFramework;

namespace HotelManagement.Data
{
    public class IdentityDbContext : MongoDbContext
    {
        public IdentityDbContext(IMongoDbConnection connection) : base(connection)
        {
        }



    }
}
