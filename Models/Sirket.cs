using System;
using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    public class Sirket
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Ad { get; set; }

        [MaxLength(250)]
        public string? Adres { get; set; }

        public string? Telefon { get; set; }

        public string? Email { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }
}
