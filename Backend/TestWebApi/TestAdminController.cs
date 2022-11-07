using BusinessLogicInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;


namespace TestWebApi
{
    [TestClass]
    public class TestAdminController
    {

        [TestMethod]
        public void Create()
        {
            Admin adminExpected = new Admin()
            {
                Username = "admin",
                Name = "Juana",
                Lastname = "Perez",
                Password = "Juana1223#@",
                Email = "juana.perez@gmail.com"

            };

            var mock = new Mock<IAdminBusinessLogic>(MockBehavior.Strict);
            mock.Setup(a => a.Add(adminExpected)).Returns(adminExpected);
            var controller = new AdminController(mock.Object);

            var result = controller.Post(adminExpected);
            var okResult = result as OkObjectResult;
            var adminResult = okResult.Value as Admin;

            mock.VerifyAll();
            Assert.AreEqual(adminExpected, adminResult);
        }
    }
};




