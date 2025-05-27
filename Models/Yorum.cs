using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FreelanceTakipSistemi.Models
{
    public class Yorum
    {
        [Key]
        public int Id { get; set; }

        // GorevId hâlâ gerekli
        [Required]
        [Display(Name = "Görev ID")]
        public int GorevId { get; set; }

        // Navigation opsiyonel, bind etmiyoruz
        [BindNever]
        public virtual Gorev? Gorev { get; set; }

        [Required(ErrorMessage = "Yorum içeriði girilmelidir.")]
        [StringLength(500, ErrorMessage = "Yorum en fazla 500 karakter olabilir.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Yorum Ýçeriði")]
        public string Icerik { get; set; } = string.Empty;

        // Kullanýcý adý formdan alýnmayacak
        [BindNever]
        public string KullaniciAdi { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [Display(Name = "Oluþturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // DüzenlemeTarihi artýk yok

        // RowVersion nullable, scaffold etmiyoruz
        [Timestamp]
        [BindNever]
        public byte[]? RowVersion { get; set; }
    }
}
