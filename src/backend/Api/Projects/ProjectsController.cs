
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using backend.Data.Entities;
using backend.Data.Repositories;
using backend.Data.Repositories.ProjectsRepo;
using Microsoft.AspNetCore.Mvc;

namespace backend.Api.Projects
{
    [ApiController, Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsRepoImpl _repo;
        public ProjectsController(ProjectsRepoImpl repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await _repo.GetAllProjects(cancellationToken);
            return result switch
            {
                IOkResultWithData<IEnumerable<Project>> ok => Ok(ok.Data),
                _ => StatusCode(500)
            };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var result = await _repo.GetProjectById(id, cancellationToken);
            return result switch
            {
                IOkResultWithData<Project> ok => Ok(ok.Data),
                INotFoundResult => NotFound(),
                _ => StatusCode(500)
            };
        }

        [HttpPost]
        public async Task<IActionResult> Add(Project project, CancellationToken cancellationToken = default)
        {
            if(project.Id != 0)
            {
                return BadRequest();
            }

            var result = await _repo.AddProject(project);
            return result switch
            {
                IOkResultWithData<Project> ok => CreatedAtAction(nameof(GetById), new {id = ok.Data.Id}, ok.Data),
                _ => StatusCode(500)
            };
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Project project, CancellationToken cancellationToken = default)
        {
            if(id != project.Id)
            {
                return BadRequest("Same id's required");
            }

            var result = await _repo.UpdateProject(project);
            return result switch
            {
                IOkResultWithData<Project> ok => Ok(ok.Data),
                INotFoundResult => NotFound(),
                _ => StatusCode(500)
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = await _repo.DeleteProjectById(id);
            return result switch
            {
                IOkResult => NoContent(),
                INotFoundResult => NotFound(),
                _ => StatusCode(500)
            };
        }
    }
}