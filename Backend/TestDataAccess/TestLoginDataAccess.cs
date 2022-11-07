using Domain;
using Domain.Utils;
using DTO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System.Data.Common;
using System.Linq;

namespace TestDataAccess
{
    [TestClass]
    public class TestLoginDataAccess
    {

        private readonly DbConnection connection;
        private readonly LoginDataAccess loginDataAccess;
        private readonly BugManagerContext bugManagerContext;
        private readonly DbContextOptions<BugManagerContext> contextOptions;

        public TestLoginDataAccess()
        {
            connection = new SqliteConnection("Filename=:memory:");
            contextOptions = new DbContextOptionsBuilder<BugManagerContext>().UseSqlite(connection).Options;
            bugManagerContext = new BugManagerContext(contextOptions);
            loginDataAccess = new LoginDataAccess(bugManagerContext);
        }

        [TestInitialize]
        public void Setup()
        {
            connection.Open();
            bugManagerContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void CleanUp()
        {
            bugManagerContext.Database.EnsureDeleted();
        }

        [DataTestMethod]
        [DataRow("asdlfk", "jose")]
        [DataRow("as8df8asdf", "admin1")]
        [DataRow("3423k423j42k342m34", "devName")]
        public void SaveLogin(string expectedToken, string username)
        {
            LoginToken token = new LoginToken
            {
                Token = expectedToken,
                Username = username
            };

            loginDataAccess.SaveLogin(token);

            bool exists = bugManagerContext.Sessions.Any(s => s.Token == expectedToken
                                                        && s.Username == username);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void GetIdFromToken()
        {
            string token = "sdfg-uytr-fds-dsdf";
            string username = "jose";
            int id = 3;

            Admin admin = new Admin()
            {
                Username = username,
                Name = "ivan",
                Email = "dfgh@fghj.com",
                Id = id,
                Lastname = "dfgh",
                Password = "122334"
            };

            LoginToken loginToken = new LoginToken
            {
                Token = token,
                Username = username
            };

            bugManagerContext.Add(admin);
            loginDataAccess.SaveLogin(loginToken);

            TokenIdDTO tokenExpected = loginDataAccess.GetIdRoleFromToken(token);

            Assert.AreEqual(tokenExpected.Id, id);
        }

        [TestMethod]
        public void GetRoleFromToken()
        {
            string token = "sdfg-uytr-fds-dsdf";
            string username = "jose";
            int id = 3;

            Admin admin = new Admin()
            {
                Username = username,
                Name = "ivan",
                Email = "dfgh@fghj.com",
                Id = id,
                Lastname = "dfgh",
                Password = "122334"
            };

            LoginToken loginToken = new LoginToken
            {
                Token = token,
                Username = username
            };

            bugManagerContext.Add(admin);
            loginDataAccess.SaveLogin(loginToken);

            TokenIdDTO tokenExpected = loginDataAccess.GetIdRoleFromToken(token);

            Assert.AreEqual(tokenExpected.Role, Roles.Admin);
        }
    }
}
