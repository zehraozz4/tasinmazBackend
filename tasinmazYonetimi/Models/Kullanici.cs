namespace tasinmazYonetimi.Models
{
    public class Kullanici
    {
        public int kullaniciId { get; set; }
        public string kullaniciAd { get; set; }
        public string kullaniciSoyad { get; set; }
        public string eMail { get; set; }
        public string parola { get; set; }
        public string rol { get; set; }
        public string adres { get; set; }
        public DateTime eklenmeTarihi { get; set; }
        public DateTime guncellemeTarihi { get; set; }
        public List<Log> Log { get; set; }
        public List<Tasinmaz> Tasinmaz { get; set; }
    }
}
