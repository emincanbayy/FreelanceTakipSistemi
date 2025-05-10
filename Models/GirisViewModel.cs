using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    public class GirisViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Parola { get; set; }
    }
}