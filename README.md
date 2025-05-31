# Hotel Management System (SOA Project)

A RESTful Hotel and Room Booking System built with ASP.NET Core 8 and MongoDB. The system is designed using Service-Oriented Architecture (SOA) principles and supports user authentication, hotel management, room bookings, reviews, and reporting features.

## 🔧 Technologies Used

- ASP.NET Core 8
- MongoDB (via MongoDB Atlas)
- MongoFramework
- AutoMapper
- ASP.NET Identity (with MongoDB)
- Swagger (Swashbuckle)
- JWT Authentication

## 📦 Features

- User registration/login with roles (Admin, Customer)
- CRUD for Hotels, Rooms, Bookings, Reviews
- Pagination, filters and sorting for hotels
- Booking availability check
- Report generation for admins
- RESTful API with full Swagger documentation

## 📂 Project Structure

/HotelManagement

├── Controllers

├── Interfaces

├── Models

├── Repositories

├── DTOs

├── Profiles

├── Data

└── Program.cs



## 🚀 Getting Started

1. Clone the repo:
   ```bash
   git clone https://github.com/your-repo/hotel-management-soa.git
   
2. Add your MongoDB connection string in Program.cs.

3. Run the project:
dotnet run

4. Open Swagger UI:
https://localhost:{PORT}/swagger

🧪 Testing
Use Swagger or Postman to test endpoints like:

/api/auth/register

/api/hotel

/api/review/id

/api/report

👥 Team

Hani Alija

Uvejs Murtezi


📘 License

This project is for educational purposes under Southeast European University.
