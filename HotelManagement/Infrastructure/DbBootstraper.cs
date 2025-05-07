using MongoDB.Driver;
using System.Threading.Tasks;

namespace HotelBooking.Api.Database
{
    public static class DbBootstrapper
    {
        public static async Task CreateCollections(IMongoDatabase db)
        {
            var collections = await db.ListCollectionNames().ToListAsync();

            string[] neededCollections = {
                "Hotels", "Rooms", "Bookings", "Reviews", "Reports", "Users", "Roles"
            };

            foreach (var name in neededCollections)
            {
                if (!collections.Contains(name))
                    await db.CreateCollectionAsync(name);
            }
        }
    }
}
