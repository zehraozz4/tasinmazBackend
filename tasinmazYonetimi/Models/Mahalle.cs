using System.ComponentModel.DataAnnotations.Schema;
namespace tasinmazYonetimi.Models
{
    public class Mahalle
    {
        public int mahalleId { get; set; }
        public string mahalleAd { get; set; }
        public int ilceId { get; set; }

        [ForeignKey("ilceId")]
        public Ilce Ilce { get; set; }
        public List<Tasinmaz> Tasinmaz { get; set; }
    }
}
