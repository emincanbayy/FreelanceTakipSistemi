using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    public class Kullanici
    {
        [Key]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Ýsim zorunludur.")]
        public string Isim { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Parola { get; set; }

        // Rol: Admin veya Kullanici
        public string Rol { get; set; } = "Kullanici";

        // Ýliþkili projeler
        public ICollection<Proje> Projeler { get; set; }
    }
}