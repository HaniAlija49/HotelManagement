using AutoMapper;
using HotelManagement.Controllers;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using HotelManagement.Contracts;

namespace HotelManagement.Tests.Controllers
{
    public class ReviewControllerTests
    {
        private readonly Mock<IReviewRepository> _mockRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly ReviewController _controller;

        public ReviewControllerTests()
        {
            _controller = new ReviewController(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetByHotel_ReturnsOk()
        {
            _mockRepo.Setup(r => r.GetByHotelIdAsync(It.IsAny<string>())).ReturnsAsync(new List<Review>());
            _mockMapper.Setup(m => m.Map<List<ReviewDto>>(It.IsAny<List<Review>>())).Returns(new List<ReviewDto>());
            var result = await _controller.GetByHotel("hotelId");
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Create_Unauthorized_ReturnsUnauthorized()
        {
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            var result = await _controller.Create(new CreateReviewRequest());
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Create_Valid_ReturnsOk()
        {
            var userId = "userId";
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = user };
            _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
            _mockMapper.Setup(m => m.Map<Review>(It.IsAny<CreateReviewRequest>())).Returns(new Review());
            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Review>())).Returns(Task.CompletedTask);
            var result = await _controller.Create(new CreateReviewRequest());
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            _mockRepo.Setup(r => r.DeleteAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
            var result = await _controller.Delete("id");
            Assert.IsType<NoContentResult>(result);
        }
    }
} 