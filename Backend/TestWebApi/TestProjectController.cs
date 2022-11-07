using BusinessLogicInterfaces;
using Domain;
using Domain.Utils;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;

namespace TestWebApi
{
    [TestClass]
    public class TestProjectController
    {
        [TestMethod]
        public void GetAll()
        {
            List<ProjectDTO> projectsExpected = new List<ProjectDTO>()
            {
                new ProjectDTO(){
                    Name = "Project1",
                    Id = 0,
                    TotalCost = 43,
                    TotalDuration = 78,
                    BugsQuantity = 6
                },
                new ProjectDTO(){
                Name = "Project2",
                Id = 1,
                TotalCost = 43,
                    TotalDuration = 78,
                    BugsQuantity = 6
                }
            };

            string token = "sdfgh-fghjf";


            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetAll(token)).Returns(projectsExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.GetAll(token);
            var okResult = result as OkObjectResult;
            var projectsResult = okResult.Value as IEnumerable<ProjectDTO>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(projectsExpected, (System.Collections.ICollection)projectsResult, new ProjectComparer());
        }

        [TestMethod]
        public void Create()
        {
            ProjectDTO projectExpected = new ProjectDTO()
            {
                Name = "Project",
                Id = 0
            };

            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.Add(projectExpected)).Returns(projectExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.Post(projectExpected);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectDTO;

            mock.VerifyAll();
            Assert.AreEqual(projectExpected, projectResult);
        }

        [TestMethod]
        public void GetProject()
        {
            ProjectDTO projectExpected = new ProjectDTO()
            {
                Name = "Project1",
                Id = 0,
                TotalCost = 43,
                TotalDuration = 78,
                BugsQuantity = 6
            };

            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetById(projectExpected.Id)).Returns(projectExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.Get(projectExpected.Id);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectDTO;

            mock.VerifyAll();
            Assert.AreEqual(projectExpected, projectResult);
        }

        [TestMethod]
        public void GetProjectByName()
        {
            ProjectDTO projectExpected = new ProjectDTO()
            {
                Name = "Project1",
                Id = 0
            };

            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetByName(projectExpected.Name)).Returns(projectExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.GetByName(projectExpected.Name);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectDTO;

            mock.VerifyAll();
            Assert.AreEqual(projectExpected, projectResult);
        }

        [TestMethod]
        public void Delete()
        {
            ProjectDTO projectExpected = new ProjectDTO()
            {
                Name = "project3",
                Id = 0
            };

            List<ProjectDTO> project = new List<ProjectDTO>()
            {
                projectExpected,
                new ProjectDTO()
                {
                    Name = "buttonProj",
                    Id = 1
                },
                 new ProjectDTO()
                {
                    Name = "myProject",
                    Id = 2
                },
            };

            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.Delete(projectExpected.Id)).Returns(new ResponseMessage(""));
            var controller = new ProjectController(mock.Object);

            var result = controller.Delete(projectExpected.Id);
            Assert.IsTrue(result is NoContentResult);
            mock.VerifyAll();

        }

        [TestMethod]
        public void Update()
        {
            ProjectDTO projectExpected = new ProjectDTO()
            {
                Name = "proyectoEnEspaniol",
                Id = 0
            };

            ProjectDTO projectModified = new ProjectDTO()
            {
                Name = "project number 4",
                Id = 0
            };

            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.Update(projectExpected.Id, projectModified)).Returns(projectExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.Update(projectExpected.Id, projectModified);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectDTO;

            mock.VerifyAll();
            Assert.AreEqual(projectExpected, projectResult);
        }


        [TestMethod]
        public void GetAllBugs()
        {
            ProjectDTO project = new ProjectDTO()
            {
                Name = "project3",
                Id = 1
            };


            List<BugDTO> bugsExpected = new List<BugDTO>()
            {
                new BugDTO(){
                Name = "Not working button",
                Description = "Upload button not working",
                Version = "1",
                IsActive = true,
                Id = 0
                },
                new BugDTO(){
                Name = "Not working button",
                Description = "Upload button not working",
                Version = "1",
                IsActive = true,
                Id = 1
                }
            };

            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetBugs(1)).Returns(bugsExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.GetBugs(1);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as IEnumerable<BugDTO>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(bugsExpected, (System.Collections.ICollection)bugsResult, new BugComparer());
        }
        [TestMethod]
        public void GetBugsQuantity()
        {
            Project project = new Project()
            {
                Id = 1,
                Name = "project",
                Bugs = new List<Bug>()
                    {
                     new Bug(){
                         Name = "Bug",
                         Id = 0,
                     },
                     new Bug(){
                          Name = "Project2",
                          Id = 1
                     }
                }
            };

            int cantExpected = project.Bugs.Count();
            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetBugsQuantity(project.Id)).Returns(new BugsQuantity(cantExpected));
            var controller = new ProjectController(mock.Object);

            var result = controller.GetBugsQuantity(project.Id);
            var okResult = result as OkObjectResult;
            var cantResult = okResult.Value as BugsQuantity;

