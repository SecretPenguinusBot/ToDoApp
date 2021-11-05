

using backend.Application;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Health
{
    [ApiController, Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public HealthController(ApplicationContext context) => _context = context;

        [HttpGet]
        public IActionResult GetAppConteInfo() => Ok(_context);
    }
}