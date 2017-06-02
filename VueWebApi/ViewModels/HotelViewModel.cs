using System.ComponentModel.DataAnnotations;

namespace VueWebApi.ViewModels
{
    public class HotelViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string City { get; set; }
    }
}
