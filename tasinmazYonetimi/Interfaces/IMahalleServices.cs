using tasinmazYonetimi.Dtos;

namespace tasinmazYonetimi.Services.Interfaces
{
    public interface IMahalleServices
    {
        Task<List<MahalleDto>> GetAllAsync();
        Task<MahalleDto?> GetByIdAsync(int id);
        Task<MahalleDto> CreateAsync(MahalleDto dto);
        Task<MahalleDto?> UpdateAsync(int id, MahalleDto dto);
        Task<MahalleDto?> DeleteAsync(int id);


    }
}

