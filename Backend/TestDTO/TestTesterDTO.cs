using Domain;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestDTO
{
    [TestClass]
    public class TestTesterDTO
    {
        private TesterDTO testerDTO;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            testerDTO = new TesterDTO()
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
            testerDTO.Id = 1;
            int expected = 1;
            Assert.AreEqual(expected, testerDTO.Id);
        }

        [TestMethod]
        public void NameGetSet()
        {
            testerDTO.Name = "Ivan";
            string expected = "Ivan";
            Assert.AreEqual(expected, testerDTO.Name);
        }

        [TestMethod]
        public void LastnameGetSet()
        {
            testerDTO.Lastname = "Monjardin";
            string expected = "Monjardin";
            Assert.AreEqual(expected, testerDTO.Lastname);
        }

        [TestMethod]
        public void UsernameGetSet()
        {
            testerDTO.Username = "ivom";
            string expected = "ivom";
            Assert.AreEqual(expected, testerDTO.Username);
        }

        [TestMethod]
        public void PasswordGetSet()
        {
            testerDTO.Password = "myPasscode";
            string expected = "myPasscode";
            Assert.AreEqual(expected, testerDTO.Password);
        }

        [TestMethod]
        public void ProjectsGet()
        {
            List<ProjectDTO> expectedProjects = new List<ProjectDTO> {
                new ProjectDTO()
            {
                Name = "Project1",
            },
                new ProjectDTO()
            {
                Name = "Project2",
            }
        };
            testerDTO.ProjectsDTO = expectedProjects;
            var actualProjects = testerDTO.ProjectsDTO;
            CollectionAssert.AreEqual(expectedProjects, actualProjects);
        }

        [TestMethod]
        public void CostGetSet()
        {
            testerDTO.Cost = 4;
            int expected = 4;
            Assert.AreEqual(expected, testerDTO.Cost);
        }

        [TestMethod]
        public void FromTesterToDTO()
        {
            Tester tester = new Tester()
            {
                Name = "agus",
                Lastname = "perez",
                Id = 3,
                Username = "perezsoy",
                Password = "sdfgh",
                Email = "xcvbnrty@cvbnm",
                Cost = 6,
            };

            TesterDTO testerDTO = new TesterDTO(tester);

            Assert.AreEqual(tester.Id, testerDTO.Id);
        }

        [TestMethod]
        public void FromDTOtoTester()
        {
            TesterDTO testerDTO = new TesterDTO()
            {
                Name = "agus",
                Lastname = "perez",
                Id = 3,
                Username = "perezsoy",
                Password = "sdfgh",
                Email = "xcvbnrty@cvbnm",
                Cost = 6,
            };

            Tester tester = testerDTO.ConvertToDomain();

            Assert.AreEqual(tester.Id, testerDTO.Id);
        }
    }
}

