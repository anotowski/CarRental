using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Database.Models
{
    public class RentalHistory
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }

        public string BookingNumber { get; set; }

        public DateTime RentStartDate { get; set; }

        public DateTime? RentEndDate { get; set; }

        public int MileageOnRentalStart { get; set; }

        public int? MileageOnRentalEnd { get; set; }

        public Customer Customer { get; set; }

        [ForeignKey(nameof(CarId))]
        [InverseProperty("RentalHistories")]
        public virtual Car Car { get; set; }
    }
}
