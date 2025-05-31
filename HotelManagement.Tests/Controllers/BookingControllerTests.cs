using AutoMapper;
using HotelManagement.Controllers;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using HotelManagement.Interfaces;

namespace HotelManagement.Tests.Controllers
{
    public class BookingControllerTests
    {
        private readonly Mock<IBookingRepository> _mockRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly BookingController _controller;

        public BookingControllerTests()
        {
            _controller = new BookingController(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Booking>());
            _mockMapper.Setup(m => m.Map<IEnumerable<BookingDto>>(It.IsAny<IEnumerable<Booking>>())).Returns(new List<BookingDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Booking)null);
            var result = await _controller.GetById("id");
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_Found_ReturnsOk()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new Booking());
            _mockMapper.Setup(m => m.Map<BookingDto>(It.IsAny<Booking>())).Returns(new BookingDto());
            var result = await _controller.GetById("id");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            _mockMapper.Setup(m => m.Map<Booking>(It.IsAny<CreateBookingDto>())).Returns(new Booking());
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Booking>())).Returns(Task.CompletedTask);
            var result = await _controller.Create(new CreateBookingDto());
            Assert.IsType<CreatedAtActionResult>(result);
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