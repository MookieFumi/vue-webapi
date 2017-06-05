using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vuew.Models;

namespace Vuew.Infrastructure
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly HotelDbContext _context;

        public DatabaseInitializer(HotelDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.Migrate();

            if (_context.Hotels.Any() == false)
            {
                var hotel1 = new Hotel("Hotel Don Juan I", "Alcalá de Henares");
                hotel1.AddRoom("1A", 21, 30.25M);
                hotel1.AddRoom("1B", 21, 40.28M);
                hotel1.AddRoom("1C", 21, 26.30M);
                hotel1.AddRoom("2A", 21, 30M);
                hotel1.AddRoom("2B", 21, 30.25M);
                hotel1.AddRoom("3A", 21, 30.25M);
                hotel1.AddRoom("4A", 21, 30.25M);

                _context.Hotels.Add(hotel1);
                _context.SaveChanges();
            }
        }
    }
}
