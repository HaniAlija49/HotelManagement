using HotelManagement.Models;
using HotelManagement.Data; // <-- Add this
using Microsoft.AspNetCore.Identity;
using MongoFramework;
using MongoFramework.AspNetCore.Identity;
using HotelBooking.Api.Database;
using HotelBooking.Api.Identity;
using HotelManagement.Profiles;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register MongoDbConnection
builder.Services.AddScoped<IMongoDbConnection>(sp =>
    MongoDbConnection.FromConnectionString("mongodb+srv://ha30049:hani2002@cluster0.uwxqy96.mongodb.net/HotelManagement?retryWrites=true&w=majority&appName=Cluster0")
);


// 🔥 Register the custom DbContext
builder.Services.AddScoped<MongoDbContext, IdentityDbContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register Identity
builder.Services.AddMongoIdentity<ApplicationUser, ApplicationRole>()
    .AddDefaultTokenProviders();

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
