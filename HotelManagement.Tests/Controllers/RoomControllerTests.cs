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
    public class RoomControllerTests
    {
        private readonly Mock<IRoomRepository> _mockRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly RoomController _controller;

        public RoomControllerTests()
        {
            _controller = new RoomController(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Room>());
            _mockMapper.Setup(m => m.Map<IEnumerable<RoomDto>>(It.IsAny<IEnumerable<Room>>())).Returns(new List<RoomDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Room)null);
            var result = await _controller.GetById("id");
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_Found_ReturnsOk()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new Room());
            _mockMapper.Setup(m => m.Map<RoomDto>(It.IsAny<Room>())).Returns(new RoomDto());
            var result = await _controller.GetById("id");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            _mockMapper.Setup(m => m.Map<Room>(It.IsAny<CreateRoomDto>())).Returns(new Room());
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Room>())).Returns(Task.CompletedTask);
            var result = await _controller.Create(new CreateRoomDto());
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