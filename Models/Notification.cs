// Models/Notification.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceTakipSistemi.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KullaniciId { get; set; }
        [ForeignKey(nameof(KullaniciId))]
        public Kullanici Kullanici { get; set; }

        public int? GorevId { get; set; }
        [ForeignKey(nameof(GorevId))]
        public Gorev Gorev { get; set; }

        [Required, StringLength(500)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}
