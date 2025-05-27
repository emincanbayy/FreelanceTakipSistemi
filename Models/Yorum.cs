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

        // GorevId h�l� gerekli
        [Required]
        [Display(Name = "G�rev ID")]
        public int GorevId { get; set; }

        // Navigation opsiyonel, bind etmiyoruz
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

        // D�zenlemeTarihi art�k yok

        // RowVersion nullable, scaffold etmiyoruz
        [Timestamp]
        [BindNever]
        public byte[]? RowVersion { get; set; }
    }
}
