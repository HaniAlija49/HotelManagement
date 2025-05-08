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
                "Hotel", "Room", "Booking", "Review", "Report"
            };

            foreach (var name in neededCollections)
            {
                if (!collections.Contains(name))
                    await db.CreateCollectionAsync(name);
            }
        }
    }
}
