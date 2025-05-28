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

        // G�rev ID zorunlu
        [Required]
        [Display(Name = "G�rev ID")]
        public int GorevId { get; set; }

        // Navigation property opsiyonel, bind edilmeyecek
        [BindNever]
        public virtual Gorev? Gorev { get; set; }

        [Required(ErrorMessage = "Yorum i�eri�i girilmelidir.")]
        [StringLength(500, ErrorMessage = "Yorum en fazla 500 karakter olabilir.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Yorum ��eri�i")]
        public string Icerik { get; set; } = string.Empty;

        // Kullan�c� ad� formdan al�nmayacak
        [BindNever]
        public string KullaniciAdi { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        [Display(Name = "Olu�turma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // RowVersion concurrency
        [Timestamp]
        [BindNever]
        public byte[]? RowVersion { get; set; }

        // Kullan�c� ili�kisi opsiyonel
        public int? KullaniciId { get; set; }

        [ForeignKey(nameof(KullaniciId))]
        [BindNever]
        public virtual Kullanici? Kullanici { get; set; }
    }
}
