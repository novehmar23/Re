using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TestDomain
{
    [TestClass]
    public class TestProject
    {
        private Project project;
        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            project = new Project()
            {
                Name = "Project",
            };
        }
        [TestMethod]
        public void NameGetSet()
        {
            project.Name = "Project1";
            string expected = "Project1";
            Assert.AreEqual(expected, project.Name);
        }

        [TestMethod]
        public void GetTestersEmpty()
        {
            List<Tester> expectedTesters = new List<Tester>();
            Assert.IsTrue(expectedTesters.SequenceEqual(project.Testers));
        }

        [TestMethod]
        public void GetSetTesters()
        {
            Tester tester = new Tester();
            List<Tester> expectedTesters = new List<Tester>();
            expectedTesters.Add(tester);
            project.Testers = expectedTesters;
            Assert.IsTrue(expectedTesters.SequenceEqual(project.Testers));
        }

        [TestMethod]
        public void GetDevelopersEmpty()
        {
            List<Developer> expectedDeveloper = new List<Developer>();
            Assert.IsTrue(expectedDeveloper.SequenceEqual(project.Developers));
        }

        [TestMethod]
        public void GetSetDevelopers()
        {
            Developer dev = new Developer();
            List<Developer> expectedDeveloper = new List<Developer>();
            expectedDeveloper.Add(dev);
            project.Developers = expectedDeveloper;
            Assert.IsTrue(expectedDeveloper.SequenceEqual(project.Developers));
        }

        [TestMethod]
        public void GetBugsEmpty()
        {
            List<Bug> expectedBugs = new List<Bug>();
            Assert.IsTrue(expectedBugs.SequenceEqual(project.Bugs));
        }

        [TestMethod]
        public void GetSetBugs()
        {
            Bug bug = new Bug();
            List<Bug> expectedBugs = new List<Bug>();
            expectedBugs.Add(bug);
            project.Bugs = expectedBugs;
            Assert.IsTrue(expectedBugs.SequenceEqual(project.Bugs));
        }

        [TestMethod]
        public void GetWorksEmpty()
        {
            List<Work> expectedWorks = new List<Work>();
            Assert.IsTrue(expectedWorks.SequenceEqual(project.Works));
        }

        [TestMethod]
        public void GetSetWorks()
        {
            Work work = new Work();
            List<Work> expectedWorks = new List<Work>();
            expectedWorks.Add(work);
            project.Works = expectedWorks;
            Assert.IsTrue(expectedWorks.SequenceEqual(project.Works));
        }

        [TestMethod]
        public void IsEmptyName()
        {
            project.Name = null;
            Assert.ThrowsException<ValidationException>(() => project.Validate());
        }

        [TestMethod]
        public void IsEmptyBugs()
        {
            project.Bugs = null;
            Assert.ThrowsException<ValidationException>(() => project.Validate());
        }

        [TestMethod]
        public void IsEmptyTesters()
        {
            project.Testers = null;
            Assert.ThrowsException<ValidationException>(() => project.Validate());
        }

        [TestMethod]
        public void IsEmptyDevelopers()
        {
            project.Developers = null;
            Assert.ThrowsException<ValidationException>(() => project.Validate());
        }

        [TestMethod]
        public void IsEmptyWorks()
        {
            project.Works = null;
            Assert.ThrowsException<ValidationException>(() => project.Validate());
        }

        [TestMethod]
        public void IsValidBug()
        {
            try
            {
                project.Validate();
            }
            catch (ValidationException e)
            {
                Assert.Fail("Expected ValidationException but instead threw" + e.Message);
            }
        }
    }
}
