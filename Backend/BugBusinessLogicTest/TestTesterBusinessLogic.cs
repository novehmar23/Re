using BusinessLogic;
using Domain;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace TestTesterBusinessLogic
{
    [TestClass]
    public class TestTesterBusinessLogic
    {


        [TestMethod]
        public void CreateTester()
        {
            Tester tester = new Tester()
            {
                Id = 1,
                Username = "pablito",
                Name = "Pedro",
                Lastname = "Rodriguez",
                Password = "asdfsdf3242",
                Email = "pedro@hotmail.com",
                Cost = 1

            };
            var mock = new Mock<ITesterDataAccess>(MockBehavior.Strict);
            mock.Setup(t => t.GetAllUsernames()).Returns(new List<string>());
            mock.Setup(t => t.Create(It.IsAny<Tester>())).Returns<Tester>(t => t);
            var testerBusinessLogic = new TesterBusinessLogic(mock.Object);

            var testerResult = testerBusinessLogic.Add(new TesterDTO(tester));
            mock.VerifyAll();

            Assert.AreEqual(testerResult, new TesterDTO(tester));
        }




        [TestMethod]
        public void FilterBugsByStatus()
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    Name = "bug1",
                    IsActive = true,
                Description = "button not work",
                Version = "4.5",
                Time = 4,
                ProjectId = 2,
                CompletedById = 6,
                Id = 3,
                Project = new Project()
                {
                    Id = 2,
                    Name = "project",
                },
                },
                new Bug()
                {
                    Name = "bug2",
                    IsActive = true,
                Description = "button not work",
                Version = "4.5",
                Time = 4,
                ProjectId = 2,
                CompletedById = 6,
                Id = 3,
                Project = new Project()
                {
                    Id = 2,
                    Name = "project",
                },
                },
            };
            int idTester = 1;

            var mock = new Mock<ITesterDataAccess>(MockBehavior.Strict);
            mock.Setup(t => t.GetBugsByStatus(idTester, true)).Returns(bugsExpected);
            var testerBusinessLogic = new TesterBusinessLogic(mock.Object);


            var bugsResult = testerBusinessLogic.GetBugsByStatus(idTester, true);

            mock.VerifyAll();
            Assert.IsTrue(bugsExpected.ConvertAll(b => new BugDTO(b)).SequenceEqual(bugsResult));
        }

        [TestMethod]
        public void FilterBugsByName()
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    Name = "bug1",
                Description = "button not work",
                Version = "4.5",
                Time = 4,
                ProjectId = 2,
                CompletedById = 6,
                Id = 3,
                Project = new Project()
                {
                    Id = 2,
                    Name = "project",
                },
                },
                new Bug()
                {
                    Name = "bug1",
                Description = "button not work",
                Version = "4.5",
                Time = 4,
                ProjectId = 2,
                CompletedById = 6,
                Id = 3,
                Project = new Project()
                {
                    Id = 2,
                    Name = "project",
                },
                },
            };
            int idTester = 1;
            var mock = new Mock<ITesterDataAccess>(MockBehavior.Strict);
            mock.Setup(t => t.GetBugsByName(idTester, "bug1")).Returns(bugsExpected);
            var testerBusinessLogic = new TesterBusinessLogic(mock.Object);


            var bugsResult = testerBusinessLogic.GetBugsByName(idTester, "bug1");

            mock.VerifyAll();
            Assert.IsTrue(bugsExpected.ConvertAll(b => new BugDTO(b)).SequenceEqual(bugsResult));
        }

        [TestMethod]
        public void FilterBugsByProject()
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    ProjectId = 3,
                    Name = "bug",
                Description = "button not work",
                Version = "4.5",
                Time = 4,
                CompletedById = 6,
                Id = 3,
                Project = new Project()
                {
                    Id = 3,
                    Name = "project",
                },
                },
                new Bug()
                {
                    ProjectId = 3,
                    Name = "bug",
                Description = "button not work",
                Version = "4.5",
                Time = 4,
                CompletedById = 6,
                Id = 3,
                Project = new Project()
                {
                    Id = 3,
                    Name = "project",
                },
                },
            };
            int idTester = 1;
            var mock = new Mock<ITesterDataAccess>(MockBehavior.Strict);
            mock.Setup(t => t.GetBugsByProject(idTester, 3)).Returns(bugsExpected);
            var testerBusinessLogic = new TesterBusinessLogic(mock.Object);

            var bugsResult = testerBusinessLogic.GetBugsByProject(idTester, 3);

            mock.VerifyAll();
            Assert.IsTrue(bugsExpected.ConvertAll(b => new BugDTO(b)).SequenceEqual(bugsResult));
        }

        [TestMethod]
        public void VerifyRole()
        {
            string token = "asdfdasf";
            var mock = new Mock<ITesterDataAccess>(MockBehavior.Strict);
            mock.Setup(m => m.VerifyRole(token)).Returns(true);
            var testerBusinessLogic = new TesterBusinessLogic(mock.Object);

            var isRole = testerBusinessLogic.VerifyRole(token);
            mock.VerifyAll();

            Assert.IsTrue(isRole);
        }

        [TestMethod]
        public void VerifyRoleNotValid()
        {
            string token = "23423423";
            var mock = new Mock<ITesterDataAccess>(MockBehavior.Strict);
            mock.Setup(m => m.VerifyRole(token)).Returns(false);
            var testerBusinessLogic = new TesterBusinessLogic(mock.Object);

            var isRole = testerBusinessLogic.VerifyRole(token);
            mock.VerifyAll();

            Assert.IsFalse(isRole);
        }
        public void CreateInvalidTester()
        {
            Tester invalidTester = new Tester
            {
                Id = 1,
                Username = "admin",
                Name = "Pedro",
                Lastname = "Rodriguez",
                Email = "pedrooo@hotmail.com"

            };

            var mock = new Mock<ITesterDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Create(invalidTester)).Returns(invalidTester);
            var testerBusinessLogic = new TesterBusinessLogic(mock.Object);

            Assert.ThrowsException<ValidationException>(() => testerBusinessLogic.Add(new TesterDTO(invalidTester)));
            mock.Verify(m => m.Create(invalidTester), Times.Never);
        }

        [TestMethod]
        public void GetAll()
        {
            List<TesterDTO> testersExpected = new List<TesterDTO>()
            {
                new TesterDTO(){
                    Username = "juana",
                Name = "Juana",
                Lastname = "López",
                Password = "Juana1223#@",
                Email = "juana.perez@gmail.com",
                },
                new TesterDTO(){
                Username = "juana",
                Name = "Juana",
                Lastname = "López",
                Password = "Juana1223#@",
                Email = "juana.perez@gmail.com"
                }
            };


            var mock = new Mock<ITesterDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetAllTesters()).Returns(testersExpected.ConvertAll(p => p.ConvertToDomain()));
            var testerBusinessLogic = new TesterBusinessLogic(mock.Object);

            var result = testerBusinessLogic.GetAllTesters();

            Assert.IsTrue(testersExpected.SequenceEqual(result));
        }
    }
}
