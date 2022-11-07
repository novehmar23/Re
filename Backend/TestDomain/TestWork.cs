using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDomain
{
    [TestClass]
    public class TestWork
    {
        private Work work;
        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            work = new Work()
            {
                Name = "Work",
                Time = 4,
                Cost = 2,
                ProjectId = 1
            };
        }

        [TestMethod]
        public void IdGetSet()
        {
            work.Id = 1;
            int expected = 1;
            Assert.AreEqual(expected, work.Id);
        }

        [TestMethod]
        public void NameGetSet()
        {
            work.Name = "work1";
            string expected = "work1";
            Assert.AreEqual(expected, work.Name);
        }

        [TestMethod]
        public void CostGetSet()
        {
            work.Cost = 4;
            int expected = 4;
            Assert.AreEqual(expected, work.Cost);
        }

        [TestMethod]
        public void TimeGetSet()
        {
            work.Time = 3;
            int expected = 3;
            Assert.AreEqual(expected, work.Time);
        }

        [TestMethod]
        public void ProjectGet()
        {
            Project expectedProject = new Project
            {
                Name = "New Project"
            };
            work.Project = expectedProject;
            var actualProject = work.Project;
            Assert.AreEqual(expectedProject, actualProject);
        }

        [DataTestMethod]
        [DataRow("New project")]
        [DataRow("Second project")]
        [DataRow("The new new project")]
        public void ProjectNameSet(string name)
        {
            work.ProjectName = name;
            string expected = name;
            Assert.AreEqual(expected, work.ProjectName);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void ProjectIdGetSet(int id)
        {
            work.ProjectId = id;
            int expected = id;
            Assert.AreEqual(expected, work.ProjectId);
        }

        [TestMethod]
        public void IsEmptyName()
        {
            work.Name = null;
            Assert.ThrowsException<ValidationException>(() => work.Validate());
        }

        [TestMethod]
        public void IsEmptyCost()
        {
            work.Cost = 0;
            Assert.ThrowsException<ValidationException>(() => work.Validate());
        }

        [TestMethod]
        public void IsEmptyDuration()
        {
            work.Time = 0;
            Assert.ThrowsException<ValidationException>(() => work.Validate());
        }

        [TestMethod]
        public void IsValidAdmin()
        {
            try
            {
                work.Validate();
            }
            catch (ValidationException e)
            {
                Assert.Fail("Expected ValidationException but instead threw " + e.Message);
            }
        }
    }
}
