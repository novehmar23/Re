using BusinessLogicInterfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBusinessLogic businessLogic;

        public LoginController(ILoginBusinessLogic newLoginBusinessLogic)
        {
            businessLogic = newLoginBusinessLogic;
        }

        [HttpPost]
        public object Login([FromBody] LoginDTO login)
        {
            return Ok(businessLogic.Login(login.Username, login.Password));
        }

    }
}