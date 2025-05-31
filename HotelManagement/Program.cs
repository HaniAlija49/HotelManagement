using HotelManagement.Models;
using HotelManagement.Data;
using HotelManagement.Profiles;
using HotelManagement.Repositories;
using HotelManagement.Interfaces;
using HotelManagement.Infrastructure.Middleware;
using HotelBooking.Api.Identity;
using HotelBooking.Api.Database;
using Microsoft.AspNetCore.Identity;
using MongoFramework;
using MongoFramework.AspNetCore.Identity;
using HotelBooking.Api.Database;
using HotelBooking.Api.Identity;
using HotelManagement.Profiles;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using HotelManagement.Contracts;
using HotelManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/hotel-management-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

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
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Add Movie Service
builder.Services.AddHttpClient<IMovieService, MovieService>();

// ✅ AutoMapper
builder.Services.AddAutoMapper(typeof(HotelProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(RoomProfile));
builder.Services.AddAutoMapper(typeof(BookingProfile));
builder.Services.AddAutoMapper(typeof(ReportProfile));
builder.Services.AddAutoMapper(typeof(ReviewProfile));

// ✅ Identity with MongoFramework
builder.Services.AddMongoIdentity<ApplicationUser, ApplicationRole>()
    .AddDefaultTokenProviders();

// ✅ MVC & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
    };
});

// Register JwtService
builder.Services.AddScoped<JwtService>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "HotelManagement API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token in the format: Bearer {your_token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add global exception handler
app.UseGlobalExceptionHandler();

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
