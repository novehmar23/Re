using BusinessLogicInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WebApi.Filters;

namespace TestWebApi
{
    [TestClass]
    public class TestAuthorizationFilter
    {
        // Hacer los tests requeria muchos mocks, incluyendo la extension de ServiceProvider que no se termino
        //https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions?view=dotnet-plat-ext-5.0
        //[TestMethod] TODO
        public void AdminAuthorization()
        {
            // SETUP
            string token = "asdfa";
            var logicMock = new Mock<IAdminBusinessLogic>(MockBehavior.Strict);
            logicMock.Setup(m => m.VerifyRole(token)).Returns(false);

            var requestServicesMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            requestServicesMock.Setup(m => m.GetService<IAdminBusinessLogic>()).Returns(logicMock.Object);

            // Source guide: https://programmium.wordpress.com/2020/04/30/unit-testing-custom-authorization-filter-in-net-core/
            var httpContextMock = new Mock<HttpContext>(MockBehavior.Loose);
            httpContextMock.Setup(m => m.Request.Headers["token"]).Returns(token);
            httpContextMock.Setup(m => m.RequestServices).Returns(requestServicesMock.Object);
            ActionContext fakeActionContext = new ActionContext(httpContextMock.Object,
                                             new Microsoft.AspNetCore.Routing.RouteData(),
                                             new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            AuthorizationFilterContext fakeAuthFilterContext =
            new AuthorizationFilterContext(fakeActionContext,
              new List<IFilterMetadata> { });

            var filter = new AuthorizationFilter("Admin");
            // ACT 
            filter.OnAuthorization(fakeAuthFilterContext);
            // VERIFY
            var result = (ContentResult)fakeAuthFilterContext.Result;
            int? code = result.StatusCode;
            Assert.AreEqual(403, code);
        }
    }
}
