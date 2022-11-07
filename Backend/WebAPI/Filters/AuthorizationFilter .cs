using BusinessLogicInterfaces;
using Domain.Utils;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApi.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private string arg;

        public AuthorizationFilter(string arguments)
        {
            arg = arguments;

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorize = false;
            string token = context.HttpContext.Request.Headers["token"];
            if (token == null)
            {
                NotLoggedRespond(context);
                return;
            }

            var loginLogic = context.HttpContext.RequestServices.GetService<ILoginBusinessLogic>();
            TokenIdDTO tokenIdDTO = loginLogic.GetIdRoleFromToken(token);
            context.HttpContext.Items.Add("role", tokenIdDTO.Role);
            context.HttpContext.Items.Add("userId", tokenIdDTO.Id);

            if (arg.Contains("Admin"))
                isAuthorize = isAuthorize || tokenIdDTO.Role == Roles.Admin;

            if (arg.Contains("Developer"))
                isAuthorize = isAuthorize || tokenIdDTO.Role == Roles.Dev;

            if (arg.Contains("Tester"))
                isAuthorize = isAuthorize || tokenIdDTO.Role == Roles.Tester;

            if (!isAuthorize)
            {
                ResponseMessage message = new ResponseMessage("You aren't logged correctly.");
                context.Result = new ObjectResult(message)
                {
                    StatusCode = 403,
                };
            }

        }

        private void NotLoggedRespond(AuthorizationFilterContext context)
        {
            ResponseMessage message = new ResponseMessage("You aren't logged.");
            context.Result = new ObjectResult(message)
            {
                StatusCode = 401,
            };
        }
    }
};
