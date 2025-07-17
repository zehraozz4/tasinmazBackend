using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace tasinmazYonetimi.Models
{
    public class Tasinmaz
    {
        public int tasinmazId { get; set; }
        public int mahalleId { get; set; }
        public string adaa { get; set; }
        public string parsel { get; set; }
        public string nitelik { get; set; }
        public string adres { get; set; }
        public int? kullaniciId { get; set; } 
        public string koordinat { get; set; }

        [ForeignKey("kullaniciId")]
        public Kullanici Kullanici { get; set; }

        [ForeignKey("mahalleId")]
        public Mahalle Mahalle { get; set; }

    }

}
