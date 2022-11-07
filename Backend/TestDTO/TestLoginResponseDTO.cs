using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDTO
{
    [TestClass]
    public class TestLoginResponseDTO
    {
        private LoginResponseDTO loginDTO;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            loginDTO = new LoginResponseDTO()
            {
                Token = "b4975af6-28b4-4a0c-b643-30c7dde2384b",
                Role = "admin"
            };
        }
        [TestMethod]
        public void TokenGetSet()
        {
            loginDTO.Token = "aab3c0d4-3c65-49ec-a253-662d32415441";
            string expected = "aab3c0d4-3c65-49ec-a253-662d32415441";
            Assert.AreEqual(expected, loginDTO.Token);
        }

        [TestMethod]
        public void RoleGetSet()
        {
            loginDTO.Role = "dev";
            string expected = "dev";
            Assert.AreEqual(expected, loginDTO.Role);
        }
    }
}
