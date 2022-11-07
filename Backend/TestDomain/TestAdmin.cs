using Domain;
using Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDomain
{
    [TestClass]
    public class TestAdmin
    {
        private Admin admin;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            admin = new Admin()
            {
                Id = 0,
                Username = "agustinadisiot",
                Name = "Agustina",
                Lastname = "Disiot",
                Password = "thisIsNotActuallyMyPassword",
                Email = "agus@email.com",
            };
        }

        [TestMethod]
        public void IdGetSet()
        {
            admin.Id = 1;
            int expected = 1;
            Assert.AreEqual(expected, admin.Id);
        }

        [TestMethod]
        public void NameGetSet()
        {
            admin.Name = "Ivan";
            string expected = "Ivan";
            Assert.AreEqual(expected, admin.Name);
        }

        [TestMethod]
        public void LastnameGetSet()
        {
            admin.Lastname = "Monjardin";
            string expected = "Monjardin";
            Assert.AreEqual(expected, admin.Lastname);
        }

        [TestMethod]
        public void UsernameGetSet()
        {
            admin.Username = "ivom";
            string expected = "ivom";
            Assert.AreEqual(expected, admin.Username);
        }

        [TestMethod]
        public void PasswordGetSet()
        {
            admin.Password = "myPasscode";
            string expected = "myPasscode";
            Assert.AreEqual(expected, admin.Password);
        }

        [TestMethod]
        public void ComparerTrue()
        {
            Admin admin1 = new Admin()
            {
                Id = 0,
                Username = "agustinadisiot",
                Name = "Agustina",
                Lastname = "Disiot",
                Password = "thisIsNotActuallyMyPassword",
                Email = "agus@email.com",
            };

            Admin admin2 = new Admin()
            {
                Id = 0,
                Username = "agustinadisiot",
                Name = "Agustina",
                Lastname = "Disiot",
                Password = "thisIsNotActuallyMyPassword",
                Email = "agus@email.com",
            };

            Assert.AreEqual(0, new UserComparer().Compare(admin1, admin2));
        }

        [TestMethod]
        public void ComparerFalse()
        {
            Admin admin1 = new Admin()
            {
                Id = 0,
                Username = "agustinadisiot",
                Name = "Agustina",
                Lastname = "Disiot",
                Password = "thisIsNotActuallyMyPassword",
                Email = "agus@email.com",
            };

            Admin admin2 = new Admin()
            {
                Id = 0,
                Username = "agustinadisiot",
                Name = "Juan",
                Lastname = "Disiot",
                Password = "thisIsNotActuallyMyPassword2",
                Email = "juan@email.com",
            };

            Assert.AreNotEqual(0, new UserComparer().Compare(admin1, admin2));
        }
    }
}
