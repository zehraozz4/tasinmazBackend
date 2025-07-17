using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace tasinmazYonetimi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginServices;
        public LoginController(ILoginService loginService)
        {
            _loginServices = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var kullanici = await _loginServices.LoginAsync(dto);
                if (kullanici == null)
                {
                    return Unauthorized(new { error = "EMail veya parola hatalı!!!" });
                }

                return Ok(new
                {
                    mesaj = "Giriş başarılı!!!",
                    eMail = kullanici.eMail,
                    kullaniciId = kullanici.kullaniciId,
                    rol = kullanici.rol
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "Sunucu hatası: " + ex.Message });
            }
        }
    }
}
