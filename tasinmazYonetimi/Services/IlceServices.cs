using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;
using tasinmazYonetimi.Services.Interfaces;
using tasinmazYonetimi.Data;
using Microsoft.EntityFrameworkCore;

namespace tasinmazYonetimi.Services
{
    public class IlceServices : IIlceServices
    {
        private readonly AppDbContext _context;

        public IlceServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<IlceDto>> GetAllAsync()
        {
            try
            {
                return await _context.Ilce
                    .Select(i => new IlceDto
                    {
                        ilceId = i.ilceId,
                        ilceAd = i.ilceAd,
                        ilId = i.ilId
                    })
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IlceDto?> GetByIdAsync(int id)
        {
            try
            {
                var ilce = await _context.Ilce.FindAsync(id);
                if (ilce == null) return null;

                return new IlceDto
                {
                    ilceId = ilce.ilceId,
                    ilceAd = ilce.ilceAd,
                    ilId = ilce.ilId
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task CreateAsync(IlceDto dto)
        {
            try
            {
                var ilce = new Ilce
                {
                    ilceAd = dto.ilceAd,
                    ilId = dto.ilId
                };

                await _context.Ilce.AddAsync(ilce);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IlceDto?> UpdateAsync(int id, IlceDto dto)
        {
            try
            {
                var ilce = await _context.Ilce.FindAsync(id);
                if (ilce == null)
                    return null;

                ilce.ilceAd = dto.ilceAd;
                ilce.ilId = dto.ilId;

                await _context.SaveChangesAsync();

                return new IlceDto
                {
                    ilceId = ilce.ilceId,
                    ilceAd = ilce.ilceAd,
                    ilId = ilce.ilId
                };
            }
            catch
            {
                throw;
            }
        }


        public async Task<IlceDto?> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var ilce = await _context.Ilce.FindAsync(id);
                if (ilce == null)
                    return null;

                _context.Ilce.Remove(ilce);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new IlceDto
                {
                    ilceId = ilce.ilceId,
                    ilceAd = ilce.ilceAd,
                    ilId = ilce.ilId
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
