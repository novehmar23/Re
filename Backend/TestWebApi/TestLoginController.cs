using BusinessLogicInterfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;


namespace TestWebApi
{
    [TestClass]
    public class TestLoginController
    {

        [DataTestMethod]
        [DataRow("admin", "Juana1223#@", "adsfasdfasdfasdf", "dev")]
        [DataRow("dev12", "devvvv", "3hjg2jh34234", "admin")]
        [DataRow("Pedro", "testqetrty", "zxcmvnwn1312m312,3", "tester")]
        public void Login(string username, string password, string guid, string role)
        {

            var mock = new Mock<ILoginBusinessLogic>(MockBehavior.Strict);
            mock.Setup(l => l.Login(username, password)).Returns(new LoginResponseDTO { Token = guid, Role = role });
            var controller = new LoginController(mock.Object);


            var result = controller.Login(new LoginDTO
            {
                Username = username,
                Password = password
            });
            var okResult = result as OkObjectResult;
            var loginResult = okResult.Value as LoginResponseDTO;

            mock.VerifyAll();
            Assert.AreEqual(loginResult.Token, guid);
        }
    }
};




