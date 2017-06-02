using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VueWebApi.Models
{
    public class Hotel
    {
        protected Hotel() { }

        public Hotel(string name, string city)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            Name = name;
            City = city;
            Rooms = new Collection<Room>();
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string City { get; private set; }

        public ICollection<Room> Rooms { get; private set; }

        public void AddRoom(string name, int vat, decimal price)
        {
            if (Rooms.SingleOrDefault(r => r.Name == name) != null)
            {
                throw new InvalidOperationException("Room name exists");
            }

            var room = new Room(name, vat, price, this);
            Rooms.Add(room);
        }

        public void ChangeHotelData(string name, string city)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            Name = name;
            City = city;
        }

        public void ChangeHotelRoomData(int roomId, string name, int vat, decimal price)
        {
            var room = Rooms.SingleOrDefault(r => r.Id == roomId);

            if (room == null)
            {
                throw new ArgumentNullException(nameof(roomId));
            }

            if (Rooms.Any(r => r.Name == name && r.Id != roomId))
            {
                throw new InvalidOperationException("Room Exists");
            }

            room.ChangeRoomData(name, vat, price);
        }
    }
}
