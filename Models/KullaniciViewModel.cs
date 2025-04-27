using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    public class KullaniciViewModel
    {
        [Required]
        [Display(Name = "İsim")]
        public string Isim { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Parola { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Parola", ErrorMessage = "Parolalar eşleşmiyor.")]
        [Display(Name = "Parola (Tekrar)")]
        public string ParolaTekrar { get; set; }
    }
}
