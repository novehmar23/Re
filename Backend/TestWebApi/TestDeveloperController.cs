using BusinessLogicInterfaces;
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
    public class TestDeveloperController
    {

        [TestMethod]
        public void Create()
        {
            DeveloperDTO devExpected = new DeveloperDTO()
            {
                Username = "juana",
                Name = "Juana",
                Lastname = "López",
                Password = "Juana1223#@",
                Email = "juana.perez@gmail.com"

            };

            var mock = new Mock<IDeveloperBusinessLogic>(MockBehavior.Strict);
            mock.Setup(d => d.Add(devExpected)).Returns(devExpected);
            var controller = new DeveloperController(mock.Object);

            var result = controller.Post(devExpected);
            var okResult = result as OkObjectResult;
            var devResult = okResult.Value as DeveloperDTO;

            mock.VerifyAll();
            Assert.AreEqual(devExpected, devResult);
        }

        [TestMethod]
        public void QuantityBugsResolved()
        {
            int idDev = 1;
            int cantBugsResolved = 1;

            var mock = new Mock<IDeveloperBusinessLogic>(MockBehavior.Strict);
            mock.Setup(d => d.GetQuantityBugsResolved(idDev)).Returns(cantBugsResolved);
            var controller = new DeveloperController(mock.Object);

            var result = controller.GetQuantityBugsResolved(idDev);
            var okResult = result as OkObjectResult;
            var cantResult = okResult.Value as BugsQuantity;

            mock.VerifyAll();
            Assert.AreEqual(cantBugsResolved, cantResult.Quantity);
        }

        [TestMethod]
        public void QuantityBugsResolvedDevNotFound()
        {
            int idDev = 1;

            var mock = new Mock<IDeveloperBusinessLogic>(MockBehavior.Strict);
            mock.Setup(d => d.GetQuantityBugsResolved(idDev)).Throws(new NonexistentUserException());
            var controller = new DeveloperController(mock.Object);

            Assert.ThrowsException<NonexistentUserException>(() => controller.GetQuantityBugsResolved(idDev));
        }

        [TestMethod]
        public void GetAll()
        {
            List<DeveloperDTO> developersExpected = new List<DeveloperDTO>()
            {
                new DeveloperDTO(){
                    Username = "juana",
                Name = "Juana",
                Lastname = "López",
                Password = "Juana1223#@",
                Email = "juana.perez@gmail.com",
                BugsResolved = 2
                },
                new DeveloperDTO(){
                Username = "juana",
                Name = "Juana",
                Lastname = "López",
                Password = "Juana1223#@",
                Email = "juana.perez@gmail.com",
                BugsResolved = 1
                }
            };


            var mock = new Mock<IDeveloperBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetAllDevs()).Returns(developersExpected);
            var controller = new DeveloperController(mock.Object);

            var result = controller.GetAllDevs();
            var okResult = result as OkObjectResult;
            var devsResult = okResult.Value as IEnumerable<DeveloperDTO>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(developersExpected, (System.Collections.ICollection)devsResult, new UserComparer());
        }
    }
};




