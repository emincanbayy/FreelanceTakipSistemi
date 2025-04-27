using System.ComponentModel.DataAnnotations;
using System;

namespace FreelanceTakipSistemi.Models
{
    public class Gorev
    {
        [Key]
        public int GorevId { get; set; }

        // Ba�l�k zorunludur
        [Required(ErrorMessage = "Ba�l�k zorunludur.")]
        public string Baslik { get; set; }

        // A��klama iste�e ba�l�d�r
        public string Aciklama { get; set; }

        // Durum, varsay�lan olarak "Yap�lacak" olacakt�r
        [Required(ErrorMessage = "Durum zorunludur.")]
        public string Durum { get; set; } = "Yap�lacak";  // Varsay�lan durum

        // G�revin olu�turulma tarihi, varsay�lan olarak �u anki tarih
        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        // G�rev bir proje ile ili�kilidir
        public int ProjeId { get; set; }
        public Proje Proje { get; set; }

        // G�rev ile ili�kili yorumlar
        public ICollection<Yorum> Yorumlar { get; set; }
    }
}
