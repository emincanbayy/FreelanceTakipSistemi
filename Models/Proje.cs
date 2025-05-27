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
        [Display(Name = "Proje Ba�l���")]
        public string Baslik { get; set; }

        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "A��klama")]
        public string Aciklama { get; set; }

        [Required]
        [Display(Name = "Ba�lang�� Tarihi")]
        public DateTime BaslangicTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Biti� Tarihi")]
        public DateTime? BitisTarihi { get; set; }

        [Required]
        [Display(Name = "Kullan�c�")]
        public int KullaniciId { get; set; }

        [BindNever]
        public Kullanici? Kullanici { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public ICollection<Gorev> Gorevler { get; set; } = new List<Gorev>();
    }
}
