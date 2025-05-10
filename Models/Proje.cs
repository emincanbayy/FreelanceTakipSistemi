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

        [Required(ErrorMessage = "Ba�l�k zorunludur.")]
        [StringLength(200, ErrorMessage = "Proje ba�l��� en fazla 200 karakter olabilir.")]
        [Display(Name = "Ba�l�k")]
        public string Baslik { get; set; }

        [StringLength(2000, ErrorMessage = "A��klama en fazla 2000 karakter olabilir.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "A��klama")]
        public string Aciklama { get; set; }

        [Required]
        [Display(Name = "Ba�lang�� Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BaslangicTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Biti� Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BitisTarihi { get; set; }

        [Required]
        [Display(Name = "Proje Sahibi")]
        public int KullaniciId { get; set; }

        [BindNever]
        public Kullanici Kullanici { get; set; }

        [BindNever]
        public ICollection<Gorev> Gorevler { get; set; } = new List<Gorev>();
    }
}
