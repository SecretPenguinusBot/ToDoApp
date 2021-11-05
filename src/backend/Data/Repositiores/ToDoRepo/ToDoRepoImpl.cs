
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Data.Repositories.ToDoRepoImpl
{
    public class ToDoRepoImplImpl
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ToDoRepoImplImpl> _logger;

        public ToDoRepoImplImpl(ApplicationDbContext db, ILogger<ToDoRepoImplImpl> logger)
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