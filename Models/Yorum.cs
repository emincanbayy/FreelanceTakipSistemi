using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceTakipSistemi.Models
{
    public class Yorum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GorevId { get; set; }

        [ForeignKey(nameof(GorevId))]
        public Gorev Gorev { get; set; }

        [Required, StringLength(500)]
        public string Icerik { get; set; }

        [Required, StringLength(100)]
        public string KullaniciAdi { get; set; }

        [Required]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }
}
