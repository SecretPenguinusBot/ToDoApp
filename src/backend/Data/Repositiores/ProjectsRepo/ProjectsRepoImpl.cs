
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Data.Repositories.ProjectsRepo
{
    public sealed class ProjectsRepoImpl
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ProjectsRepoImpl> _logger;

        public ProjectsRepoImpl(ApplicationDbContext db, ILogger<ProjectsRepoImpl> logger) => (_db, _logger) = (db, logger);

        public async Task<IRepositoryActionResult> GetAllProjects(CancellationToken cancellationToken = default)
        {
            try{
                var result = await _db.Projects
                    .Include(_ => _.ToDoItems)
                    .ToArrayAsync(cancellationToken);
                return new OkResultWithData<IEnumerable<Project>>
                {
                    Data = result
                };
            }
            catch(Exception ex) { _logger.LogError(ex.ToString()); }
            return new ServerErrorResult
            {
                Error = "Cant get projects"
            };
        }

        public async Task<IRepositoryActionResult> GetProjectById(int id, CancellationToken cancellationToken = default)
        {
            try{
                var result = await _db.Projects
                    .Include(_ => _.ToDoItems)
                    .FirstOrDefaultAsync( x => x.Id == id);
                if(result is null)
                {
                    return NotFoundResult.Instance;
                }
                return new OkResultWithData<Project>
                {
                    Data = result
                };
            }
            catch(Exception ex) { _logger.LogError(ex.ToString()); }
            return new ServerErrorResult
            {
                Error = "Can't get project"
            };
        }

        public async Task<IRepositoryActionResult> AddProject(Project project)
        {
            try
            {
                var result = await _db.Projects.AddAsync(project);
                await _db.SaveChangesAsync();
                return new OkResultWithData<Project>
                {
                    Data = project
                };
            }
            catch(Exception ex) { _logger.LogError(ex.ToString()); }
            return new ServerErrorResult
            {
                Error = "Can't add project"
            }; 
        }

        public async Task<IRepositoryActionResult> UpdateProject(Project project)
        {
            try
            {
                _db.Entry(project).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return new OkResultWithData<Project>
                {
                    Data = project
                };
            }
            catch(DbUpdateException)
            {
                var dbItem = await _db.Projects.FirstOrDefaultAsync(x => x.Id == project.Id);
                if(dbItem is null)
                {
                    return NotFoundResult.Instance;
                }
            }
            catch(Exception ex) { _logger.LogError(ex.ToString()); }
            return new ServerErrorResult
            {
                Error = "Can't add project"
            }; 
        }
        public async Task<IRepositoryActionResult> DeleteProjectById(int id)
        {
            try
            {
                var item = await _db.Projects.SingleOrDefaultAsync(x => x.Id == id);
                if(item is null)
                {
                    return NotFoundResult.Instance;
                }
                _db.Projects.Remove(item);
                await _db.SaveChangesAsync();
                return OkResult.Instance;
            }
            catch(Exception ex) { _logger.LogError(ex.ToString()); }
            return new ServerErrorResult
            {
                Error = "Can't delete project"
            }; 
        } 
    }
}