using System;
using System.ComponentModel.DataAnnotations;

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

        public int MilageOnRentalStart { get; set; }

        public int? MilageOnRentalEnd { get; set; }

        public Customer Customer { get; set; }
    }
}
