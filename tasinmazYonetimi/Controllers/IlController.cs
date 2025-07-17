using Microsoft.AspNetCore.Mvc;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Services;
using tasinmazYonetimi.Services.Interfaces;

namespace tasinmazYonetimi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IlController : ControllerBase
    {
        private readonly IIlService _ilServices;

        public IlController(IIlService ilService)
        {
            _ilServices = ilService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var iller = await _ilServices.GetAllAsync();
                return Ok(iller);
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
                var il = await _ilServices.GetByIdAsync(id);
                if (il == null)
                    return NotFound($"ID değeri {id} olan il bulunamadı.");

                return Ok(il);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IlDto dto)
        {
            try
            {
                await _ilServices.CreateAsync(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Bir hata oluştu: {ex.Message}" });
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IlDto dto)
        {
            try
            {
                var guncellenenIl = await _ilServices.UpdateAsync(id, dto);
                if (guncellenenIl == null)
                    return NotFound($"ID değeri {id} olan il bulunamadı.");

                return Ok(guncellenenIl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var silinenIl = await _ilServices.DeleteAsync(id);
                if (silinenIl == null)
                    return NotFound($"ID değeri {id} olan il bulunamadı.");

                return Ok(new
                {
                    message = "Silme işlemi başarılı.",
                    data = silinenIl
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
