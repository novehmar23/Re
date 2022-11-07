using Domain;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestDTO
{
    [TestClass]
    public class TestDeveloperDTO
    {
        private DeveloperDTO developerDTO;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            developerDTO = new DeveloperDTO()
            {
                Id = 1,
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
            developerDTO.Id = 1;
            int expected = 1;
            Assert.AreEqual(expected, developerDTO.Id);
        }

        [TestMethod]
        public void NameGetSet()
        {
            developerDTO.Name = "Ivan";
            string expected = "Ivan";
            Assert.AreEqual(expected, developerDTO.Name);
        }

        [TestMethod]
        public void LastnameGetSet()
        {
            developerDTO.Lastname = "Monjardin";
            string expected = "Monjardin";
            Assert.AreEqual(expected, developerDTO.Lastname);
        }

        [TestMethod]
        public void UsernameGetSet()
        {
            developerDTO.Username = "ivom";
            string expected = "ivom";
            Assert.AreEqual(expected, developerDTO.Username);
        }

        [TestMethod]
        public void PasswordGetSet()
        {
            developerDTO.Password = "myPasscode";
            string expected = "myPasscode";
            Assert.AreEqual(expected, developerDTO.Password);
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
            developerDTO.ProjectsDTO = expectedProjects;
            var actualProjects = developerDTO.ProjectsDTO;
            CollectionAssert.AreEqual(expectedProjects, actualProjects);
        }


        [TestMethod]
        public void CostGetSet()
        {
            developerDTO.Cost = 4;
            int expected = 4;
            Assert.AreEqual(expected, developerDTO.Cost);
        }

        [TestMethod]
        public void BugsResolvedGetSet()
        {
            developerDTO.BugsResolved = 4;
            int expected = 4;
            Assert.AreEqual(expected, developerDTO.BugsResolved);
        }


        [TestMethod]
        public void FromDevToDTO()
        {
            Developer dev = new Developer()
            {
                Name = "agus",
                Lastname = "perez",
                Id = 3,
                Username = "perezsoy",
                Password = "sdfgh",
                Email = "xcvbnrty@cvbnm",
                Cost = 6,
            };

            DeveloperDTO devDTO = new DeveloperDTO(dev);

            Assert.AreEqual(dev.Id, devDTO.Id);
        }

        [TestMethod]
        public void FromDTOtoDev()
        {
            DeveloperDTO devDTO = new DeveloperDTO()
            {
                Name = "agus",
                Lastname = "perez",
                Id = 3,
                Username = "perezsoy",
                Password = "sdfgh",
                Email = "xcvbnrty@cvbnm",
                Cost = 6,
            };

            Developer dev = devDTO.ConvertToDomain();

            Assert.AreEqual(dev.Id, devDTO.Id);
        }
    }
}
