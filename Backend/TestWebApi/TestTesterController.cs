using BusinessLogicInterfaces;
using Domain.Utils;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;


namespace TestWebApi
{
    [TestClass]
    public class TestTesterController
    {

        [TestMethod]
        public void Create()
        {
            TesterDTO testerExpected = new TesterDTO()
            {
                Username = "jose",
                Name = "Joselito",
                Lastname = "López",
                Password = "josephe3#@",
                Email = "jose.lopez@gmail.com"

            };

            var mock = new Mock<ITesterBusinessLogic>(MockBehavior.Strict);
            mock.Setup(t => t.Add(testerExpected)).Returns(testerExpected);
            var controller = new TesterController(mock.Object);

            var result = controller.Post(testerExpected);
            var okResult = result as OkObjectResult;
            var devResult = okResult.Value as TesterDTO;

            mock.VerifyAll();
            Assert.AreEqual(testerExpected, devResult);
        }

        [TestMethod]
        public void FilterBugsByStatus()
        {
            List<BugDTO> bugsExpected = new List<BugDTO>()
            {
                new BugDTO()
                {
                    Name = "bug1",
                    IsActive = true
                },
                new BugDTO()
                {
                    Name = "bug2",
                    IsActive = true
                },
            };
            int idTester = 1;
            var mock = new Mock<ITesterBusinessLogic>(MockBehavior.Strict);
            mock.Setup(t => t.GetBugsByStatus(idTester, true)).Returns(bugsExpected);
            var controller = new TesterController(mock.Object);

            var result = controller.GetBugsByStatus(idTester, true);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugDTO>;

            mock.VerifyAll();
            Assert.IsTrue(bugsExpected.SequenceEqual(bugsResult));
        }

        [TestMethod]
        public void FilterBugsByName()
        {
            List<BugDTO> bugsExpected = new List<BugDTO>()
            {
                new BugDTO()
                {
                    Name = "bug1",
                },
                new BugDTO()
                {
                    Name = "bug1",
                },
            };
            int idTester = 1;
            var mock = new Mock<ITesterBusinessLogic>(MockBehavior.Strict);
            mock.Setup(t => t.GetBugsByName(idTester, "bug1")).Returns(bugsExpected);
            var controller = new TesterController(mock.Object);

            var result = controller.GetBugsByName(idTester, "bug1");
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugDTO>;

            mock.VerifyAll();
            Assert.IsTrue(bugsExpected.SequenceEqual(bugsResult));
        }

        [TestMethod]
        public void FilterBugsByProject()
        {
            List<BugDTO> bugsExpected = new List<BugDTO>()
            {
                new BugDTO()
                {
                    ProjectId = 3,
                },
                new BugDTO()
                {
                    ProjectId = 3,
                },
            };
            int idTester = 1;
            var mock = new Mock<ITesterBusinessLogic>(MockBehavior.Strict);
            mock.Setup(t => t.GetBugsByProject(idTester, 3)).Returns(bugsExpected);
            var controller = new TesterController(mock.Object);

            var result = controller.GetBugsByProject(idTester, 3);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugDTO>;

            mock.VerifyAll();
            Assert.IsTrue(bugsExpected.SequenceEqual(bugsResult));
        }

        [TestMethod]
        public void GetAll()
        {
            List<TesterDTO> testersExpected = new List<TesterDTO>()
            {
                new TesterDTO(){
                    Username = "juana",
                Name = "Juana",
                Lastname = "López",
                Password = "Juana1223#@",
                Email = "juana.perez@gmail.com",
                },
                new TesterDTO(){
                Username = "juana",
                Name = "Juana",
                Lastname = "López",
                Password = "Juana1223#@",
                Email = "juana.perez@gmail.com"
                }
            };


            var mock = new Mock<ITesterBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetAllTesters()).Returns(testersExpected);
            var controller = new TesterController(mock.Object);

            var result = controller.GetAllTesters();
            var okResult = result as OkObjectResult;
            var testersResult = okResult.Value as IEnumerable<TesterDTO>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(testersExpected, (System.Collections.ICollection)testersResult, new UserComparer());
        }
    }
};




