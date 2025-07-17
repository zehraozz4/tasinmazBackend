using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;

namespace tasinmazYonetimi.Services.Interfaces
{
    public interface ITasinmazService
    {
        Task<List<TasinmazDto>> GetAllAsync();
        Task<TasinmazDto?> GetByIdAsync(int id);
        Task<TasinmazDto> AddAsync(TasinmazDto dto);
        Task<TasinmazDto?> UpdateAsync(int id, TasinmazDto dto);
        Task<TasinmazDto?> DeleteAsync(int id, TasinmazDto dto);
        Task<(List<TasinmazDto> data, int totalCount)> GetPagedTasinmazlarAsync(int page, int pageSize);



    }
}
