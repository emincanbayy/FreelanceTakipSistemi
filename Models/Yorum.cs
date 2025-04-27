using System.ComponentModel.DataAnnotations;

namespace FreelanceTakipSistemi.Models
{
    public class Yorum
    {
        [Key]
        public int YorumId { get; set; }

        public string Icerik { get; set; }

        public DateTime OlusturulmaTarihi { get; set; } = DateTime.Now;

        public int GorevId { get; set; }
        public Gorev Gorev { get; set; }
    }
}
