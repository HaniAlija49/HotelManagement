using HotelManagement.Models;
using HotelManagement.Data;
using HotelManagement.Profiles;
using HotelManagement.Repositories;
using HotelManagement.Interfaces;
using HotelBooking.Api.Identity;
using HotelBooking.Api.Database;
using Microsoft.AspNetCore.Identity;
using MongoFramework;
using MongoFramework.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// ✅ MongoDB connection
builder.Services.AddScoped<IMongoDbConnection>(sp =>
    MongoDbConnection.FromConnectionString("mongodb+srv://ha30049:hani2002@cluster0.uwxqy96.mongodb.net/HotelManagement?retryWrites=true&w=majority&appName=Cluster0")
);

// ✅ DbContext & Repositories
builder.Services.AddScoped<IdentityDbContext>();
builder.Services.AddScoped<MongoDbContext, IdentityDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

// ✅ AutoMapper
builder.Services.AddAutoMapper(typeof(HotelProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(RoomProfile));

// ✅ Identity with MongoFramework
builder.Services.AddMongoIdentity<ApplicationUser, ApplicationRole>()
    .AddDefaultTokenProviders();

// ✅ MVC & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ✅ Seeding Roles & Admin + Collections
using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;

    await DbInitializer.SeedRolesAndAdmin(sp);

    var dbConnection = sp.GetRequiredService<IMongoDbConnection>();
    var db = dbConnection.GetDatabase();

    await DbBootstrapper.CreateCollections(db);
}

app.Run();
