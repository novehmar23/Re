using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDomain
{
    [TestClass]
    public class TestUser
    {
        private User user;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            user = new User()
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
            user.Id = 1;
            int expected = 1;
            Assert.AreEqual(expected, user.Id);
        }

        [TestMethod]
        public void NameGetSet()
        {
            user.Name = "Ivan";
            string expected = "Ivan";
            Assert.AreEqual(expected, user.Name);
        }

        [TestMethod]
        public void LastnameGetSet()
        {
            user.Lastname = "Monjardin";
            string expected = "Monjardin";
            Assert.AreEqual(expected, user.Lastname);
        }

        [TestMethod]
        public void UsernameGetSet()
        {
            user.Username = "ivom";
            string expected = "ivom";
            Assert.AreEqual(expected, user.Username);
        }

        [TestMethod]
        public void PasswordGetSet()
        {
            user.Password = "myPasscode";
            string expected = "myPasscode";
            Assert.AreEqual(expected, user.Password);
        }

        [TestMethod]
        public void IsEmptyUsername()
        {
            user.Username = null;
            Assert.ThrowsException<ValidationException>(() => user.Validate());
        }

        [TestMethod]
        public void IsEmptyName()
        {
            user.Name = null;
            Assert.ThrowsException<ValidationException>(() => user.Validate());
        }

        [TestMethod]
        public void IsEmptyLastname()
        {
            user.Lastname = null;
            Assert.ThrowsException<ValidationException>(() => user.Validate());
        }

        [TestMethod]
        public void IsEmptyPassword()
        {
            user.Password = null;
            Assert.ThrowsException<ValidationException>(() => user.Validate());
        }

        [TestMethod]
        public void IsEmptyEmail()
        {
            user.Email = null;
            Assert.ThrowsException<ValidationException>(() => user.Validate());
        }

        [TestMethod]
        public void IsValidAdmin()
        {
            try
            {
                user.Validate();
            }
            catch (ValidationException e)
            {
                Assert.Fail("Expected ValidationException but instead threw" + e.Message);
            }
        }
    }
}
