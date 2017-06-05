using System.ComponentModel.DataAnnotations;

namespace Vuew.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int Vat { get; set; }

        [Required]
        [Range(1, 99999)]
        public decimal Price { get; set; }
    }
}
