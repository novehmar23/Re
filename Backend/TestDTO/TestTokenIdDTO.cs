using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDTO
{
    [TestClass]
    public class TestTokenIdDTO
    {
        private TokenIdDTO tokenDTO;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            tokenDTO = new TokenIdDTO()
            {
                Id = 5,
                Role = "admin"
            };
        }
        [TestMethod]
        public void TokenGetSet()
        {
            tokenDTO.Id = 5;
            int expected = 5;
            Assert.AreEqual(expected, tokenDTO.Id);
        }

        [TestMethod]
        public void RoleGetSet()
        {
            tokenDTO.Role = "dev";
            string expected = "dev";
            Assert.AreEqual(expected, tokenDTO.Role);
        }
    }
}
