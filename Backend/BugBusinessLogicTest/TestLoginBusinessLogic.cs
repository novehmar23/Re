using BusinessLogic;
using Domain;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryInterfaces;
using System.Security.Authentication;

namespace TestLoginBusinessLogic
{
    [TestClass]
    public class TestLoginBusinessLogic
    {

        [DataTestMethod]
        [DataRow("admin", "Juana1223#@")]
        [DataRow("dev12", "devvvv")]
        [DataRow("Pedro", "testqetrty")]
        public void LoginTokenNotEmpty(string username, string password)
        {
            string role = "admin";
            var mock = new Mock<ILoginDataAccess>(MockBehavior.Strict);

            mock.Setup(l => l.VerifyUser(username, password)).Returns(role);
            mock.Setup(l => l.SaveLogin(It.IsAny<LoginToken>()));
            var loginBusinessLogic = new LoginBusinessLogic(mock.Object);

            LoginResponseDTO token = loginBusinessLogic.Login(username, password);
            mock.VerifyAll();
            Assert.IsTrue(token.Token.Length > 0);
        }

        [DataTestMethod]
        [DataRow("admin", "Juana1223#@")]
        [DataRow("dev12", "devvvv")]
        [DataRow("Pedro", "testqetrty")]
        public void LoginTokenDifferentEachLogin(string username, string password)
        {
            string role = "admin";
            var mock = new Mock<ILoginDataAccess>(MockBehavior.Strict);

            mock.Setup(l => l.VerifyUser(username, password)).Returns(role);
            mock.Setup(l => l.SaveLogin(It.IsAny<LoginToken>()));
            var loginBusinessLogic = new LoginBusinessLogic(mock.Object);

            LoginResponseDTO first = loginBusinessLogic.Login(username, password);
            LoginResponseDTO second = loginBusinessLogic.Login(username, password);
            mock.VerifyAll();
            Assert.IsTrue(first.Token != second.Token);
        }

        [TestMethod]
        public void LoginTokenDifferentEachLoginDifferentAccounts()
        {
            string role = "admin";
            string role2 = "dev";
            var mock = new Mock<ILoginDataAccess>(MockBehavior.Strict);

            mock.Setup(l => l.VerifyUser("admin", "Juana1223#@")).Returns(role);
            mock.Setup(l => l.VerifyUser("dev12", "devvvv")).Returns(role2);
            mock.Setup(l => l.SaveLogin(It.IsAny<LoginToken>()));
            var loginBusinessLogic = new LoginBusinessLogic(mock.Object);

            LoginResponseDTO first = loginBusinessLogic.Login("admin", "Juana1223#@");
            LoginResponseDTO second = loginBusinessLogic.Login("dev12", "devvvv");
            mock.VerifyAll();
            Assert.IsTrue(first.Token != second.Token);
        }

        [TestMethod]
        public void LoginNotVerify()
        {
            string roleNotFound = null;
            var mock = new Mock<ILoginDataAccess>(MockBehavior.Strict);

            mock.Setup(l => l.VerifyUser("admin", "Juana1223#@")).Returns(roleNotFound);
            var loginBusinessLogic = new LoginBusinessLogic(mock.Object);

            Assert.ThrowsException<AuthenticationException>(() => loginBusinessLogic.Login("admin", "Juana1223#@"));
        }


        [TestMethod]
        public void ReturnedTokenSameAsSaved()
        {
            string role = "admin";
            var mock = new Mock<ILoginDataAccess>(MockBehavior.Strict);
            mock.Setup(l => l.VerifyUser("admin", "Juana1223#@")).Returns(role);
            LoginToken saved = null;
            mock.Setup(l => l.SaveLogin(It.IsAny<LoginToken>())).Callback<LoginToken>((t) => { saved = t; });
            var loginBusinessLogic = new LoginBusinessLogic(mock.Object);

            LoginResponseDTO returned = loginBusinessLogic.Login("admin", "Juana1223#@");

            Assert.AreEqual(saved.Token, returned.Token);
        }


        [DataTestMethod]
        [DataRow("admin", "Juana1223#@")]
        [DataRow("dev12", "devvvv")]
        [DataRow("Pedro", "testqetrty")]
        public void LoginTokenHasRole(string username, string password)
        {
            string role = "dev";
            var mock = new Mock<ILoginDataAccess>(MockBehavior.Strict);

            mock.Setup(l => l.VerifyUser(username, password)).Returns(role);
            mock.Setup(l => l.SaveLogin(It.IsAny<LoginToken>()));
            var loginBusinessLogic = new LoginBusinessLogic(mock.Object);

            LoginResponseDTO token = loginBusinessLogic.Login(username, password);
            mock.VerifyAll();
            Assert.IsTrue(token.Role == role);
        }

        [TestMethod]
        public void GetIdFromToken()
        {
            string token = "sdfg-uytr-fds-dsdf";
            string role = "dev";
            int id = 4;
            var mock = new Mock<ILoginDataAccess>(MockBehavior.Strict);

            mock.Setup(l => l.GetIdRoleFromToken(token)).Returns(new TokenIdDTO() { Id = id, Role = role });
            var loginBusinessLogic = new LoginBusinessLogic(mock.Object);

            TokenIdDTO expected = loginBusinessLogic.GetIdRoleFromToken(token);
            mock.VerifyAll();
            Assert.AreEqual(expected.Id, id);
        }

        public void GetRoleFromToken()
        {
            string role = "dev";
            string token = "asdfghj-dfgh-rwq-fds";
            int id = 4;

            var mock = new Mock<ILoginDataAccess>(MockBehavior.Strict);
            mock.Setup(l => l.GetIdRoleFromToken(token)).Returns(new TokenIdDTO() { Id = id, Role = role });
            var loginBusinessLogic = new LoginBusinessLogic(mock.Object);

            TokenIdDTO tokenExpected = loginBusinessLogic.GetIdRoleFromToken(token);
            mock.VerifyAll();
            Assert.AreEqual(tokenExpected.Role, role);
        }
    }
}
