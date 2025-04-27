using System.ComponentModel.DataAnnotations;
using System;

namespace FreelanceTakipSistemi.Models
{
    public class Gorev
    {
        [Key]
        public int GorevId { get; set; }

        // Baþlýk zorunludur
        [Required(ErrorMessage = "Baþlýk zorunludur.")]
        public string Baslik { get; set; }

        // Açýklama isteðe baðlýdýr
        public string Aciklama { get; set; }

        // Durum, varsayýlan olarak "Yapýlacak" olacaktýr
        [Required(ErrorMessage = "Durum zorunludur.")]
        public string Durum { get; set; } = "Yapýlacak";  // Varsayýlan durum

        // Görevin oluþturulma tarihi, varsayýlan olarak þu anki tarih
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        // Görev bir proje ile iliþkilidir
        public int ProjeId { get; set; }
        public Proje Proje { get; set; }

        // Görev ile iliþkili yorumlar
        public ICollection<Yorum> Yorumlar { get; set; }
    }
}
