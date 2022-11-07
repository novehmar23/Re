using BusinessLogicInterfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectBusinessLogic businessLogic;

        public ProjectController(IProjectBusinessLogic newProjectBusinessLogic)
        {
            businessLogic = newProjectBusinessLogic;
        }

        [AuthorizationFilter("Admin/Tester/Developer")]
        [HttpGet]
        public IActionResult GetAll([FromHeader] string token)
        {
            return Ok(businessLogic.GetAll(token));
        }

        [AuthorizationFilter("Admin")]
        [HttpPost]
        public object Post([FromBody] ProjectDTO projectExpected)
        {
            return Ok(businessLogic.Add(projectExpected));
        }

        [HttpGet("{id}")]
        public object Get([FromRoute] int id)
        {
            return Ok(businessLogic.GetById(id));
        }

        [HttpGet("name/{name}")]
        public object GetByName([FromRoute] string name)
        {
            return Ok(businessLogic.GetByName(name));
        }
        [AuthorizationFilter("Admin")]
        [HttpPut("{id}")]
        public object Update([FromRoute] int id, [FromBody] ProjectDTO projectModified)
        {
            return Ok(businessLogic.Update(id, projectModified));
        }

        [AuthorizationFilter("Admin")]
        [HttpDelete("{id}")]
        public object Delete([FromRoute] int id)
        {
            businessLogic.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}/bugs")]
        public object GetBugs([FromRoute] int id)
        {
            return Ok(businessLogic.GetBugs(id));
        }

        [AuthorizationFilter("Admin")]
        [HttpGet("{id}/bugs/quantity")]
        public object GetBugsQuantity([FromRoute] int id)
        {
            return Ok(businessLogic.GetBugsQuantity(id));
        }

        [AuthorizationFilter("Admin/Tester")]
        [HttpGet("{id}/devs")]
        public object GetDevelopers([FromRoute] int id)
        {
            return Ok(businessLogic.GetDevelopers(id));
        }

        [AuthorizationFilter("Admin")]
        [HttpGet("{id}/testers")]
        public object GetTesters([FromRoute] int id)
        {
            return Ok(businessLogic.GetTesters(id));
        }

        [AuthorizationFilter("Admin")]
        [HttpDelete("{idProject}/devs/{idDev}")]
        public object RemoveDeveloperFromProject(int idProject, int idDev)
        {
            return Ok(businessLogic.RemoveDeveloperFromProject(idProject, idDev));
        }

        [AuthorizationFilter("Admin")]
        [HttpDelete("{idProject}/testers/{idTester}")]
        public object RemoveTesterFromProject(int idProject, int idTester)
        {
            return Ok(businessLogic.RemoveTesterFromProject(idProject, idTester));
        }

        [AuthorizationFilter("Admin")]
        [HttpPost("{idProject}/devs/{idDev}")]
        public object AddDeveloperToProject(int idProject, int idDev)
        {
            return Ok(businessLogic.AddDeveloperToProject(idProject, idDev));
        }

        [AuthorizationFilter("Admin")]
        [HttpPost("{idProject}/testers/{idTester}")]
        public object AddTesterToProject(int idProject, int idTester)
        {
            return Ok(businessLogic.AddTesterToProject(idProject, idTester));
        }

        [AuthorizationFilter("Admin")]
        [HttpGet("{id}/cost")]
        public object GetProjectCost([FromRoute] int id)
        {
            return Ok(businessLogic.GetProjectCost(id));
        }

        [AuthorizationFilter("Admin")]
        [HttpGet("{id}/duration")]
        public object GetProjectDuration([FromRoute] int id)
        {
            return Ok(businessLogic.GetProjectDuration(id));
        }
    }
}