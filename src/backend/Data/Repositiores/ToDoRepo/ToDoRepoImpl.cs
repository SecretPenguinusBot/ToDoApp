
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Data.Repositories.ToDoRepo
{
    public class ToDoRepo
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ToDoRepo> _logger;

        public ToDoRepo(ApplicationDbContext db, ILogger<ToDoRepo> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IRepositoryActionResult> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogTrace("Get all ToDoItems start");
            var result = await _db.ToDoItems.ToArrayAsync();
            _logger.LogTrace("Get all ToDoItems complete");
            
            return new OkResultWithData<IEnumerable<ToDoItem>>
            {
                Data = result
            };
        }
    }
}