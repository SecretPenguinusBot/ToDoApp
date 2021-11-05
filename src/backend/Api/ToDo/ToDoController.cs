
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.ToDo
{
    [ApiController,Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices]ApplicationDbContext db) => Ok(db.ToDoItems);
    }
}