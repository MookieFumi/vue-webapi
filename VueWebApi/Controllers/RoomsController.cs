using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueWebApi.Filters;
using VueWebApi.Infrastructure;
using VueWebApi.Models;
using VueWebApi.ViewModels;

namespace VueWebApi.Controllers
{
    [Route("api/hotels/{hotelId:int}/[controller]")]
    public class RoomsController : Controller
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        //
        // GET: api/hotels/1/rooms
        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(int hotelId)
        {
            var rooms = await _context.Rooms.Where(r => r.HotelId == hotelId).ToArrayAsync();

            var roomsViewModel = rooms.Select(room => new RoomViewModel
            {
                Id = room.Id,
                Name = room.Name,
                Vat = room.Vat,
                Price = room.Price
            });

            return Ok(roomsViewModel);
        }

        //
        // GET: api/hotels/1/rooms/1
        [HttpGet("{roomId:int}")]
        public async Task<IActionResult> GetHotelRoomById(int hotelId, int roomId)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == roomId && r.HotelId == hotelId);

            if (room == null)
            {
                return NotFound();
            }

            var roomViewModel = new RoomViewModel
            {
                Id = room.Id,
                Name = room.Name,
                Vat = room.Vat,
                Price = room.Price
            };

            return Ok(roomViewModel);
        }

        //
        // POST: /api/hotels/1/rooms
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> CreateHotelRoom([FromBody]RoomViewModel model, int hotelId)
        {
            var hotel = await 
                _context
                .Hotels
                .Include(h => h.Rooms)
                .SingleOrDefaultAsync(h => h.Id == hotelId);

            hotel.AddRoom(model.Name, model.Vat, model.Price);

            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        //
        // PUT: /api/hotels/1/rooms/1
        [HttpPut("{roomId:int}")]
        [ValidateModelState]
        public async Task<IActionResult> UpdateHotelRoom(
            int hotelId,
            int roomId,
            [FromBody]RoomViewModel model)
        {
            var hotel = await _context.Hotels.Include(h => h.Rooms).SingleOrDefaultAsync(h => h.Id == hotelId);

            if (hotel == null)
            {
                return NotFound();
            }

            hotel.ChangeHotelRoomData(roomId, model.Name, model.Vat, model.Price);

            await _context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
