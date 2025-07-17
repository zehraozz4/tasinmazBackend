using Microsoft.AspNetCore.Mvc;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Services;
using tasinmazYonetimi.Services.Interfaces;

namespace tasinmazYonetimi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlceController : ControllerBase
    {
        private readonly IIlceServices _ilceServices;

        public IlceController(IIlceServices ilceServices)
        {
            _ilceServices = ilceServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ilceler = await _ilceServices.GetAllAsync();
                return Ok(ilceler);
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
                var ilce = await _ilceServices.GetByIdAsync(id);
                if (ilce == null) return NotFound();
                return Ok(ilce);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IlceDto dto)
        {
            try
            {
                await _ilceServices.CreateAsync(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Bir hata oluştu: {ex.Message}" });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IlceDto dto)
        {
            try
            {
                var guncellenenIlce = await _ilceServices.UpdateAsync(id, dto);
                if (guncellenenIlce == null)
                    return NotFound($"ID değeri {id} olan ilçe bulunamadı.");

                return Ok(new
                {
                    message = "Güncelleme başarılı.",
                    data = guncellenenIlce
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var silinenIlce = await _ilceServices.DeleteAsync(id);
                if (silinenIlce == null)
                    return NotFound($"ID değeri {id} olan ilçe bulunamadı.");

                return Ok(new
                {
                    message = "İlçe başarıyla silindi.",
                    data = silinenIlce
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

    }
}
