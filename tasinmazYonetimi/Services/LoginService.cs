using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using tasinmazYonetimi.Data;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Interfaces;
using tasinmazYonetimi.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace tasinmazYonetimi.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Kullanici> _passwordHasher;
        private readonly ILogServices _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LoginService(AppDbContext context, IPasswordHasher<Kullanici> passwordHasher, ILogServices logService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<KullaniciDto?> LoginAsync(LoginDto loginDto, string? ip = null)
        {
            try
            {
                var kullanici = await _context.Kullanici
                    .FirstOrDefaultAsync(k => k.eMail == loginDto.eMail);

                if (kullanici == null)
                {
                    try
                    {
                        await _logService.CreateAsync(new LogDto
                        {
                            kullaniciId = 0,
                            durum = "Başarısız",
                            islemTipi = "Giriş",
                            tarihSaat = DateTime.UtcNow,
                            ip = GetClientIp(),
                            aciklama = $"E-mail ile eşleşen kullanıcı bulunamadı: {loginDto.eMail}"
                        });
                    }
                    catch { }

                    return null;
                }

                var result = _passwordHasher.VerifyHashedPassword(kullanici, kullanici.parola, loginDto.parola);

                if (result == PasswordVerificationResult.Success)
                {
                    try
                    {
                        await _logService.CreateAsync(new LogDto
                        {
                            kullaniciId= kullanici.kullaniciId,
                            durum = "Başarılı",
                            islemTipi = "Giriş",
                            tarihSaat = DateTime.UtcNow,
                            ip = GetClientIp(),
                            aciklama = $"Kullanıcı giriş yaptı: {kullanici.eMail}"
                        });
                    }
                    catch { }

                    return new KullaniciDto
                    {
                        kullaniciId = kullanici.kullaniciId,
                        kullaniciAd = kullanici.kullaniciAd,
                        kullaniciSoyad = kullanici.kullaniciSoyad,
                        eMail = kullanici.eMail,
                        rol = kullanici.rol,
                        adres = kullanici.adres,
                        eklenmeTarihi = kullanici.eklenmeTarihi,
                        guncellemeTarihi = kullanici.guncellemeTarihi
                    };
                }

                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = kullanici.kullaniciId,
                        durum = "Başarısız",
                        islemTipi = "Giriş",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Hatalı parola girildi: {kullanici.eMail}"
                    });
                }
                catch { }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login işleminde hata oluştu: {ex.Message}");
                return null;
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

    }
}
