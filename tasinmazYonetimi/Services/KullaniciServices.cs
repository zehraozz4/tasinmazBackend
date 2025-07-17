using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;
using tasinmazYonetimi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace tasinmazYonetimi.Services
{
    public class KullaniciServices : IKullaniciServices
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Kullanici> _passwordHasher;
        private readonly ILogServices _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KullaniciServices(AppDbContext context, IPasswordHasher<Kullanici> passwordHasher, ILogServices logService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<KullaniciDto>> GetAllAsync()
        {
            try
            {
                return await _context.Kullanici
                    .Select(k => new KullaniciDto
                    {
                        kullaniciId = k.kullaniciId,
                        kullaniciAd = k.kullaniciAd,
                        kullaniciSoyad = k.kullaniciSoyad,
                        eMail = k.eMail,
                        parola = k.parola,
                        rol = k.rol,
                        adres = k.adres,
                        eklenmeTarihi = k.eklenmeTarihi,
                        guncellemeTarihi = k.guncellemeTarihi
                    })
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<KullaniciDto?> GetByIdAsync(int id)
        {
            try
            {
                var k = await _context.Kullanici.FindAsync(id);
                if (k == null) return null;

                return new KullaniciDto
                {
                    kullaniciId = k.kullaniciId,
                    kullaniciAd = k.kullaniciAd,
                    kullaniciSoyad = k.kullaniciSoyad,
                    eMail = k.eMail,
                    parola = null,
                    rol = k.rol,
                    adres = k.adres,
                    eklenmeTarihi = k.eklenmeTarihi,
                    guncellemeTarihi = k.guncellemeTarihi
                };
            }
            catch
            {
                throw;
            }
        }
        public async Task<KullaniciDto> CreateAsync(KullaniciDto dto)
        {
            var girisYapanId = await _context.Log
                .Where(l => l.islemTipi == "Giriş")
                .OrderByDescending(l => l.tarihSaat)
                .Select(l => l.kullaniciId)
                .FirstOrDefaultAsync();

            try
            {
                var k = new Kullanici
                {
                    kullaniciAd = dto.kullaniciAd,
                    kullaniciSoyad = dto.kullaniciSoyad,
                    eMail = dto.eMail,
                    rol = dto.rol,
                    adres = dto.adres,
                    eklenmeTarihi = dto.eklenmeTarihi,
                    guncellemeTarihi = dto.guncellemeTarihi
                };

                k.parola = _passwordHasher.HashPassword(k, dto.parola);

                await _context.Kullanici.AddAsync(k);
                await _context.SaveChangesAsync();

                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = girisYapanId,
                        durum = "Başarılı",
                        islemTipi = "Kullanıcı Ekleme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Kullanıcı başarıyla eklendi: {dto.eMail}"
                    });
                }
                catch { }

                dto.kullaniciId = k.kullaniciId;
                dto.parola = null;

                return dto;
            }
            catch (Exception ex)
            {
                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = girisYapanId,
                        durum = "Başarısız",
                        islemTipi = "Kullanıcı Ekleme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Kullanıcı eklenirken hata oluştu: {ex.Message}"
                    });
                }
                catch { }

                throw;
            }
        }


        public async Task<KullaniciDto?> UpdateAsync(int id, KullaniciDto dto)
        {
            var girisYapanId = await _context.Log
                .Where(l => l.islemTipi == "Giriş")
                .OrderByDescending(l => l.tarihSaat)
                .Select(l => l.kullaniciId)
                .FirstOrDefaultAsync();

            try
            {
                var k = await _context.Kullanici.FindAsync(id);
                if (k == null) return null;

                k.kullaniciAd = dto.kullaniciAd;
                k.kullaniciSoyad = dto.kullaniciSoyad;
                k.eMail = dto.eMail;
                if (!string.IsNullOrWhiteSpace(dto.parola))
                {
                    k.parola = _passwordHasher.HashPassword(k, dto.parola);
                }
                k.rol = dto.rol;
                k.adres = dto.adres;
                k.eklenmeTarihi = dto.eklenmeTarihi;
                k.guncellemeTarihi = dto.guncellemeTarihi;

                await _context.SaveChangesAsync();

                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = girisYapanId,
                        durum = "Başarılı",
                        islemTipi = "Kullanıcı Güncelleme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Kullanıcı başarıyla güncellendi: {dto.eMail}"
                    });
                }
                catch { }

                return new KullaniciDto
                {
                    kullaniciId = k.kullaniciId,
                    kullaniciAd = k.kullaniciAd,
                    kullaniciSoyad = k.kullaniciSoyad,
                    eMail = k.eMail,
                    rol = k.rol,
                    adres = k.adres,
                    eklenmeTarihi = k.eklenmeTarihi,
                    guncellemeTarihi = k.guncellemeTarihi,
                    parola = null
                };
            }
            catch (Exception ex)
            {
                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = girisYapanId,
                        durum = "Başarısız",
                        islemTipi = "Kullanıcı Güncelleme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Kullanıcı güncellerken hata oluştu: {ex.Message}"
                    });
                }
                catch { }
                throw;
            }
        }

        public async Task<KullaniciDto?> DeleteAsync(int id, KullaniciDto dto)
        {
            var logs = await _context.Log
                .Where(l => l.kullaniciId == id)
                .ToListAsync();

            foreach (var log in logs)
            {
                log.kullaniciId = null;
            }
            await _context.SaveChangesAsync();

            var girisYapanId = await _context.Log
                .Where(l => l.islemTipi == "Giriş")
                .OrderByDescending(l => l.tarihSaat)
                .Select(l => l.kullaniciId)
                .FirstOrDefaultAsync();

            try
            {
                var tasinmazlar = await _context.Tasinmaz.Where(t => t.kullaniciId == id).ToListAsync();
                if (tasinmazlar.Any())
                {
                    _context.Tasinmaz.RemoveRange(tasinmazlar);
                }

                await _context.SaveChangesAsync();

                var k = await _context.Kullanici.FindAsync(id);
                if (k == null) return null;

                _context.Kullanici.Remove(k);
                await _context.SaveChangesAsync();

                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = girisYapanId,
                        durum = "Başarılı",
                        islemTipi = "Kullanıcı Silme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Kullanıcı başarıyla silindi: {dto.eMail}"
                    });
                }
                catch { }

                return new KullaniciDto
                {
                    kullaniciId = k.kullaniciId,
                    kullaniciAd = k.kullaniciAd,
                    kullaniciSoyad = k.kullaniciSoyad,
                    eMail = k.eMail,
                    rol = k.rol,
                    adres = k.adres,
                    eklenmeTarihi = k.eklenmeTarihi,
                    guncellemeTarihi = k.guncellemeTarihi,
                    parola = null
                };
            }
            catch (Exception ex)
            {
                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = girisYapanId,
                        durum = "Başarısız",
                        islemTipi = "Kullanıcı Silme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Kullanıcı silinirken hata oluştu: {ex.Message}"
                    });
                }
                catch { }

                throw;
            }
        }

        private string GetClientIp()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return "IP bulunamadı";

            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(ip))
                return ip;

            var remoteIp = context.Connection.RemoteIpAddress;

            if (remoteIp == null)
                return "IP bulunamadı";

            if (IPAddress.IPv6Loopback.Equals(remoteIp))
                return IPAddress.Loopback.ToString();

            if (remoteIp.IsIPv4MappedToIPv6)
                remoteIp = remoteIp.MapToIPv4();

            return remoteIp.ToString();
        }
        public async Task<(List<Kullanici> data, int totalCount)> GetPagedKullanicilarAsync(int page, int pageSize)
        {
            var query = _context.Kullanici.OrderByDescending(k => k.kullaniciId); 
            var totalCount = await query.CountAsync();
            var kullanicilar = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (kullanicilar, totalCount);
        }


    }
}
