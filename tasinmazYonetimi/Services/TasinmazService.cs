using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using tasinmazYonetimi.Data;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;
using tasinmazYonetimi.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace tasinmazYonetimi.Services
{
    public class TasinmazService : ITasinmazService
    {
        private readonly AppDbContext _context;
        private readonly ILogServices _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TasinmazService(AppDbContext context, IMapper mapper, ILogServices logService,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }

        private Tasinmaz DtoToEntity(TasinmazDto dto)
        {
            return new Tasinmaz
            {
                mahalleId = dto.mahalleId,
                adaa = dto.adaa,
                parsel = dto.parsel,
                nitelik = dto.nitelik,
                adres = dto.adres,
                kullaniciId = dto.kullaniciId,
                koordinat = dto.koordinat
            };
        }

        private TasinmazDto EntityToDto(Tasinmaz entity)
        {
            return new TasinmazDto
            {
                tasinmazId = entity.tasinmazId,
                mahalleId = entity.mahalleId,
                adaa = entity.adaa,
                parsel = entity.parsel,
                nitelik = entity.nitelik,
                adres = entity.adres,
                mahalleAd = entity.Mahalle.mahalleAd,
                ilceAd=entity.Mahalle.Ilce.ilceAd,
                ilAd=entity.Mahalle.Ilce.Il.ilAd,
                koordinat = entity.koordinat,
                kullaniciId = entity.kullaniciId ?? 0
            };
        }

        public async Task<List<TasinmazDto>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Tasinmaz
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Il)
                    .Include(t => t.Kullanici)
                    .ToListAsync();
                
                return entities.Select(EntityToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Tüm taşınmazlar alınırken hata oluştu.", ex);
            }
        }

        public async Task<TasinmazDto?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Tasinmaz
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Il)
                    .Include(t => t.Kullanici)
                    .FirstOrDefaultAsync(t => t.tasinmazId == id);

                return entity == null ? null : EntityToDto(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Taşınmaz detayları alınırken hata oluştu.", ex);
            }
        }

        public async Task<TasinmazDto> AddAsync(TasinmazDto dto)
        {
            try
            {
                var entity = DtoToEntity(dto);
                await _context.Tasinmaz.AddAsync(entity);
                await _context.SaveChangesAsync();

                var newEntity = await _context.Tasinmaz
                    .Include(t => t.Mahalle).ThenInclude(m => m.Ilce).ThenInclude(i => i.Il)
                    .Include(t => t.Kullanici)
                    .FirstOrDefaultAsync(t => t.tasinmazId == entity.tasinmazId);

                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = dto.kullaniciId,
                        durum = "Başarılı",
                        islemTipi = "Taşınmaz Ekleme",
                        tarihSaat = DateTime.UtcNow,
                        ip =GetClientIp(),
                        aciklama = $"Taşınmaz başarıyla eklendi:  Adres: {dto.adres}"
                    });
                }
                catch { }

                return EntityToDto(newEntity!);
            }
            catch (Exception ex)
            {
                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = dto.kullaniciId,
                        durum = "Başarısız",
                        islemTipi = "Taşınmaz Ekleme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Taşınmaz eklenirken hata oluştu: {ex.Message}"
                    });
                }
                catch { }

                throw new Exception("Taşınmaz eklenirken hata oluştu.", ex);
            }
        }


        public async Task<TasinmazDto?> UpdateAsync(int id, TasinmazDto dto)
        {
            var girisYapanAd = await _context.Log
                .Where(l => l.islemTipi == "Giriş")
                .OrderByDescending(l => l.tarihSaat)
                .FirstOrDefaultAsync();
            try
            {
                var entity = await _context.Tasinmaz.FindAsync(id);
                if (entity == null) return null;

                entity.mahalleId = dto.mahalleId;
                entity.adaa = dto.adaa;
                entity.parsel = dto.parsel;
                entity.nitelik = dto.nitelik;
                entity.adres = dto.adres;
                entity.kullaniciId = dto.kullaniciId;
                entity.koordinat = dto.koordinat;

                _context.Tasinmaz.Update(entity);
                await _context.SaveChangesAsync();

                var updatedEntity = await _context.Tasinmaz
                    .Include(t => t.Mahalle).ThenInclude(m => m.Ilce).ThenInclude(i => i.Il)
                    .Include(t => t.Kullanici)
                    .FirstOrDefaultAsync(t => t.tasinmazId == entity.tasinmazId);
                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = dto.kullaniciId,
                        durum = "Başarılı",
                        islemTipi = "Taşınmaz Güncelleme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Taşınmaz başarıyla güncellendi:  Adres: {dto.adres}"
                    });
                }
                catch (Exception logEx) {
                    Console.WriteLine("log hata" + logEx);
                }
                return EntityToDto(updatedEntity!);
            }
            catch (Exception ex)
            {
                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = dto.kullaniciId,
                        durum = "Başarısız",
                        islemTipi = "Taşınmaz Güncelleme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Taşınmaz güncellerken oluştu: {ex.Message}"
                    });
                }
                catch { }
                throw new Exception("Taşınmaz güncellenirken hata oluştu.", ex);
            }
        }

        public async Task<TasinmazDto?> DeleteAsync(int id, TasinmazDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = await _context.Tasinmaz.FindAsync(id);
                if (entity == null) return null;

                _context.Tasinmaz.Remove(entity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = dto.kullaniciId,
                        durum = "Başarılı",
                        islemTipi = "Taşınmaz Silme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Taşınmaz başarıyla silindi:  Adres: {dto.adres}"
                    });
                }
                catch { }

                return dto;
            }
            catch (Exception ex)
            {
                try
                {
                    await _logService.CreateAsync(new LogDto
                    {
                        kullaniciId = dto.kullaniciId,
                        durum = "Başarısız",
                        islemTipi = "Taşınmaz Silme",
                        tarihSaat = DateTime.UtcNow,
                        ip = GetClientIp(),
                        aciklama = $"Taşınmaz silinirken hata oluştu: {ex.Message}"
                    });
                }
                catch { }

                await transaction.RollbackAsync();
                throw new Exception("Taşınmaz silinirken hata oluştu.", ex);
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

        public async Task<(List<TasinmazDto> data, int totalCount)> GetPagedTasinmazlarAsync(int page, int pageSize)
        {
            var query = _context.Tasinmaz
                .Include(t => t.Mahalle)
                    .ThenInclude(m => m.Ilce)
                        .ThenInclude(i => i.Il)
                .OrderByDescending(k => k.tasinmazId);

            var totalCount = await query.CountAsync();

            var tasinmazlar = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtoList = tasinmazlar.Select(EntityToDto).ToList();
            return (dtoList, totalCount);
        }
    }
}
