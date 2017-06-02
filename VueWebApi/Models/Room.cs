using System;

namespace VueWebApi.Models
{
    public class Room
    {
        protected Room() { }

        internal Room(string name, int vat, decimal price, Hotel hotel)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (vat == default(int))
            {
                throw new ArgumentNullException(nameof(vat));
            }

            if (price == default(decimal))
            {
                throw new ArgumentNullException(nameof(price));
            }

            Name = name;
            Vat = vat;
            Price = price;
            Hotel = hotel;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Vat { get; private set; }

        public decimal Price { get; private set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; private set; }

        internal void ChangeRoomData(string name, int vat, decimal price)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (vat == default(int))
            {
                throw new ArgumentNullException(nameof(vat));
            }

            if (price == default(decimal))
            {
                throw new ArgumentNullException(nameof(price));
            }

            Name = name;
            Vat = vat;
            Price = price;
        }
    }
}
