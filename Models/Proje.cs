using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FreelanceTakipSistemi.Models
{
    public class Proje
    {
        [Key]
        public int ProjeId { get; set; }

        [Required, StringLength(200)]
        public string Baslik { get; set; }

        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string Aciklama { get; set; }

        [Required]
        public DateTime BaslangicTarihi { get; set; } = DateTime.Now;

        public DateTime? BitisTarihi { get; set; }

        [Required]
        public int KullaniciId { get; set; }

        [BindNever]
        public Kullanici Kullanici { get; set; }

        [BindNever]
        public ICollection<Gorev> Gorevler { get; set; } = new List<Gorev>();
    }
}
