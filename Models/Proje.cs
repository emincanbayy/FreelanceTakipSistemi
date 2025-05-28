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

        // Ba�l�k ve A��klama (iste�e ba�l�)
        public string? Baslik { get; set; }
        public string? Aciklama { get; set; }

        // Tarih alanlar� (nullable)
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        // Kullan�c� ili�kisi (nullable)
        public int? KullaniciId { get; set; }
        [ForeignKey(nameof(KullaniciId))]
        public Kullanici? Kullanici { get; set; }

        // �irket ili�kisi (nullable)
        public int? SirketId { get; set; }
        [ForeignKey(nameof(SirketId))]
        public Sirket? Sirket { get; set; }

        // G�revler koleksiyonu (nullable)
        public ICollection<Gorev>? Gorevler { get; set; }
    }
}