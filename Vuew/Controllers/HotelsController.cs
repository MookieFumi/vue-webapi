using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vuew.Filters;
using Vuew.Infrastructure;
using Vuew.Models;
using Vuew.ViewModels;

namespace Vuew.Controllers
{
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly HotelDbContext _context;

        public HotelsController(HotelDbContext context)
        {
            _context = context;
        }

        //
        // GET: api/hotels
        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _context.Hotels.ToArrayAsync();

            var hotelsViewModel = hotels.Select(hotel => new HotelViewModel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City
            });

            return Ok(hotelsViewModel);
        }

        //
        // GET: api/hotels/1
        [HttpGet("{hotelId:int}")]
        public async Task<IActionResult> GetHotelById(int hotelId)
        {
            var hotel = await _context.Hotels.SingleOrDefaultAsync(h => h.Id == hotelId);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelViewModel = new HotelViewModel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City
            };

            return Ok(hotelViewModel);
        }

        //
        // POST: /api/hotels
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> CreateHotel([FromBody]HotelViewModel model)
        {
            var existingHotel = await _context.Hotels.SingleOrDefaultAsync(h => h.Name == model.Name);
            if (existingHotel != null)
            {
                throw new InvalidOperationException("Hotel exists");
            }

            var hotel = new Hotel(model.Name, model.City);

            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        //
        // PUT: /api/hotels/1
        [HttpPut("{hotelId:int}")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateHotel(
            int hotelId,
            [FromBody]HotelViewModel model)
        {
            var hotel = await _context.Hotels.SingleOrDefaultAsync(h => h.Id == hotelId);

            if (hotel == null)
            {
                return NotFound();
            }

            var existingHotel = await _context.Hotels.SingleOrDefaultAsync(h => h.Name == model.Name && h.Id != hotelId);
            if (existingHotel != null)
            {
                throw new InvalidOperationException("Hotel exists");
            }

            hotel.ChangeHotelData(model.Name, model.City);

            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
