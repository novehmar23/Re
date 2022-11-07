using BusinessLogicInterfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("testers")]
    public class TesterController : ControllerBase
    {
        private readonly ITesterBusinessLogic businessLogic;

        public TesterController(ITesterBusinessLogic newTesterBusinessLogic)
        {
            businessLogic = newTesterBusinessLogic;
        }

        [AuthorizationFilter("Admin")]
        [HttpPost]
        public object Post([FromBody] TesterDTO testerExpected)
        {
            return Ok(businessLogic.Add(testerExpected));
        }

        [AuthorizationFilter("Tester")]
        [HttpGet("{idTester}/bugs/status/{filter}")]
        public object GetBugsByStatus([FromRoute] int idTester, [FromRoute] bool filter)
        {
            return Ok(businessLogic.GetBugsByStatus(idTester, filter));
        }
        [AuthorizationFilter("Tester")]
        [HttpGet("{idTester}/bugs/name/{filter}")]
        public object GetBugsByName([FromRoute] int idTester, [FromRoute] string filter)
        {
            return Ok(businessLogic.GetBugsByName(idTester, filter));

        }
        [AuthorizationFilter("Tester")]
        [HttpGet("{idTester}/bugs/project/{filter}")]
        public object GetBugsByProject([FromRoute] int idTester, [FromRoute] int filter)
        {
            return Ok(businessLogic.GetBugsByProject(idTester, filter));

        }

        [AuthorizationFilter("Admin")]
        [HttpGet]
        public object GetAllTesters()
        {
            return Ok(businessLogic.GetAllTesters());
        }
    }
}