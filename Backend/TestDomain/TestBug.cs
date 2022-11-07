using Domain;
using Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDomain
{
    [TestClass]
    public class TestBug
    {
        private Bug bug;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            Project project = new Project()
            {
                Name = "Project",
                Id = 3
            };

            bug = new Bug()
            {
                Name = "Bug1",
                Description = "Cuando el servidor se cierra y estoy en login se rompe",
                Version = "12.4.5",
                Time = 4,
                Project = project,
                ProjectId = 3,
                ProjectName = "Project"
            };
        }
        [TestMethod]
        public void NameGetSet()
        {
            bug.Name = "bug1";
            string expected = "bug1";
            Assert.AreEqual(expected, bug.Name);
        }

        [TestMethod]
        public void DescriptionGetSet()
        {
            bug.Description = "No se pudo hacer la conexion con el data access";
            string expected = "No se pudo hacer la conexion con el data access";
            Assert.AreEqual(expected, bug.Description);
        }

        [TestMethod]
        public void VersionGetSet()
        {
            bug.Version = "14.2.1";
            string expected = "14.2.1";
            Assert.AreEqual(expected, bug.Version);
        }


        [TestMethod]
        public void ProjectGet()
        {
            Project expectedProject = new Project
            {
                Name = "New Project"
            };
            bug.Project = expectedProject;
            var actualProject = bug.Project;
            Assert.AreEqual(expectedProject, actualProject);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void IdGetSet(int id)
        {
            bug.Id = id;
            int expected = id;
            Assert.AreEqual(expected, bug.Id);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void ProjectIdGetSet(int id)
        {
            bug.ProjectId = id;
            int expected = id;
            Assert.AreEqual(expected, bug.ProjectId);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void CompletedByIdIdGetSet(int id)
        {
            bug.CompletedById = id;
            int expected = id;
            Assert.AreEqual(expected, bug.CompletedById);
        }


        [DataTestMethod]
        [DataRow("New project")]
        [DataRow("Second project")]
        [DataRow("The new new project")]
        public void ProjectNameSet(string name)
        {
            bug.ProjectName = name;
            string expected = name;
            Assert.AreEqual(expected, bug.ProjectName);
        }

        [TestMethod]
        public void IsActive()
        {
            Assert.IsTrue(bug.IsActive);
        }

        [TestMethod]
        public void NullDeveloper()
        {
            Assert.IsNull(bug.CompletedBy);
        }

        [TestMethod]
        public void ComparerTrue()
        {
            Bug bug1 = new Bug()
            {

                Name = "Bug1",
                Description = "Cuando el servidor se cierra y estoy en login se rompe",
                Version = "12.4.5"
            };

            Bug bug2 = new Bug()
            {
                Name = "Bug1",
                Description = "Cuando el servidor se cierra y estoy en login se rompe",
                Version = "12.4.5"
            };

            Assert.AreEqual(0, new BugComparer().Compare(bug1, bug2));
        }

        [TestMethod]
        public void ComparerFalse()
        {
            Bug bug1 = new Bug()
            {

                Name = "Bug1",
                Description = "Cuando el servidor se cierra y estoy en login se rompe",
                Version = "12.4.5"
            };

            Bug bug2 = new Bug()
            {
                Name = "Bug2",
                Description = "IU bug",
                Version = "12.4.5"
            };

            Assert.AreNotEqual(0, new BugComparer().Compare(bug1, bug2));
        }

        [TestMethod]
        public void TimeGetSet()
        {
            bug.Time = 4;
            int expected = 4;
            Assert.AreEqual(expected, bug.Time);
        }

        [TestMethod]
        public void IsEmptyName()
        {
            bug.Name = null;
            Assert.ThrowsException<ValidationException>(() => bug.Validate());
        }

        [TestMethod]
        public void IsEmptyDescription()
        {
            bug.Description = null;
            Assert.ThrowsException<ValidationException>(() => bug.Validate());
        }

        [TestMethod]
        public void IsEmptyVersion()
        {
            bug.Version = null;
            Assert.ThrowsException<ValidationException>(() => bug.Validate());
        }

        [TestMethod]
        public void IsEmptyProject()
        {
            bug.Project = null;
            bug.ProjectId = 0;
            bug.ProjectName = null;
            Assert.ThrowsException<ValidationException>(() => bug.Validate());
        }

        [TestMethod]
        public void IsValidBug()
        {
            try
            {
                bug.Validate();
            }
            catch (ValidationException e)
            {
                Assert.Fail("Expected ValidationException but instead threw" + e.Message);
            }
        }
    }
}
