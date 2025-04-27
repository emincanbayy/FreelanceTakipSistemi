using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Ýsim alaný zorunludur.")]
        public string Isim { get; set; }

        [Required(ErrorMessage = "E-posta alaný zorunludur."), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola alaný zorunludur.")]
        public string Parola { get; set; }

        public string Rol { get; set; } = "Kullanici";

        public ICollection<Proje> Projeler { get; set; }
    }
}
