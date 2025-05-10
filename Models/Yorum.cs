using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    /// <summary>
    /// G�revle ili�kilendirilen yorumlar� temsil eder.
    /// </summary>
    public class Yorum
    {
        [Key]
        public int YorumId { get; set; }

        [Required(ErrorMessage = "Yorum i�eri�i zorunludur.")]
        public string Icerik { get; set; }

        /// <summary>
        /// Yorumun olu�turulma tarihi (varsay�lan: uygulama zaman damgas�)
        /// </summary>
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        // Hangi g�reve ait oldu�u
        public int GorevId { get; set; }
        public Gorev Gorev { get; set; }
    }
}
