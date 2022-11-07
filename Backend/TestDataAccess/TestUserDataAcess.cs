using Domain;
using Domain.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using RepositoryInterfaces;
using System.Data.Common;

namespace TestDataAccess
{
    [TestClass]
    public abstract class TestUserDataAccess<T> where T : User
    {
        protected IUserDataAccess<T> userDataAccess { get; set; }
        protected DbSet<T> users;
        protected T user;
        protected string role;
        protected User userDifferentRole;
        protected readonly DbConnection connection;
        protected readonly LoginDataAccess loginDataAccess;
        protected readonly BugManagerContext bugManagerContext;
        protected readonly DbContextOptions<BugManagerContext> contextOptions;

        public TestUserDataAccess()
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

        [TestMethod]
        public void VerifyValidCredentials()
        {
            user.Username = "userPedro";
            user.Name = "Pedro";
            user.Lastname = "López";
            user.Password = "fransico234";
            user.Email = "pedrooo2@hotmail.com";

            bugManagerContext.Add(user);
            bugManagerContext.SaveChanges();

            string expectedRole = loginDataAccess.VerifyUser("userPedro", "fransico234");

            Assert.AreEqual(role, expectedRole);
        }


        [TestMethod]
        public void VerifyNotValidCredentials()
        {
            user.Username = "userPedro";
            user.Name = "Pedro";
            user.Lastname = "Jorge";
            user.Password = "fransico234";
            user.Email = "pedrooo2@hotmail.com";

            bugManagerContext.Add(user);
            bugManagerContext.SaveChanges();

            string expectedRole = loginDataAccess.VerifyUser("userPedro", "jose454");

            Assert.AreNotEqual(role, expectedRole);
        }

        [TestMethod]
        public void VerifyNonExistingUser()
        {
            string expectedRole = loginDataAccess.VerifyUser("administradorPedro", "contraseñaIncorrecta");

            Assert.AreNotEqual(expectedRole, role);
        }


        [TestMethod]
        public void Create()
        {
            user.Username = "userPedro";
            user.Name = "Pedro";
            user.Lastname = "Jorge";
            user.Password = "fransico234";
            user.Email = "pedrooo2@hotmail.com";

            User userSaved = userDataAccess.Create(user);
            Assert.AreEqual(0, new UserComparer().Compare(user, userSaved));
        }

        [TestMethod]
        public void CreateNull()
        {
            Assert.ThrowsException<NonexistentUserException>(() => userDataAccess.Create(null));
        }

        [TestMethod]
        public void VerifyRoleValid()
        {

            user.Username = "userPedro";
            user.Name = "Pedro";
            user.Lastname = "Jorge";
            user.Password = "fransico234";
            user.Email = "pedrooo2@hotmail.com";

            User userSaved = userDataAccess.Create(user);

            LoginToken token = new LoginToken
            {
                Token = "asdfasdfa34234",
                Username = "userPedro"
            };

            loginDataAccess.SaveLogin(token);
            bool isRole = userDataAccess.VerifyRole("asdfasdfa34234");
            Assert.IsTrue(isRole);
        }


        [TestMethod]
        public void VerifyRoleInvalid()
        {

            userDifferentRole.Username = "juana";
            userDifferentRole.Name = "Juana";
            userDifferentRole.Lastname = "Perez";
            userDifferentRole.Password = "123qwerty";
            userDifferentRole.Email = "Perez@gmail.com";

            bugManagerContext.Add(userDifferentRole);
            bugManagerContext.SaveChanges();

            LoginToken token = new LoginToken
            {
                Token = "234234wfadsf",
                Username = "juana"
            };

            loginDataAccess.SaveLogin(token);
            bool isRole = userDataAccess.VerifyRole("234234wfadsf");
            Assert.IsFalse(isRole);
        }
    }
}
