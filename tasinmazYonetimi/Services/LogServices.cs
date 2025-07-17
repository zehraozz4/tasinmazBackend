using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;
using tasinmazYonetimi.Data;
using Microsoft.EntityFrameworkCore;

namespace tasinmazYonetimi.Services
{
    public class LogServices : ILogServices
    {
        private readonly AppDbContext _context;
        
        public LogServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LogDto>> GetAllAsync()
        {
            try
            {
                return await _context.Log
                    .Select(l => new LogDto
                    {
                        logId = l.logId,
                        kullaniciId = l.kullaniciId,
                        durum = l.durum,
                        islemTipi = l.islemTipi,
                        tarihSaat = l.tarihSaat,
                        ip = l.ip,
                        aciklama = l.aciklama
                    })
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<LogDto?> GetByIdAsync(int id)
        {
            try
            {
                var l = await _context.Log.FindAsync(id);
                if (l == null) return null;

                return new LogDto
                {
                    logId = l.logId,
                    kullaniciId = l.kullaniciId,
                    durum = l.durum,
                    islemTipi = l.islemTipi,
                    tarihSaat = l.tarihSaat,
                    ip = l.ip,
                    aciklama = l.aciklama
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<LogDto> CreateAsync(LogDto dto)
        {
            try
            {
                var log = new Log
                {
                    kullaniciId = dto.kullaniciId ?? 0,
                    durum = dto.durum,
                    islemTipi = dto.islemTipi,
                    tarihSaat = dto.tarihSaat,
                    ip = dto.ip,
                    aciklama = dto.aciklama
                };

                await _context.Log.AddAsync(log);
                await _context.SaveChangesAsync();

                return new LogDto
                {
                    logId = log.logId,
                    kullaniciId = log.kullaniciId,
                    durum = log.durum,
                    islemTipi = log.islemTipi,
                    tarihSaat = log.tarihSaat,
                    ip = log.ip,
                    aciklama = log.aciklama
                };
            }
            catch
            {
                throw;
            }
        }


        public async Task<LogDto?> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var log = await _context.Log.FindAsync(id);
                if (log == null)
                    return null;

                _context.Log.Remove(log);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new LogDto
                {
                    logId = log.logId,
                    kullaniciId = log.kullaniciId,
                    durum = log.durum,
                    islemTipi = log.islemTipi,
                    tarihSaat = log.tarihSaat,
                    ip = log.ip,
                    aciklama = log.aciklama
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<(List<Log> data, int totalCount)> GetPagedLogsAsync(int page, int pageSize)
        {
            var query = _context.Log.OrderByDescending(l => l.tarihSaat);
            var totalCount = await query.CountAsync();
            var logs = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (logs, totalCount);
        }

    }
}
