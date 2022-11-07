using BusinessLogicInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("admins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusinessLogic businessLogic;

        public AdminController(IAdminBusinessLogic newAdminBusinessLogic)
        {
            businessLogic = newAdminBusinessLogic;
        }

        [AuthorizationFilter("Admin")]
        [HttpPost]
        public object Post([FromBody] Admin adminExpected)
        {
            return Ok(businessLogic.Add(adminExpected));
        }

    }
}