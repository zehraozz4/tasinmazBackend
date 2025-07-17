using tasinmazYonetimi.Models;

namespace tasinmazYonetimi.Dtos
{
    public class TasinmazDto
    {
        public int tasinmazId { get; set; }
        public string adaa { get; set; }
        public string parsel { get; set; }
        public string nitelik { get; set; }
        public string adres { get; set; }
        public int mahalleId { get; set; }
        public string koordinat { get; set; }
        public string mahalleAd { get; set; }
        public string ilAd { get; set; }
        public string ilceAd { get; set; }
        public int kullaniciId { get; set; }
    }
}
