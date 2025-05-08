using AutoMapper;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _repository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll()
        {
            var bookings = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetById(string id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(_mapper.Map<BookingDto>(booking));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetByUserId(string userId)
        {
            var bookings = await _repository.GetByUserIdAsync(userId);
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }

        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetByRoomId(string roomId)
        {
            var bookings = await _repository.GetByRoomIdAsync(roomId);
            return Ok(_mapper.Map<IEnumerable<BookingDto>>(bookings));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
        {
            var booking = _mapper.Map<Booking>(dto);
            booking.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CreateBookingDto dto)
        {
            var booking = _mapper.Map<Booking>(dto);
            await _repository.UpdateAsync(id, booking);
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
