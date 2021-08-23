using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Database.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string PlateNumber { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public CategoryType Category { get; set; }

        public bool IsAvailable { get; set; }

        [Column(TypeName = "decimal(18, 2")]
        public decimal BaseDayRentalFee { get; set; }

        [Column(TypeName = "decimal(18, 2")]
        public decimal KilometerFee { get; set; }

        [Required]
        public int CurrentMileage { get; set; }

        public ICollection<RentalHistory> RentalHistories { get; set; } 
    }
}
