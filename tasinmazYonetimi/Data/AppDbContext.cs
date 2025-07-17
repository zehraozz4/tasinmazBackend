using Microsoft.EntityFrameworkCore;
using tasinmazYonetimi.Models;
namespace tasinmazYonetimi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Il> Il { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Mahalle> Mahalle { get; set; }
        public DbSet<Kullanici> Kullanici{ get; set; }
        public DbSet<Tasinmaz> Tasinmaz { get; set; }
        public DbSet<Log> Log { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ilce>()
                .HasOne(ilce => ilce.Il)
                .WithMany(il => il.Ilce)
                .HasForeignKey(ilce => ilce.ilId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Mahalle>()
                .HasOne(mahalle => mahalle.Ilce)
                .WithMany(ilce => ilce.Mahalle)
                .HasForeignKey(mahalle => mahalle.ilceId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Tasinmaz>()
                .HasOne(tasinmaz => tasinmaz.Mahalle)
                .WithMany(mahalle => mahalle.Tasinmaz)
                .HasForeignKey(tasinmaz => tasinmaz.mahalleId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Tasinmaz>()
                .HasOne(tasinmaz => tasinmaz.Kullanici)
                .WithMany(mahalle => mahalle.Tasinmaz)
                .HasForeignKey(tasinmaz => tasinmaz.tasinmazId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Log>()
                .HasOne(log => log.Kullanici)
                .WithMany(kullanici => kullanici.Log)
                .HasForeignKey(log => log.logId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Il>()
                .Property(p => p.ilAd)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Ilce>()
                .Property(p => p.ilceAd)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Mahalle>()
                .Property(p => p.mahalleAd)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Kullanici>()
                .Property(p => p.kullaniciAd)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Kullanici>()
                .Property(p => p.kullaniciSoyad)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Kullanici>()
                .Property(p => p.eMail)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Kullanici>()
                .Property(p => p.parola)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Kullanici>()
                .Property(p => p.rol)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Kullanici>()
                .Property(p => p.adres)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Tasinmaz>()
                .Property(p => p.adaa)
                .HasMaxLength(100);
            modelBuilder.Entity<Tasinmaz>()
                .Property(p => p.parsel)
                .HasMaxLength(250);
            modelBuilder.Entity<Tasinmaz>()
                .Property(p => p.nitelik)
                .HasMaxLength(100);
            modelBuilder.Entity<Tasinmaz>()
                .Property(p => p.adres)
                .HasMaxLength(250);
            base.OnModelCreating(modelBuilder);
        }*/
    }
}
