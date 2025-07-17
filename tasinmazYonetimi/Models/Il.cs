namespace tasinmazYonetimi.Models
{
    public class Il
    {
        public int ilId { get; set; }
        public string ilAd { get; set; }
        public List<Ilce> Ilce { get; set; }
    }
}
