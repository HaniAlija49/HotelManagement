using AutoMapper;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;

        public ReportController(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetAll()
        {
            var reports = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ReportDto>>(reports));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDto>> GetById(string id)
        {
            var report = await _repository.GetByIdAsync(id);
            if (report == null) return NotFound();
            return Ok(_mapper.Map<ReportDto>(report));
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetByHotelId(string hotelId)
        {
            var reports = await _repository.GetByHotelIdAsync(hotelId);
            return Ok(_mapper.Map<IEnumerable<ReportDto>>(reports));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReportDto dto)
        {
            var report = _mapper.Map<Report>(dto);
            report.GeneratedAt = DateTime.UtcNow;
            await _repository.AddAsync(report);
            return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CreateReportDto dto)
        {
            var report = _mapper.Map<Report>(dto);
            await _repository.UpdateAsync(id, report);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}