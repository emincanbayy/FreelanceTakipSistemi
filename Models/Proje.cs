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

        [Required]
        [StringLength(200)]
        [Display(Name = "Proje Baþlýðý")]
        public string Baslik { get; set; }

        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Açýklama")]
        public string Aciklama { get; set; }

        [Required]
        [Display(Name = "Baþlangýç Tarihi")]
        public DateTime BaslangicTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Bitiþ Tarihi")]
        public DateTime? BitisTarihi { get; set; }

        [Required]
        [Display(Name = "Kullanýcý")]
        public int KullaniciId { get; set; }

        [BindNever]
        public Kullanici? Kullanici { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public ICollection<Gorev> Gorevler { get; set; } = new List<Gorev>();
    }
}
