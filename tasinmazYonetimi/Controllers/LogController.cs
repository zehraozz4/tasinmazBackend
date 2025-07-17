using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasinmazYonetimi.Dtos;
using tasinmazYonetimi.Services;

namespace tasinmazYonetimi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogServices _logServices;

        public LogController(ILogServices logServices)
        {
            _logServices = logServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var logs = await _logServices.GetAllAsync();
                return Ok(logs);
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
                var log = await _logServices.GetByIdAsync(id);
                if (log == null) return NotFound();
                return Ok(log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LogDto dto)
        {
            try
            {
                var olusanLog = await _logServices.CreateAsync(dto);
                return Ok(new
                {
                    message = "Log başarıyla oluşturuldu.",
                    data = olusanLog
                });
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
                var silinenLog = await _logServices.DeleteAsync(id);
                if (silinenLog == null)
                    return NotFound($"ID değeri {id} olan log bulunamadı.");

                return Ok(new
                {
                    message = "Log başarıyla silindi.",
                    data = silinenLog
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }


        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedLogs(int page = 1, int pageSize = 10)
        {
            var (data, totalCount) = await _logServices.GetPagedLogsAsync(page, pageSize);

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