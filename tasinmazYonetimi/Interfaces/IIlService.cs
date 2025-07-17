using tasinmazYonetimi.Dtos;

namespace tasinmazYonetimi.Services.Interfaces
{
    public interface IIlService
    {
        Task<List<IlDto>> GetAllAsync();
        Task<IlDto> GetByIdAsync(int id);
        Task<IlDto> CreateAsync(IlDto dto);
        Task<IlDto> UpdateAsync(int id, IlDto dto);
        Task<IlDto> DeleteAsync(int id);
    }
}