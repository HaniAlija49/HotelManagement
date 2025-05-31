using AutoMapper;
using HotelManagement.DTOs.Requests;
using HotelManagement.DTOs.Responses;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _repository;
        private readonly IMapper _mapper;

        public HotelController(IHotelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetAll(
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 10)
        {
            var hotels = await _repository.GetAllAsync();

            var paged = hotels
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(_mapper.Map<IEnumerable<HotelDto>>(paged));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetById(string id)
        {
            var hotel = await _repository.GetByIdAsync(id);
            if (hotel == null) return NotFound();

            return Ok(_mapper.Map<HotelDto>(hotel));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateHotelDto dto)
        {
            var hotel = _mapper.Map<Hotel>(dto);
            await _repository.AddAsync(hotel);
            return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, CreateHotelDto dto)
        {
            var hotel = _mapper.Map<Hotel>(dto);
            await _repository.UpdateAsync(id, hotel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> Search([FromQuery] string? name, [FromQuery] string? city, [FromQuery] string? country)
        {
            var hotels = await _repository.GetAllAsync();

            var filtered = hotels.Where(h =>
                (string.IsNullOrEmpty(name) || h.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(city) || h.City.Contains(city, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(country) || h.Country.Contains(country, StringComparison.OrdinalIgnoreCase))
            );

            return Ok(_mapper.Map<IEnumerable<HotelDto>>(filtered));
        }
        [HttpGet("sorted-by-rating")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetSortedByRating([FromQuery] bool descending = true)
        {
            var hotels = await _repository.GetAllAsync();
            var sorted = descending
                ? hotels.OrderByDescending(h => h.Rating)
                : hotels.OrderBy(h => h.Rating);

            return Ok(_mapper.Map<IEnumerable<HotelDto>>(sorted));
        }


    }
}
