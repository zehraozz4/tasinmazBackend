using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasinmazYonetimi.Data;
using tasinmazYonetimi.Models;

namespace tasinmazYonetimi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
    }
}
