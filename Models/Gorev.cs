using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FreelanceTakipSistemi.Models
{
    /// <summary>Projeye baðlý görevleri temsil eder.</summary>
    public class Gorev
    {
        [Key]
        public int GorevId { get; set; }

        [Required(ErrorMessage = "Baþlýk zorunludur.")]
        [StringLength(100, ErrorMessage = "Baþlýk en fazla 100 karakter olabilir.")]
        [Display(Name = "Baþlýk")]
        public string Baslik { get; set; }

        [StringLength(1000, ErrorMessage = "Açýklama en fazla 1000 karakter olabilir.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Açýklama")]
        public string Aciklama { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Durum en fazla 50 karakter olabilir.")]
        [Display(Name = "Durum")]
        public string Durum { get; set; } = "Yapýlacak";

        [Required]
        [Display(Name = "Oluþturulma Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Bitiþ Tarihi")]
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
