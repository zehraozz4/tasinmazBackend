using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Services.Interfaces;

namespace tasinmazYonetimi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasinmazController : ControllerBase
    {
        private readonly ITasinmazService _tasinmazServices;

        public TasinmazController(ITasinmazService tasinmazService)
        {
            _tasinmazServices = tasinmazService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _tasinmazServices.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Hata oluştu: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _tasinmazServices.GetByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Hata oluştu: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TasinmazDto dto)
        {
            try
            {
                var created = await _tasinmazServices.AddAsync(dto);
                return Ok(new { message = "Taşınmaz başarıyla eklendi." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Hata oluştu: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TasinmazDto dto)
        {
            try
            {
                var updated = await _tasinmazServices.UpdateAsync(id, dto);
                if (updated == null) return NotFound();

                return Ok("Tasinmaz basarıyla guncellendi");

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Hata oluştu: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] TasinmazDto dto)
        {
            try
            {
                var silinenTasinmaz = await _tasinmazServices.DeleteAsync(id, dto);
                if (silinenTasinmaz == null)
                    return NotFound();

                return Ok(new { message = "Silme işlemi başarılı.", data = silinenTasinmaz });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Hata oluştu: {ex.Message}" });
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedTasinmazlarlar(int page = 1, int pageSize = 10)
        {
            var (data, totalCount) = await _tasinmazServices.GetPagedTasinmazlarAsync(page, pageSize);

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