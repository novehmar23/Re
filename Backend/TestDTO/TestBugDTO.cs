using Domain;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDTO
{
    [TestClass]
    public class TestBugDTO
    {
        private BugDTO bugDTO;

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            bugDTO = new BugDTO()
            {
                Name = "Bug1",
                Description = "Cuando el servidor se cierra y estoy en login se rompe",
                Version = "12.4.5",
                Time = 4,
                ProjectId = 3,
                ProjectName = "Project"
            };
        }
        [TestMethod]
        public void NameGetSet()
        {
            bugDTO.Name = "bug1";
            string expected = "bug1";
            Assert.AreEqual(expected, bugDTO.Name);
        }

        [TestMethod]
        public void DescriptionGetSet()
        {
            bugDTO.Description = "No se pudo hacer la conexion con el data access";
            string expected = "No se pudo hacer la conexion con el data access";
            Assert.AreEqual(expected, bugDTO.Description);
        }

        [TestMethod]
        public void VersionGetSet()
        {
            bugDTO.Version = "14.2.1";
            string expected = "14.2.1";
            Assert.AreEqual(expected, bugDTO.Version);
        }


        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void IdGetSet(int id)
        {
            bugDTO.Id = id;
            int expected = id;
            Assert.AreEqual(expected, bugDTO.Id);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void ProjectIdGetSet(int id)
        {
            bugDTO.ProjectId = id;
            int expected = id;
            Assert.AreEqual(expected, bugDTO.ProjectId);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void CompletedByIdIdGetSet(int id)
        {
            bugDTO.CompletedById = id;
            int expected = id;
            Assert.AreEqual(expected, bugDTO.CompletedById);
        }


        [TestMethod]
        public void IsActive()
        {
            Assert.IsTrue(bugDTO.IsActive);
        }


        [TestMethod]
        public void TimeGetSet()
        {
            bugDTO.Time = 4;
            int expected = 4;
            Assert.AreEqual(expected, bugDTO.Time);
        }

        [TestMethod]
        public void CompletedByNameGetSet()
        {
            bugDTO.CompletedByUsername = "Agus";
            string expected = "Agus";
            Assert.AreEqual(expected, bugDTO.CompletedByUsername);
        }

        [TestMethod]
        public void FromBugToDTO()
        {
            Bug bug = new Bug()
            {
                Name = "bug",
                Description = "button not work",
                Version = "4.5",
                Time = 4,
                ProjectId = 2,
                CompletedById = 6,
                CompletedBy = new Developer()
                {
                    Username = "ivan",
                    Name = "ivan",
                    Cost = 5,
                    Email = "fghj",
                    Id = 6,
                    Lastname = "perez",
                    Password = "234567clave",
                },
                Id = 3,
                Project = new Project()
                {
                    Id = 2,
                    Name = "project",
                },
            };

            BugDTO bugDTO = new BugDTO(bug);

            Assert.AreEqual(bug.Id, bugDTO.Id);
        }

        [TestMethod]
        public void FromDTOtoBug()
        {
            BugDTO bugDTO = new BugDTO()
            {
                Name = "bug",
                Description = "button not work",
                Version = "4.5",
                Time = 4,
                ProjectId = 2,
                CompletedById = 6,
                Id = 3
            };

            Bug bug = bugDTO.ConvertToDomain();

            Assert.AreEqual(bug.Id, bugDTO.Id);
        }
    }
}
