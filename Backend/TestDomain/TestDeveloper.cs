using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestDomain
{
    [TestClass]
    public class TestDeveloper
    {
        private Developer developer;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            developer = new Developer()
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
            developer.Id = 1;
            int expected = 1;
            Assert.AreEqual(expected, developer.Id);
        }

        [TestMethod]
        public void NameGetSet()
        {
            developer.Name = "Ivan";
            string expected = "Ivan";
            Assert.AreEqual(expected, developer.Name);
        }

        [TestMethod]
        public void LastnameGetSet()
        {
            developer.Lastname = "Monjardin";
            string expected = "Monjardin";
            Assert.AreEqual(expected, developer.Lastname);
        }

        [TestMethod]
        public void UsernameGetSet()
        {
            developer.Username = "ivom";
            string expected = "ivom";
            Assert.AreEqual(expected, developer.Username);
        }

        [TestMethod]
        public void PasswordGetSet()
        {
            developer.Password = "myPasscode";
            string expected = "myPasscode";
            Assert.AreEqual(expected, developer.Password);
        }


        [TestMethod]
        public void ProjectsGet()
        {
            List<Project> expectedProjects = new List<Project> {
                new Project()
            {
                Name = "Project1",
            },
                new Project()
            {
                Name = "Project2",
            }
        };
            developer.Projects = expectedProjects;
            var actualProjects = developer.Projects;
            CollectionAssert.AreEqual(expectedProjects, actualProjects);
        }

        [TestMethod]
        public void ProjectsGetEmtpy()
        {
            var actualProjects = developer.Projects;
            Assert.IsTrue(actualProjects.Count == 0);
        }

        [TestMethod]
        public void CostGetSet()
        {
            developer.Cost = 4;
            int expected = 4;
            Assert.AreEqual(expected, developer.Cost);
        }
    }
}
