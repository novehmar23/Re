using BusinessLogic;
using Domain;
using DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestWorkBusinessLogic
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
                Id = 1,
                Name = "work1",
                Cost = 3,
                Time = 2,
                ProjectId = 1,
                Project = new Project()
                {
                    Id = 1,
                    Name = "project"
                },
                ProjectName = "project"
            };

        }

        [TestMethod]
        public void CreateWork()
        {
            var mock = new Mock<IWorkDataAccess>(MockBehavior.Strict);
            var expectedWork = new WorkDTO(work);
            mock.Setup(d => d.Create(It.IsAny<Work>())).Returns(work);
            var workBusinessLogic = new WorkBusinessLogic(mock.Object);
            var workResult = workBusinessLogic.Add(expectedWork);
            mock.VerifyAll();

            Assert.AreEqual(workResult, expectedWork);
        }

        [TestMethod]
        public void GetById()
        {
            Work workExpected = new Work()
            {
                Name = "Login",
                Time = 3,
                Cost = 2,
                Id = 1
            };

            var mock = new Mock<IWorkDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetById(workExpected.Id)).Returns(workExpected);
            var workBusinessLogic = new WorkBusinessLogic(mock.Object);

            var result = workBusinessLogic.GetById(workExpected.Id);

            Assert.AreEqual(workExpected, result);
        }

        [TestMethod]
        public void CreateInvalidWork()
        {
            Work invalidWork = new Work()
            {
                Name = "invalid work",
                Cost = 2,
            };

            var mock = new Mock<IWorkDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.Create(invalidWork)).Returns(invalidWork);
            var workBusinessLogic = new WorkBusinessLogic(mock.Object);

            Assert.ThrowsException<ValidationException>(() => workBusinessLogic.Add(new WorkDTO(invalidWork)));
            mock.Verify(m => m.Create(invalidWork), Times.Never);

        }

        [TestMethod]
        public void GetAll()
        {
            List<Work> worksExpected = new List<Work>()
            {
                new Work()
                {
                     Id = 0,
                Name = "work1",
                Cost = 3,
                ProjectId = 3,
                ProjectName = "project",
                Time = 3,
                Project = new Project()
                {
                    Id = 3,
                    Name = "project"
                }
                },
                new Work()
                {
                    Id = 1,
                Name = "work",
                Cost = 4,
                ProjectId = 3,
                ProjectName = "project",
                Time = 3,
                Project = new Project()
                {
                    Id = 3,
                    Name = "project"
                }
                },
                 new Work()
                {
                    Id = 2,
                Name = "Bug1",
                Cost = 7,
                ProjectId = 4,
                 ProjectName = "project",
                Time = 3,
                Project = new Project()
                {
                    Id = 4,
                    Name = "project"
                }
                },
            };

            string token = "dfgh-fgh-fds";

            var mock = new Mock<IWorkDataAccess>(MockBehavior.Strict);
            mock.Setup(b => b.GetAll(token)).Returns(worksExpected);
            var workBusinessLogic = new WorkBusinessLogic(mock.Object);

            var result = workBusinessLogic.GetAll(token);

            Assert.IsTrue(worksExpected.ConvertAll(b => new WorkDTO(b)).SequenceEqual(result));
        }
    }
}
