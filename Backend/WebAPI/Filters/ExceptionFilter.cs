using BusinessLogicInterfaces;
using CustomBugImportation;
using CustomBugImporter;
using Domain;
using Domain.Utils;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Security.Authentication;

namespace WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode;

            string errorMesssage = context.Exception.Message;

            if (context.Exception is NonexistentBugException)
            {
                statusCode = 404;
            }
            else if (context.Exception is NonexistentProjectException)
            {
                statusCode = 404;
            }
            else if (context.Exception is NonexistentUserException)
            {
                statusCode = 404;
            }
            else if (context.Exception is CustomImporterException)
            {
                statusCode = 400;
            }
            else if (context.Exception is AuthenticationException)
            {
                statusCode = 401;
                errorMesssage = "Incorrect password or username";
            }
            else if (context.Exception is DirectoryNotFoundException)
            {
                statusCode = 500;
            }
            else if (context.Exception is ImporterManagerException)
            {
                statusCode = 500;
            }
            else if (context.Exception is ValidationException)
            {
                statusCode = 400;
            }
            else
            {
                statusCode = 500;
            }

            ResponseMessage message = new ResponseMessage(errorMesssage);
            context.Result = new ObjectResult(message)
            {
                StatusCode = statusCode,
            };
        }
    }
}