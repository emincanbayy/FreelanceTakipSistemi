using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceTakipSistemi.Models
{
    public class Gorev
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Baslik { get; set; }

        [StringLength(2000)]
        public string Aciklama { get; set; }

        [Required]
        public int ProjeId { get; set; }

        [ForeignKey(nameof(ProjeId))]
        public Proje Proje { get; set; }

        public ICollection<Yorum> Yorumlar { get; set; } = new List<Yorum>();

        [Required]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        public DateTime? TeslimTarihi { get; set; }

        [StringLength(50)]
        public string Durum { get; set; }

        [StringLength(50)]
        public string Oncelik { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
