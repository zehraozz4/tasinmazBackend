using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmazYonetimi.Models
{
    public class Ilce
    {
        public int ilceId { get; set; }
        public string ilceAd { get; set; }
        public int ilId { get; set; }

        [ForeignKey("ilId")]
        public Il Il { get; set; }

        public List<Mahalle> Mahalle { get; set; }
    }
}
