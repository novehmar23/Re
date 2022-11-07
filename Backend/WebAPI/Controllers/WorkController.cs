using BusinessLogicInterfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("works")]
    public class WorkController : ControllerBase
    {
        private readonly IWorkBusinessLogic businessLogic;

        public WorkController(IWorkBusinessLogic newbusinessLogic)
        {
            businessLogic = newbusinessLogic;
        }

        [AuthorizationFilter("Admin/Tester")]
        [HttpPost]
        public object Post([FromBody]WorkDTO workExpected)
        {
            return Ok(businessLogic.Add(workExpected));

        }

        [HttpGet("{id}")]
        public object Get([FromRoute] int id)
        {
            return Ok(businessLogic.GetById(id));
        }


        [HttpGet]
        public object GetAll([FromHeader]string token)
        {
            return Ok(businessLogic.GetAll(token));
        }
    }
}