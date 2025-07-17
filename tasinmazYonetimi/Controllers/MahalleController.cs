using Microsoft.AspNetCore.Mvc;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Services;
using tasinmazYonetimi.Services.Interfaces;

namespace tasinmazYonetimi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahalleController : ControllerBase
    {
        private readonly IMahalleServices _mahalleServices;

        public MahalleController(IMahalleServices service)
        {
            _mahalleServices = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var mahalleler = await _mahalleServices.GetAllAsync();
                return Ok(mahalleler);
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
                var mahalle = await _mahalleServices.GetByIdAsync(id);
                if (mahalle == null) return NotFound();
                return Ok(mahalle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MahalleDto dto)
        {
            try
            {
                var olusanMahalle = await _mahalleServices.CreateAsync(dto);
                return Ok(new
                {
                    message = "Mahalle başarıyla eklendi.",
                    data = olusanMahalle
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = $"Mahalle eklenemedi: {ex.Message}"
                });
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MahalleDto dto)
        {
            try
            {
                var guncellenenMahalle = await _mahalleServices.UpdateAsync(id, dto);
                if (guncellenenMahalle == null)
                    return NotFound($"ID değeri {id} olan mahalle bulunamadı.");

                return Ok(new
                {
                    message = "Mahalle başarıyla güncellendi.",
                    data = guncellenenMahalle
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var silinenMahalle = await _mahalleServices.DeleteAsync(id);
                if (silinenMahalle == null)
                    return NotFound($"ID değeri {id} olan mahalle bulunamadı.");

                return Ok(new
                {
                    message = "Mahalle başarıyla silindi.",
                    data = silinenMahalle
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

    }
}