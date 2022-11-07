using BusinessLogicInterfaces;
using Domain;
using Domain.Utils;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApi.Controllers;

namespace TestWebApi
{
    [TestClass]
    public class TestWorkController
    {
        [TestMethod]
        public void Create()
        {
            WorkDTO workExpected = new WorkDTO()
            {
                Name = "implement menu",
                Cost = 2,
                Time = 4,
                Id = 3,
                ProjectId = 2
            };

            var mock = new Mock<IWorkBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.Add(workExpected)).Returns(workExpected);
            var controller = new WorkController(mock.Object);

            var result = controller.Post(workExpected);
            var okResult = result as OkObjectResult;
            var workResult = okResult.Value as WorkDTO;

            mock.VerifyAll();
            Assert.AreEqual(workExpected, workResult);
        }

        [TestMethod]
        public void GetWork()
        {
            Work workExpected = new Work()
            {
                Name = "Login and create user",
                Cost = 2,
                Time = 4,
                Id = 3
            };

            var mock = new Mock<IWorkBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetById(workExpected.Id)).Returns(workExpected);
            var controller = new WorkController(mock.Object);

            var result = controller.Get(workExpected.Id);
            var okResult = result as OkObjectResult;
            var workResult = okResult.Value as Work;

            mock.VerifyAll();
            Assert.AreEqual(workExpected, workResult);
        }

        [TestMethod]
        public void GetAll()
        {
            List<WorkDTO> worksExpected = new List<WorkDTO>()
            {
                new WorkDTO(){
                   Name = "Do frontend",
                   Cost = 3,
                   Id = 2,
                   ProjectId = 3,
                   Time = 6
                },
                new WorkDTO(){
                 Name = "Do frontend",
                   Cost = 3,
                   Id = 2,
                   ProjectId = 3,
                   Time = 6
                }
            };

            string token = "sdfgh-fghjf";

            var mock = new Mock<IWorkBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetAll(token)).Returns(worksExpected);
            var controller = new WorkController(mock.Object);

            var result = controller.GetAll(token);
            var okResult = result as OkObjectResult;
            var workResult = okResult.Value as IEnumerable<WorkDTO>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(worksExpected, (System.Collections.ICollection)workResult, new WorkComparer());
        }
    }
}
