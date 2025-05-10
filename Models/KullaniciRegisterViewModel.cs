// Models/KullaniciRegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    public class KullaniciRegisterViewModel
    {
        [Required(ErrorMessage = "İsim zorunludur.")]
        public string Isim { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola zorunludur.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Parola en az 6 karakter olmalı.")]
        public string Parola { get; set; }

        [Required(ErrorMessage = "Parola tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola (Tekrar)")]
        [Compare(nameof(Parola), ErrorMessage = "Parolalar eşleşmiyor.")]
        public string ParolaTekrar { get; set; }
    }
}
