using BusinessLogicInterfaces;
using CustomBugImportation;
using Domain.Utils;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("bugs")]
    public class BugController : ControllerBase
    {
        private readonly IBugBusinessLogic businessLogic;

        public BugController(IBugBusinessLogic newBusinessLogic)
        {
            businessLogic = newBusinessLogic;
        }

        [HttpGet]
        public IActionResult GetAll([FromHeader] string token)
        {
            return Ok(businessLogic.GetAll(token));
        }

        //[AuthorizationFilter("Admin/Tester")]
        [HttpPost]
        public object Post([FromBody] BugDTO bugExpected)
        {
            return Ok(businessLogic.Add(bugExpected));
        }

        [HttpGet("{id}")]
        public object Get([FromRoute] int id)
        {
            return Ok(businessLogic.GetById(id));
        }

        [AuthorizationFilter("Admin/Tester")]
        [HttpPut("{id}")]
        public object Update([FromRoute] int id, [FromBody] BugDTO bugModified)
        {
            return Ok(businessLogic.Update(id, bugModified));
        }

        [AuthorizationFilter("Admin/Tester")]
        [HttpDelete("{id}")]
        public object Delete([FromRoute] int id)
        {
            businessLogic.Delete(id);
            return NoContent();
        }


        [HttpPost("{id}/priority")]
        public object ClassifyPriority([FromRoute] int id, [FromBody] BugTypeDTO priority)
        {
            businessLogic.ClassifyPriority(id, priority);
            return Ok();
        }


        [HttpPost("{id}/severity")]
        public object ClassifySeverity([FromRoute] int id, [FromBody] BugTypeDTO severity)
        {
            businessLogic.ClassifySeverity(id, severity);
            return Ok();
        }


        [AuthorizationFilter("Admin")]
        [HttpPost("import/{format}")]
        public object ImportBugs([FromHeader] string path, [FromRoute] string format)
        {
            ImportCompany parsedFormat = (ImportCompany)Enum.Parse(typeof(ImportCompany), format, true);
            businessLogic.ImportBugs(path, parsedFormat);
            return Ok();
        }

        [AuthorizationFilter("Admin")]
        [HttpGet("custom-importers")]
        public object GetCustomImportersInfo()
        {
            List<ImporterInfo> info = businessLogic.GetCustomImportersInfo();
            return Ok(info);
        }

        [AuthorizationFilter("Developer")]
        [HttpPut("{id}/resolve")]
        public object ResolveBug([FromRoute] int id, [FromHeader] string token)
        {
            return Ok(businessLogic.ResolveBug(id, token));
        }

        [AuthorizationFilter("Developer")]
        [HttpPut("{id}/unresolve")]
        public object UnresolveBug([FromRoute] int id, [FromHeader] string token)
        {
            return Ok(businessLogic.UnresolveBug(id, token));
        }

        [AuthorizationFilter("Admin")]
        [HttpPost("custom-importers")]
        public object ImportBugsCustom([FromBody] ImporterInfo importerInfo)
        {
            businessLogic.ImportBugsCustom(importerInfo.ImporterName, importerInfo.Params);
            return Ok();
        }

    }
}
