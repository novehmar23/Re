using BusinessLogic;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryInterfaces;
using System.Collections.Generic;

namespace TestAdminBusinessLogic
{
    [TestClass]
    public class TestAdminBusinessLogic
    {
        [TestMethod]
        public void CreateAdmin()
        {
            Admin admin = new Admin
            {
                Id = 1,
                Username = "admin",
                Name = "Pedro",
                Lastname = "Rodriguez",
                Password = "asdfsdf3242",
                Email = "pedrooo@hotmail.com"

            };
            var mock = new Mock<IAdminDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetAllUsernames()).Returns(new List<string>()); 
            mock.Setup(b => b.Create(admin)).Returns(admin);
            var adminBusinessLogic = new AdminBusinessLogic(mock.Object);

            var adminResult = adminBusinessLogic.Add(admin);
            mock.VerifyAll();

            Assert.AreEqual(adminResult, admin);
        }

        [TestMethod]
        public void VerifyRole()
        {
            string token = "asdfdasf";
            var mock = new Mock<IAdminDataAccess>(MockBehavior.Strict);
            mock.Setup(m => m.VerifyRole(token)).Returns(true);
            var adminBusinessLogic = new AdminBusinessLogic(mock.Object);

            var isRole = adminBusinessLogic.VerifyRole(token);
            mock.VerifyAll();

            Assert.IsTrue(isRole);
        }

        [TestMethod]
        public void VerifyRoleNotValid()
        {
            string token = "23423423";
            var mock = new Mock<IAdminDataAccess>(MockBehavior.Strict);
            mock.Setup(m => m.VerifyRole(token)).Returns(false);
            var adminBusinessLogic = new AdminBusinessLogic(mock.Object);

            var isRole = adminBusinessLogic.VerifyRole(token);
            mock.VerifyAll();

            Assert.IsFalse(isRole);
        }

        public void CreateInvalidAdmin()
        {
            Admin invalidAdmin = new Admin
            {
                Id = 1,
                Username = "admin",
                Name = "Pedro",
                Lastname = "Rodriguez",
                Email = "pedrooo@hotmail.com"

            };

            var mock = new Mock<IAdminDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Create(invalidAdmin)).Returns(invalidAdmin);
            var adminBusinessLogic = new AdminBusinessLogic(mock.Object);

            Assert.ThrowsException<ValidationException>(() => adminBusinessLogic.Add(invalidAdmin));
            mock.Verify(m => m.Create(invalidAdmin), Times.Never);

        }
    }
}
