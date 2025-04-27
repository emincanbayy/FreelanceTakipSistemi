using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    public class Proje
    {
        [Key]
        public int ProjeId { get; set; }

        [Required(ErrorMessage = "Proje ad� zorunludur.")]
        public string ProjeAdi { get; set; }

        public string Aciklama { get; set; }

        [Required(ErrorMessage = "Ba�lang�� tarihi zorunludur.")]
        public DateTime BaslangicTarihi { get; set; }

        [Required(ErrorMessage = "Biti� tarihi zorunludur.")]
        public DateTime BitisTarihi { get; set; }

        [Required(ErrorMessage = "Durum zorunludur.")]
        public string Durum { get; set; } // Durum: Aktif veya Tamamland� gibi de�erler alacak

        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

        public ICollection<Gorev> Gorevler { get; set; }
    }
}
