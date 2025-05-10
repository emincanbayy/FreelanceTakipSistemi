using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    /// <summary>
    /// Görevle iliþkilendirilen yorumlarý temsil eder.
    /// </summary>
    public class Yorum
    {
        [Key]
        public int YorumId { get; set; }

        [Required(ErrorMessage = "Yorum içeriði zorunludur.")]
        public string Icerik { get; set; }

        /// <summary>
        /// Yorumun oluþturulma tarihi (varsayýlan: uygulama zaman damgasý)
        /// </summary>
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        // Hangi göreve ait olduðu
        public int GorevId { get; set; }
        public Gorev Gorev { get; set; }
    }
}
