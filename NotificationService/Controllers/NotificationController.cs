using Microsoft.AspNetCore.Mvc;
using NotificationService.Data;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationDbContext _context;
        public NotificationController(NotificationDbContext context) => _context = context;

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Notifications.ToList());
    }
}
