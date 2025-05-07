using HotelManagement.Models;
using HotelManagement.Data; // <-- Add this
using Microsoft.AspNetCore.Identity;
using MongoFramework;
using MongoFramework.AspNetCore.Identity;
using HotelBooking.Api.Database;
using HotelBooking.Api.Identity;

var builder = WebApplication.CreateBuilder(args);

// Register MongoDbConnection
builder.Services.AddSingleton<IMongoDbConnection>(sp =>
    MongoDbConnection.FromConnectionString("mongodb://localhost:27017/HotelBookingDb")
);

// 🔥 Register the custom DbContext
builder.Services.AddScoped<MongoDbContext, IdentityDbContext>();

// Register Identity
builder.Services.AddMongoIdentity<ApplicationUser, ApplicationRole>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;

    await DbInitializer.SeedRolesAndAdmin(sp);

    // Get the IMongoDatabase to create collections
    var dbConnection = sp.GetRequiredService<IMongoDbConnection>();
    var db = dbConnection.GetDatabase();

    await DbBootstrapper.CreateCollections(db);
}
app.Run();
