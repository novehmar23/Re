using BusinessLogic;
using BusinessLogicInterfaces;
using Domain;
using Domain.Utils;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace TestProjectBusinessLogic
{
    [TestClass]
    public class TestProjectBusinessLogic
    {
        private List<Project> projects;
        private Project project;

        private List<Tester> testers;
        private List<Developer> developers;
        private List<Bug> bugs;


        [TestCleanup]
        public void TearDown()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            project = new Project()
            {
                Id = 0,
                Name = "Web API"
            };

            projects = new List<Project>()
            {
                project
            };

            bugs = new List<Bug>();
            testers = new List<Tester>();
            developers = new List<Developer>();

        }


        [TestMethod]
        public void CreateProject()
        {
            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Create(project)).Returns(project);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var projectResult = projectBusinessLogic.Add(new ProjectDTO(project));
            mock.VerifyAll();

            Assert.AreEqual(projectResult, new ProjectDTO(project));
        }

        [TestMethod]
        public void DeleteProjectByNameNotFound()
        {
            string nameProjectToDelete = "Empty project";

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.DeleteByName(nameProjectToDelete)).Throws(new NonexistentProjectException());
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            Assert.ThrowsException<NonexistentProjectException>(() => projectBusinessLogic.DeleteByName(nameProjectToDelete));
        }

        [TestMethod]
        public void DeleteProjectByName()
        {
            string nameProjectToDelete = "project1";

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.DeleteByName(nameProjectToDelete)).Returns(new ResponseMessage("Deleted successfully"));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.DeleteByName(nameProjectToDelete);
            mock.VerifyAll();
            Assert.IsTrue(result is ResponseMessage);
        }

        [TestMethod]
        public void DeleteProjectByIdNotFound()
        {
            int idProjectToDelete = 1;

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Delete(idProjectToDelete)).Throws(new NonexistentProjectException());
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            Assert.ThrowsException<NonexistentProjectException>(() => projectBusinessLogic.Delete(idProjectToDelete));
        }

        [TestMethod]
        public void DeleteProjectById()
        {
            int idProjectToDelete = 0;
            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Delete(idProjectToDelete)).Returns(new ResponseMessage("Deleted successfully"));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.Delete(idProjectToDelete);
            mock.VerifyAll();
            Assert.IsTrue(result is ResponseMessage);
        }

        [TestMethod]
        public void GetByNameNotFound()
        {
            Project projectExpected = new Project()
            {
                Name = "Project",
                Id = 0
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetByName(projectExpected.Name)).Throws(new NonexistentProjectException());
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            Assert.ThrowsException<NonexistentProjectException>(() => projectBusinessLogic.GetByName(projectExpected.Name));
        }

        [TestMethod]
        public void GetByName()
        {
            Project projectExpected = new Project()
            {
                Name = "Project",
                Id = 0
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetByName(projectExpected.Name)).Returns(projectExpected);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.GetByName(projectExpected.Name);

            Assert.AreEqual(new ProjectDTO(projectExpected), result);
        }


        [TestMethod]
        public void GetByIdNotFound()
        {
            Project projectExpected = new Project()
            {
                Name = "Project",
                Id = 0
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetById(projectExpected.Id)).Throws(new NonexistentProjectException());
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            Assert.ThrowsException<NonexistentProjectException>(() => projectBusinessLogic.GetById(projectExpected.Id));
        }

        [TestMethod]
        public void GetById()
        {
            Project projectExpected = new Project()
            {
                Name = "Project",
                Id = 0
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetById(projectExpected.Id)).Returns(projectExpected);
            mock.Setup(b => b.GetProjectCost(It.IsAny<int>())).Returns(new ProjectCost(0));
            mock.Setup(b => b.GetProjectDuration(It.IsAny<int>())).Returns(new ProjectDuration(0));
            mock.Setup(b => b.GetBugsQuantity(It.IsAny<int>())).Returns(new BugsQuantity(0));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.GetById(projectExpected.Id);

            Assert.AreEqual(new ProjectDTO(projectExpected), result);
        }

        [TestMethod]
        public void GetAllProjects()
        {
            List<ProjectDTO> projectsExpected = new List<ProjectDTO>()
            {
                new ProjectDTO()
                {
                     Name = "projecOne",
                     Id = 0
                },
                new ProjectDTO()
                {
                    Name = "projectDos",
                    Id = 1
                },
                 new ProjectDTO()
                {
                    Name = "ProjectTrois",
                    Id = 2
                },
            };

            string token = "dfgh-fgh-fds";

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetAll(token)).Returns(projectsExpected.ConvertAll(p => p.ConvertToDomain()));
            mock.Setup(b => b.GetProjectCost(It.IsAny<int>())).Returns(new ProjectCost(0));
            mock.Setup(b => b.GetProjectDuration(It.IsAny<int>())).Returns(new ProjectDuration(0));
            mock.Setup(b => b.GetBugsQuantity(It.IsAny<int>())).Returns(new BugsQuantity(0));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.GetAll(token);

            Assert.IsTrue(projectsExpected.SequenceEqual(result));
        }

        [TestMethod]
        public void UpdateProjectByNameNotFound()
        {
            string nameProjectToUpdate = "Nonexistent Project";

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.UpdateByName(nameProjectToUpdate, project)).Throws(new NonexistentProjectException());
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            Assert.ThrowsException<NonexistentProjectException>(() => projectBusinessLogic.UpdateByName(nameProjectToUpdate, new ProjectDTO(project)));
        }

        [TestMethod]
        public void UpdateProjectByName()
        {
            string nameProjectToUpdate = "Web API";
            Project projectModified = new Project()
            {
                Id = 0,
                Name = "projectMod"
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.UpdateByName(nameProjectToUpdate, projectModified)).Returns(projectModified);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var projectResult = projectBusinessLogic.UpdateByName(nameProjectToUpdate, new ProjectDTO(projectModified));

            mock.VerifyAll();

            Assert.AreEqual(projectResult, new ProjectDTO(projectModified));
        }

        [TestMethod]
        public void UpdateProjectByIdNotFound()
        {
            int idprojectToUpdate = 1;

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Update(idprojectToUpdate, project)).Throws(new NonexistentProjectException());
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            Assert.ThrowsException<NonexistentProjectException>(() => projectBusinessLogic.Update(idprojectToUpdate, new ProjectDTO(project)));
        }

        [TestMethod]
        public void UpdateProjectById()
        {
            int idprojectToUpdate = 0;

            Project projectModified = new Project()
            {
                Id = 0,
                Name = "projectMod"
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Update(idprojectToUpdate, projectModified)).Returns(projectModified);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var projectResult = projectBusinessLogic.Update(idprojectToUpdate, new ProjectDTO(projectModified));

            mock.VerifyAll();

            Assert.AreEqual(projectResult, new ProjectDTO(projectModified));
        }

        [TestMethod]
        public void GetBugs()
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                     Name = "Not working button",
                     Description = "Upload button not working",
                     Version = "1",
                     IsActive = true,
                     CompletedBy = null,
                     Id = 0,
                     CompletedById = 6,
                     ProjectId = 1,
                    Project = new Project()
                    {
                    Id = 1,
                    Name = "project",
                    },
                },
                new Bug()
                {
                    Name = "button",
                    Description = "Upload not working",
                    Version = "1.4.5",
                    IsActive = false,
                    CompletedBy = null,
                    Id = 1,
                      CompletedById = 6,
                     ProjectId = 1,
                    Project = new Project()
                    {
                    Id = 1,
                    Name = "project",
                    },
                },
                 new Bug()
                {
                    Name = "Not working button",
                    Description = "Upload button not working",
                    Version = "6.2",
                    IsActive = true,
                    CompletedBy = null,
                    Id = 1,
                      CompletedById = 0,
                     ProjectId = 2,
                    Project = new Project()
                    {
                    Id = 1,
                    Name = "project",
                    },
                },
            };


            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetBugs(1)).Returns(bugsExpected);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.GetBugs(1);
            mock.VerifyAll();
            Assert.IsTrue(bugsExpected.ConvertAll(b => new BugDTO(b)).SequenceEqual(result));
        }

        [TestMethod]
        public void GetBugsQuantity()
        {
            Project project = new Project()
            {
                Name = "project",
                Id = 1,
                Bugs = new List<Bug>()
                {
                    new Bug()
                     {
                     Name = "Not working button",
                     Description = "Upload button not working",
                     Version = "1",
                     IsActive = true,
                     CompletedBy = null
                    },
                    new Bug()
                    {
                    Name = "button",
                    Description = "Upload not working",
                    Version = "1.4.5",
                    IsActive = false,
                    CompletedBy = null
                    },
                    new Bug()
                    {
                    Name = "Not working button",
                    Description = "Upload button not working",
                    Version = "6.2",
                    IsActive = true,
                    CompletedBy = null
                     }
                }
            };

            int cant = project.Bugs.Count();

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetBugsQuantity(project.Id)).Returns(new BugsQuantity(cant));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.GetBugsQuantity(project.Id);

            mock.VerifyAll();
            Assert.AreEqual(cant, result.Quantity);
        }

        [TestMethod]
        public void GetDevelopers()
        {
            List<Developer> devsExpected = new List<Developer>()
            {
                new Developer()
                {
                     Username = "Nicolas"
                },
                new Developer()
                {
                    Username = "Ivan"
                },
                 new Developer()
                {
                    Username = "Agus"
                },
            };


            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetDevelopers(1)).Returns(devsExpected);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.GetDevelopers(1);
            mock.VerifyAll();
            Assert.IsTrue(devsExpected.ConvertAll(b => new DeveloperDTO(b)).SequenceEqual(result));
        }
        [TestMethod]
        public void GetTesters()
        {
            List<Tester> testersExpected = new List<Tester>()
            {
                new Tester()
                {
                     Username = "Nicolas"
                },
                new Tester()
                {
                    Username = "Ivan"
                },
                 new Tester()
                {
                    Username = "Agus"
                },
            };


            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetTesters(1)).Returns(testersExpected);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var result = projectBusinessLogic.GetTesters(1);
            mock.VerifyAll();
            Assert.IsTrue(testersExpected.ConvertAll(b => new TesterDTO(b)).SequenceEqual(result));
        }


        [TestMethod]
        public void AddDeveloperToProject()
        {
            Project project = new Project()
            {
                Name = "project3",
                Id = 1,
            };

            Developer devExpected = new Developer()
            {
                Id = 2,
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com",
                Cost = 4
            };


            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.AddDeveloperToProject(project.Id, devExpected.Id)).Returns(devExpected);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);


            var result = projectBusinessLogic.AddDeveloperToProject(project.Id, devExpected.Id);

            mock.VerifyAll();
            Assert.AreEqual(new DeveloperDTO(devExpected), result);
        }

        [TestMethod]
        public void AddTesterToProject()
        {
            Project project = new Project()
            {
                Name = "project3",
                Id = 1,
            };

            Tester testerExpected = new Tester()
            {
                Id = 2,
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com",
                Cost = 3
            };


            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.AddTesterToProject(project.Id, testerExpected.Id)).Returns(testerExpected);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);


            var result = projectBusinessLogic.AddTesterToProject(project.Id, testerExpected.Id);

            mock.VerifyAll();
            Assert.AreEqual(new TesterDTO(testerExpected), result);
        }

        [TestMethod]
        public void DeleteDeveloperFromProject()
        {
            Project project = new Project()
            {
                Id = 1,
            };

            Developer dev = new Developer()
            {
                Id = 2,
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com"
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.RemoveDeveloperFromProject(project.Id, dev.Id)).Returns(new ResponseMessage("Deleted from project"));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);


            var result = projectBusinessLogic.RemoveDeveloperFromProject(project.Id, dev.Id);

            mock.VerifyAll();
            Assert.IsTrue(result is ResponseMessage);
        }

        [TestMethod]
        public void DeleteTesterFromProject()
        {
            Project project = new Project()
            {
                Id = 1,
            };

            Tester tester = new Tester()
            {
                Id = 2,
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com"
            };
            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.RemoveTesterFromProject(project.Id, tester.Id)).Returns(new ResponseMessage("Deleted from project"));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);


            var result = projectBusinessLogic.RemoveTesterFromProject(project.Id, tester.Id);

            mock.VerifyAll();
            Assert.IsTrue(result is ResponseMessage);
        }

        [TestMethod]
        public void GetProjectCost()
        {
            int expectedCost = 6;

            Project project = new Project()
            {
                Id = 0,
                Name = "projectMod"
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetProjectCost(project.Id)).Returns(new ProjectCost(expectedCost));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var projectResult = projectBusinessLogic.GetProjectCost(project.Id);

            mock.VerifyAll();

            Assert.AreEqual(projectResult.Cost, expectedCost);
        }

        [TestMethod]
        public void GetProjectDuration()
        {
            int expectedDuration = 6;

            Project project = new Project()
            {
                Id = 0,
                Name = "projectMod"
            };

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetProjectDuration(project.Id)).Returns(new ProjectDuration(expectedDuration));
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            var projectResult = projectBusinessLogic.GetProjectDuration(project.Id);

            mock.VerifyAll();

            Assert.AreEqual(projectResult.Duration, expectedDuration);
        }

        [TestMethod]
        public void CreateInvalidProject()
        {
            Project invalidProject = new Project();

            var mock = new Mock<IProjectDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Create(invalidProject)).Returns(invalidProject);
            var projectBusinessLogic = new ProjectBusinessLogic(mock.Object);

            Assert.ThrowsException<ValidationException>(() => projectBusinessLogic.Add(new ProjectDTO(invalidProject)));
            mock.Verify(m => m.Create(invalidProject), Times.Never);

        }
    }
}
