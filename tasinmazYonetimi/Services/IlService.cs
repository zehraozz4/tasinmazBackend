using Microsoft.EntityFrameworkCore;
using tasinmazYonetimi.Data;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;
using tasinmazYonetimi.Services.Interfaces;

namespace tasinmazYonetimi.Services
{
    public class IlService : IIlService
    {
        private readonly AppDbContext _context;

        public IlService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<IlDto>> GetAllAsync()
        {
            try
            {
                var iller = await _context.Il
                    .Select(x => new IlDto
                    {
                        ilId = x.ilId,
                        ilAd = x.ilAd
                    })
                    .ToListAsync();

                return iller;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IlDto> GetByIdAsync(int id)
        {
            try
            {
                var il = await _context.Il.FindAsync(id);

                if (il == null)
                    return null;

                return new IlDto
                {
                    ilId = il.ilId,
                    ilAd = il.ilAd
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<IlDto> CreateAsync(IlDto dto)
        {
            try
            {
                var il = new Il
                {
                    ilAd = dto.ilAd
                };

                _context.Il.Add(il);
                await _context.SaveChangesAsync();

                dto.ilId = il.ilId;

                return dto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IlDto> UpdateAsync(int id, IlDto dto)
        {
            try
            {
                var il = await _context.Il.FindAsync(id);
                if (il == null)
                    return null;

                il.ilAd = dto.ilAd;

                await _context.SaveChangesAsync();

                return new IlDto
                {
                    ilId = il.ilId,
                    ilAd = il.ilAd
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<IlDto?> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var il = await _context.Il.FindAsync(id);
                if (il == null)
                    return null;

                _context.Il.Remove(il);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var dto = new IlDto
                {
                    ilId = il.ilId,
                    ilAd = il.ilAd
                };

                return dto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
