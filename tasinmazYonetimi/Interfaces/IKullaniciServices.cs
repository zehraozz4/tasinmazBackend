using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Models;

namespace tasinmazYonetimi.Services
{
    public interface IKullaniciServices
    {
        Task<List<KullaniciDto>> GetAllAsync();
        Task<KullaniciDto?> GetByIdAsync(int id);
        Task <KullaniciDto>CreateAsync(KullaniciDto dto);
        Task<KullaniciDto?> UpdateAsync(int id, KullaniciDto dto);

        Task<KullaniciDto?> DeleteAsync(int id, KullaniciDto dto);

        Task<(List<Kullanici> data, int totalCount)> GetPagedKullanicilarAsync(int page, int pageSize);
    }
}
