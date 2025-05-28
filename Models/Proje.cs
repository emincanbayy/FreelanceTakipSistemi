using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceTakipSistemi.Models
{
    public class Proje
    {
        [Key]
        public int ProjeId { get; set; }

        // Baþlýk ve Açýklama (isteðe baðlý)
        public string? Baslik { get; set; }
        public string? Aciklama { get; set; }

        // Tarih alanlarý (nullable)
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        // Kullanýcý iliþkisi (nullable)
        public int? KullaniciId { get; set; }
        [ForeignKey(nameof(KullaniciId))]
        public Kullanici? Kullanici { get; set; }

        // Þirket iliþkisi (nullable)
        public int? SirketId { get; set; }
        [ForeignKey(nameof(SirketId))]
        public Sirket? Sirket { get; set; }

        // Görevler koleksiyonu (nullable)
        public ICollection<Gorev>? Gorevler { get; set; }
    }
}