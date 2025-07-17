using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Services;

namespace tasinmazYonetimi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciServices _kullaniciServices;

        public KullaniciController(IKullaniciServices kullaniciService)
        {
            _kullaniciServices = kullaniciService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var kullanicilar = await _kullaniciServices.GetAllAsync();
                return Ok(kullanicilar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var kullanici = await _kullaniciServices.GetByIdAsync(id);
                if (kullanici == null) return NotFound();
                return Ok(kullanici);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KullaniciDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.eMail) ||
                !Regex.IsMatch(dto.eMail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return BadRequest(new { error = "Geçerli bir e-posta adresi giriniz." });
            }

            if (string.IsNullOrWhiteSpace(dto.parola) || dto.parola.Length < 8 || !Regex.IsMatch(dto.parola, @"\d"))
            {
                return BadRequest(new { error = "Parola en az 8 karakterli olmalı ve en az bir rakam içermelidir." });
            }

            try
            {
                var olusanKullanici = await _kullaniciServices.CreateAsync(dto);
                return Ok(new
                {
                    message = "Kullanıcı başarıyla oluşturuldu.",
                    data = olusanKullanici
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Bir hata oluştu: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] KullaniciDto dto)
        {
            try
            {
                var guncellenenKullanici = await _kullaniciServices.UpdateAsync(id, dto);
                if (guncellenenKullanici == null)
                    return NotFound($"ID değeri {id} olan kullanıcı bulunamadı.");

                return Ok(new
                {
                    message = "Kullanıcı başarıyla güncellendi.",
                    data = guncellenenKullanici
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Bir hata oluştu.",
                    error = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] KullaniciDto dto)
        {
            try
            {
                var silinenKullanici = await _kullaniciServices.DeleteAsync(id, dto);
                if (silinenKullanici == null)
                    return NotFound(new { error = "Kullanıcı bulunamadı." });

                return Ok(new
                {
                    message = "Kullanıcı başarıyla silindi.",
                    data = silinenKullanici
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Beklenmeyen bir hata oluştu." });
            }
        }


        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedKullanicilar(int page = 1, int pageSize = 10)
        {
            var (data, totalCount) = await _kullaniciServices.GetPagedKullanicilarAsync(page, pageSize);

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                data
            });
        }


    }
}
