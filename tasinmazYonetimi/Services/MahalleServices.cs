using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;
using tasinmazYonetimi.Services.Interfaces;
using tasinmazYonetimi.Data;
using Microsoft.EntityFrameworkCore;

namespace tasinmazYonetimi.Services
{
    public class MahalleServices : IMahalleServices
    {
        private readonly AppDbContext _context;

        public MahalleServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MahalleDto>> GetAllAsync()
        {
            try
            {
                return await _context.Mahalle
                    .Select(m => new MahalleDto
                    {
                        mahalleId = m.mahalleId,
                        mahalleAd = m.mahalleAd,
                        ilceId = m.ilceId
                    })
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<MahalleDto?> GetByIdAsync(int id)
        {
            try
            {
                var mahalle = await _context.Mahalle.FindAsync(id);
                if (mahalle == null) return null;

                return new MahalleDto
                {
                    mahalleId = mahalle.mahalleId,
                    mahalleAd = mahalle.mahalleAd,
                    ilceId = mahalle.ilceId
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<MahalleDto> CreateAsync(MahalleDto dto)
        {
            try
            {
                var mahalle = new Mahalle
                {
                    mahalleAd = dto.mahalleAd,
                    ilceId = dto.ilceId
                };

                await _context.Mahalle.AddAsync(mahalle);
                await _context.SaveChangesAsync();

                return new MahalleDto
                {
                    mahalleId = mahalle.mahalleId,
                    mahalleAd = mahalle.mahalleAd,
                    ilceId = mahalle.ilceId
                };
            }
            catch
            {
                throw;
            }
        }


        public async Task<MahalleDto?> UpdateAsync(int id, MahalleDto dto)
        {
            try
            {
                var mahalle = await _context.Mahalle.FindAsync(id);
                if (mahalle == null)
                    return null;

                mahalle.mahalleAd = dto.mahalleAd;
                mahalle.ilceId = dto.ilceId;

                await _context.SaveChangesAsync();

                return new MahalleDto
                {
                    mahalleId = mahalle.mahalleId,
                    mahalleAd = mahalle.mahalleAd,
                    ilceId = mahalle.ilceId
                };
            }
            catch
            {
                throw;
            }
        }


        public async Task<MahalleDto?> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var mahalle = await _context.Mahalle.FindAsync(id);
                if (mahalle == null)
                    return null;

                _context.Mahalle.Remove(mahalle);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new MahalleDto
                {
                    mahalleId = mahalle.mahalleId,
                    mahalleAd = mahalle.mahalleAd,
                    ilceId = mahalle.ilceId
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
