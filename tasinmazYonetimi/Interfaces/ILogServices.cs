using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;

namespace tasinmazYonetimi.Services
{
    public interface ILogServices
    {
        Task<List<LogDto>> GetAllAsync();
        Task<LogDto?> GetByIdAsync(int id);
        Task<LogDto> CreateAsync(LogDto dto);

        Task<LogDto?> DeleteAsync(int id);

        Task<(List<Log> data, int totalCount)> GetPagedLogsAsync(int page, int pageSize);

    }
}