            mock.VerifyAll();
            Assert.AreEqual(cantExpected, cantResult.Quantity);
        }

        [TestMethod]
        public void GetAllDevelopers()
        {
            List<DeveloperDTO> devsExpected = new List<DeveloperDTO>()
            {
                new DeveloperDTO(){
                Name = "Ivan",
                Lastname = "monja",
                Username = "Ivo",
                Password = "456738",
                Email = "ivi@gmail.com"

                },
                new DeveloperDTO(){
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com"
                }
            };

            ProjectDTO project = new ProjectDTO()
            {
                Name = "project3",
                Id = 1
            };


            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetDevelopers(project.Id)).Returns(devsExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.GetDevelopers(project.Id);
            var okResult = result as OkObjectResult;
            var devsResult = okResult.Value as IEnumerable<DeveloperDTO>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(devsExpected, (System.Collections.ICollection)devsResult, new UserComparer());
        }

        [TestMethod]
        public void GetAllTesters()
        {
            List<TesterDTO> testersExpected = new List<TesterDTO>()
            {
                new TesterDTO(){
                 Name = "Ivan",
                Lastname = "monja",
                Username = "Ivo",
                Password = "456738",
                Email = "ivi@gmail.com"

                },
                new TesterDTO(){
                  Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com"
                }
            };

            ProjectDTO project = new ProjectDTO()
            {
                Name = "project3",
                Id = 1,
            };


            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetTesters(project.Id)).Returns(testersExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.GetTesters(project.Id);
            var okResult = result as OkObjectResult;
            var testersResult = okResult.Value as IEnumerable<TesterDTO>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(testersExpected, (System.Collections.ICollection)testersResult, new UserComparer());
        }

        [TestMethod]
        public void AddDeveloperToProject()
        {
            ProjectDTO project = new ProjectDTO()
            {
                Name = "project3",
                Id = 1,
            };

            DeveloperDTO devExpected = new DeveloperDTO()
            {
                Id = 2,
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com"
            };


            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.AddDeveloperToProject(project.Id, devExpected.Id)).Returns(devExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.AddDeveloperToProject(project.Id, devExpected.Id);
            var okResult = result as OkObjectResult;
            var devResult = okResult.Value as DeveloperDTO;

            mock.VerifyAll();
            Assert.AreEqual(devExpected, devResult);
        }

        [TestMethod]
        public void AddTesterToProject()
        {
            ProjectDTO project = new ProjectDTO()
            {
                Name = "project3",
                Id = 1,
            };

            TesterDTO testerExpected = new TesterDTO()
            {
                Id = 2,
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com"
            };


            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.AddTesterToProject(project.Id, testerExpected.Id)).Returns(testerExpected);
            var controller = new ProjectController(mock.Object);

            var result = controller.AddTesterToProject(project.Id, testerExpected.Id);
            var okResult = result as OkObjectResult;
            var testerResult = okResult.Value as TesterDTO;

            mock.VerifyAll();
            Assert.AreEqual(testerExpected, testerResult);
        }

        [TestMethod]
        public void DeleteDeveloperFromProject()
        {
            ProjectDTO project = new ProjectDTO()
            {
                Id = 1,
            };

            DeveloperDTO dev = new DeveloperDTO()
            {
                Id = 2,
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com"
            };
            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.RemoveDeveloperFromProject(project.Id, dev.Id)).Returns(new ResponseMessage("Deleted from project"));
            var controller = new ProjectController(mock.Object);

            var result = controller.RemoveDeveloperFromProject(project.Id, dev.Id);
            var okResult = result as OkObjectResult;
            var devResult = okResult.Value as ResponseMessage;

            mock.VerifyAll();
            Assert.IsTrue(devResult is ResponseMessage);
        }

        [TestMethod]
        public void DeleteTesterFromProject()
        {
            ProjectDTO project = new ProjectDTO()
            {
                Id = 1,
            };

            TesterDTO tester = new TesterDTO()
            {
                Id = 2,
                Name = "Agustina",
                Lastname = "didios",
                Username = "Agus",
                Password = "rosadopastel",
                Email = "hell@yahoo.com"
            };
            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.RemoveTesterFromProject(project.Id, tester.Id)).Returns(new ResponseMessage("Deleted from project"));
            var controller = new ProjectController(mock.Object);

            var result = controller.RemoveTesterFromProject(project.Id, tester.Id);
            var okResult = result as OkObjectResult;
            var testerResult = okResult.Value as ResponseMessage;

            mock.VerifyAll();
            Assert.IsTrue(testerResult is ResponseMessage);
        }

        [TestMethod]
        public void GetProjectCost()
        {
            ProjectDTO projectExpected = new ProjectDTO()
            {
                Name = "Project1",
                Id = 0
            };
            int expectedCost = 5;
            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetProjectCost(projectExpected.Id)).Returns(new ProjectCost(expectedCost));
            var controller = new ProjectController(mock.Object);

            var result = controller.GetProjectCost(projectExpected.Id);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectCost;

            mock.VerifyAll();
            Assert.AreEqual(expectedCost, projectResult.Cost);
        }

        [TestMethod]
        public void GetProjectDuration()
        {
            ProjectDTO projectExpected = new ProjectDTO()
            {
                Name = "Project1",
                Id = 0
            };
            int expectedDuration = 5;
            var mock = new Mock<IProjectBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetProjectDuration(projectExpected.Id)).Returns(new ProjectDuration(expectedDuration));
            var controller = new ProjectController(mock.Object);

            var result = controller.GetProjectDuration(projectExpected.Id);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectDuration;

            mock.VerifyAll();
            Assert.AreEqual(expectedDuration, projectResult.Duration);
        }
    }
}
