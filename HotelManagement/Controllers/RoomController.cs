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
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;

        public RoomController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll()
        {
            var rooms = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RoomDto>>(rooms));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetById(string id)
        {
            var room = await _repository.GetByIdAsync(id);
            if (room == null) return NotFound();

            return Ok(_mapper.Map<RoomDto>(room));
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetByHotelId(string hotelId)
        {
            var rooms = await _repository.GetByHotelIdAsync(hotelId);
            return Ok(_mapper.Map<IEnumerable<RoomDto>>(rooms));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);
            await _repository.AddAsync(room);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CreateRoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);
            await _repository.UpdateAsync(id, room);
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