using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Database.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
