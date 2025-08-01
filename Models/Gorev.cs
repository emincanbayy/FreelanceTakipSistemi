﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceTakipSistemi.Models
{
    public class Gorev
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proje seçimi zorunludur.")]
        public int? ProjeId { get; set; }

        [Required(ErrorMessage = "Başlık girilmelidir.")]
        [StringLength(100)]
        public string Baslik { get; set; }

        public string? Aciklama { get; set; }

        [Required(ErrorMessage = "Durum seçilmelidir.")]
        public string Durum { get; set; }

        [DataType(DataType.Date)]
        public DateTime? TeslimTarihi { get; set; }

        public string? Oncelik { get; set; }

        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // ——— Buraya eklenen alanlar ———
        [Display(Name = "Atanan Kullanıcı")]
        public int? AtananKullaniciId { get; set; }

        [ForeignKey(nameof(AtananKullaniciId))]
        public virtual Kullanici? AtananKullanici { get; set; }
        // ————————————————————————

        public Proje? Proje { get; set; }
        public ICollection<Yorum> Yorumlar { get; set; } = new List<Yorum>();
    }
}
