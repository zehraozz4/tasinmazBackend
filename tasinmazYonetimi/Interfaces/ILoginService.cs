using System.Threading.Tasks;
using tasinmazYonetimi.Dtos;

namespace tasinmazYonetimi.Interfaces
{
    public interface ILoginService
    {
        Task<KullaniciDto?> LoginAsync(LoginDto loginDto, string? ip = null);

    }
}
