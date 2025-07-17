using tasinmazYonetimi.Dtos;

namespace tasinmazYonetimi.Services.Interfaces
{
    public interface IIlceServices
    {
        Task<List<IlceDto>> GetAllAsync();
        Task<IlceDto?> GetByIdAsync(int id);
        Task CreateAsync(IlceDto dto);
        Task<IlceDto> UpdateAsync(int id, IlceDto dto);
        Task<IlceDto> DeleteAsync(int id);
    }
}