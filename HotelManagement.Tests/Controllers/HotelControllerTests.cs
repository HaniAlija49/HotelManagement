using AutoMapper;
using HotelManagement.Controllers;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HotelManagement.Tests.Controllers
{
    public class HotelControllerTests
    {
        private readonly Mock<IHotelRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly HotelController _controller;

        public HotelControllerTests()
        {
            _mockRepository = new Mock<IHotelRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new HotelController(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithHotels()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new Hotel { Id = "1", Name = "Test Hotel 1" },
                new Hotel { Id = "2", Name = "Test Hotel 2" }
            };

            var hotelDtos = new List<HotelDto>
            {
                new HotelDto { Id = "1", Name = "Test Hotel 1" },
                new HotelDto { Id = "2", Name = "Test Hotel 2" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(hotels);

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<HotelDto>>(It.IsAny<IEnumerable<Hotel>>()))
                .Returns(hotelDtos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<HotelDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var hotel = new Hotel { Id = "1", Name = "Test Hotel" };
            var hotelDto = new HotelDto { Id = "1", Name = "Test Hotel" };

            _mockRepository.Setup(repo => repo.GetByIdAsync("1"))
                .ReturnsAsync(hotel);

            _mockMapper.Setup(mapper => mapper.Map<HotelDto>(hotel))
                .Returns(hotelDto);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<HotelDto>(okResult.Value);
            Assert.Equal("1", returnValue.Id);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync("999"))
                .ReturnsAsync((Hotel)null);

            // Act
            var result = await _controller.GetById("999");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_WithValidHotel_ReturnsCreatedResult()
        {
            // Arrange
            var createHotelDto = new CreateHotelDto { Name = "New Hotel" };
            var hotel = new Hotel { Id = "1", Name = "New Hotel" };

            _mockMapper.Setup(mapper => mapper.Map<Hotel>(createHotelDto))
                .Returns(hotel);

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Hotel>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(createHotelDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetById), createdResult.ActionName);
            Assert.Equal("1", createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task Search_WithValidParameters_ReturnsFilteredResults()
        {
            // Arrange
            var hotels = new List<Hotel>
            {
                new Hotel { Id = "1", Name = "Test Hotel", City = "New York", Country = "USA" },
                new Hotel { Id = "2", Name = "Another Hotel", City = "London", Country = "UK" }
            };

            var filteredHotels = hotels.Where(h => 
                h.Name.Contains("Test", StringComparison.OrdinalIgnoreCase) &&
                h.City.Contains("New York", StringComparison.OrdinalIgnoreCase) &&
                h.Country.Contains("USA", StringComparison.OrdinalIgnoreCase)
            ).ToList();

            var hotelDtos = new List<HotelDto>
            {
                new HotelDto { Id = "1", Name = "Test Hotel", City = "New York", Country = "USA" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(hotels);

            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<HotelDto>>(It.Is<IEnumerable<Hotel>>(h => 
                h.Count() == 1 && h.First().Id == "1")))
                .Returns(hotelDtos);

            // Act
            var result = await _controller.Search("Test", "New York", "USA");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<HotelDto>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("1", returnValue.First().Id);
        }
    }
} 