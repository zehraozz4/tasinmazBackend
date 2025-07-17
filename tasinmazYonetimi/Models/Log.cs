using System.ComponentModel.DataAnnotations.Schema;

namespace tasinmazYonetimi.Models
{
    public class Log
    {
        public int logId { get; set; }
        public int? kullaniciId { get; set; }
        public string durum { get; set; }
        public string islemTipi { get; set; }
        public DateTime tarihSaat { get; set; }
        public string ip { get; set; }
        public string aciklama { get; set; }

        [ForeignKey("kullaniciId")]
        public Kullanici? Kullanici { get; set; }

    }
}
