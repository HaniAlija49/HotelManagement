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
    public class ReportControllerTests
    {
        private readonly Mock<IReportRepository> _mockRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly ReportController _controller;

        public ReportControllerTests()
        {
            _controller = new ReportController(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Report>());
            _mockMapper.Setup(m => m.Map<IEnumerable<ReportDto>>(It.IsAny<IEnumerable<Report>>())).Returns(new List<ReportDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Report)null);
            var result = await _controller.GetById("id");
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetById_Found_ReturnsOk()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(new Report());
            _mockMapper.Setup(m => m.Map<ReportDto>(It.IsAny<Report>())).Returns(new ReportDto());
            var result = await _controller.GetById("id");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            _mockMapper.Setup(m => m.Map<Report>(It.IsAny<CreateReportDto>())).Returns(new Report());
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Report>())).Returns(Task.CompletedTask);
            var result = await _controller.Create(new CreateReportDto());
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