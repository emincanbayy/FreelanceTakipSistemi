using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FreelanceTakipSistemi.Models
{
    /// <summary>Projeye ba�l� g�revleri temsil eder.</summary>
    public class Gorev
    {
        [Key]
        public int GorevId { get; set; }

        [Required(ErrorMessage = "Ba�l�k zorunludur.")]
        [StringLength(100, ErrorMessage = "Ba�l�k en fazla 100 karakter olabilir.")]
        [Display(Name = "Ba�l�k")]
        public string Baslik { get; set; }

        [StringLength(1000, ErrorMessage = "A��klama en fazla 1000 karakter olabilir.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "A��klama")]
        public string Aciklama { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Durum en fazla 50 karakter olabilir.")]
        [Display(Name = "Durum")]
        public string Durum { get; set; } = "Yap�lacak";

        [Required]
        [Display(Name = "Olu�turulma Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Biti� Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BitisTarihi { get; set; }

        [Required]
        [Display(Name = "Proje")]
        public int ProjeId { get; set; }

        [BindNever]
        public Proje Proje { get; set; }

        [BindNever]
        public ICollection<Yorum> Yorumlar { get; set; } = new List<Yorum>();

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
