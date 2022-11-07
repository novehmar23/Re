using Domain;
using Domain.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace TestDataAccess
{
    [TestClass]
    public class TestBugDataAccess
    {

        private readonly DbConnection connection;
        private readonly BugDataAccess bugDataAccess;
        private readonly LoginDataAccess loginDataAccess;
        private readonly BugManagerContext bugManagerContext;
        private readonly DbContextOptions<BugManagerContext> contextOptions;

        public TestBugDataAccess()
        {
            connection = new SqliteConnection("Filename=:memory:");
            contextOptions = new DbContextOptionsBuilder<BugManagerContext>().UseSqlite(connection).Options;
            bugManagerContext = new BugManagerContext(contextOptions);
            bugDataAccess = new BugDataAccess(bugManagerContext);
            loginDataAccess = new LoginDataAccess(bugManagerContext);
        }

        [TestInitialize]
        public void Setup()
        {
            connection.Open();
            bugManagerContext.Database.EnsureCreated();

            Project project = new Project()
            {
                Id = 1,
                Name = "project1"
            };
            bugManagerContext.Projects.Add(project);
            bugManagerContext.SaveChanges();

        }

        [TestCleanup]
        public void CleanUp()
        {
            bugManagerContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AdminGetAll()
        {
            string token = "sdfg-uytr-fds-dsdf";
            string username = "jose";
            int id = 3;

            Admin admin = new Admin()
            {
                Username = username,
                Name = "ivan",
                Email = "dfgh@fghj.com",
                Id = id,
                Lastname = "dfgh",
                Password = "122334"
            };

            LoginToken loginToken = new LoginToken
            {
                Token = token,
                Username = username
            };

            bugManagerContext.Add(admin);
            loginDataAccess.SaveLogin(loginToken);

            var bugsExpected = new List<Bug>
            {
                new Bug
                {
                    Id = 1,
                    Name = "a",
                    Description = "a",
                    ProjectId = 1,
                    Version = "1.0",
                    IsActive = true
        }
    };
            bugManagerContext.Add(new Bug
            {
                Id = 1,
                Name = "a",
                Description = "a",
                ProjectId = 1,
                Version = "1.0",
                IsActive = true
            });
            bugManagerContext.SaveChanges();
            List<Bug> bugDataBase = bugDataAccess.GetAll(token).ToList();

            Assert.AreEqual(1, bugDataBase.Count);
            CollectionAssert.AreEqual(bugsExpected, bugDataBase, new BugComparer());
        }

        [TestMethod]
        public void DevGetAll()
        {
            string token = "sdfg-uytr-fds-dsdf";
            string username = "jose";
            int id = 3;

            Developer dev = new Developer()
            {
                Username = username,
                Name = "ivan",
                Email = "dfgh@fghj.com",
                Id = id,
                Lastname = "dfgh",
                Password = "122334"
            };

            LoginToken loginToken = new LoginToken
            {
                Token = token,
                Username = username
            };

            Project proj = new Project()
            {
                Id = 24,
                Name = "Project",
                Developers = { dev },
            };

            bugManagerContext.Add(proj);
            bugManagerContext.Add(dev);
            loginDataAccess.SaveLogin(loginToken);

            var bugsExpected = new List<Bug>
            {
                new Bug
                {
                    Id = 1,
                    Name = "a",
                    Description = "a",
                    ProjectId = 24,
                    Version = "1.0",
                    IsActive = true
        }
    };
            bugManagerContext.Add(new Bug
            {
                Id = 1,
                Name = "a",
                Description = "a",
                ProjectId = 24,
                Version = "1.0",
                IsActive = true
            });
            bugManagerContext.SaveChanges();
            List<Bug> bugDataBase = bugDataAccess.GetAll(token).ToList();

            Assert.AreEqual(1, bugDataBase.Count);
            CollectionAssert.AreEqual(bugsExpected, bugDataBase, new BugComparer());
        }

        [TestMethod]
        public void TesterGetAll()
        {
            string token = "sdfg-uytr-fds-dsdf";
            string username = "jose";
            int id = 3;

            Tester tester = new Tester()
            {
                Username = username,
                Name = "ivan",
                Email = "dfgh@fghj.com",
                Id = id,
                Lastname = "dfgh",
                Password = "122334"
            };

            LoginToken loginToken = new LoginToken
            {
                Token = token,
                Username = username
            };

            Project proj = new Project()
            {
                Id = 26,
                Name = "Project",
                Testers = { tester },
            };

            bugManagerContext.Add(proj);
            bugManagerContext.Add(tester);
            loginDataAccess.SaveLogin(loginToken);

            var bugsExpected = new List<Bug>
            {
                new Bug
                {
                    Id = 1,
                    Name = "a",
                    Description = "a",
                    ProjectId = 26,
                    Version = "1.0",
                    IsActive = true
        }
    };
            bugManagerContext.Add(new Bug
            {
                Id = 1,
                Name = "a",
                Description = "a",
                ProjectId = 26,
                Version = "1.0",
                IsActive = true
            });
            bugManagerContext.SaveChanges();
            List<Bug> bugDataBase = bugDataAccess.GetAll(token).ToList();

            Assert.AreEqual(1, bugDataBase.Count);
            CollectionAssert.AreEqual(bugsExpected, bugDataBase, new BugComparer());
        }

        [TestMethod]
        public void Update()
        {
            Developer dev = new Developer()
            {
                Id = 4,
                Name = "Juan"
            };
            bugManagerContext.Add(dev);
            bugManagerContext.SaveChanges();

            Project project = new Project()
            {
                Name = "project",
                Id = 4,
            };
            bugManagerContext.Add(project);
            bugManagerContext.SaveChanges();

            Bug bug = bugDataAccess.Create(new Bug
            {
                Id = 2,
                Name = "b",
                Description = "a",
                Version = "1.0",
                ProjectId = 4,
                IsActive = false
            });

            var bugUpdated = new Bug
            {
                Id = 2,
                Name = "a",
                Description = "a",
                ProjectId = 4,
                Version = "1.0",
                IsActive = true
            };

            Bug bugModified = bugDataAccess.Update(bug.Id, bugUpdated);
            bugManagerContext.SaveChanges();


            Assert.AreEqual(0, new BugComparer().Compare(bugUpdated, bugModified));

        }

        [TestMethod]
        public void Create()
        {
            bugManagerContext.Add(new Project()
            {
                Name = "project",
                Id = 2
            });
            bugManagerContext.SaveChanges();
            Bug expectedBug = new Bug()
            {
                Id = 1,
                Name = "Error",
                Description = "Erorr critico",
                ProjectId = 2,
                Version = "2.0",
                IsActive = true,
            };

            bugDataAccess.Create(new Bug()
            {
                Id = 1,
                Name = "Error",
                Description = "Erorr critico",
                Version = "2.0",
                ProjectId = 2,
                IsActive = true,

            });

            var bugSaved = bugDataAccess.GetById(1);
            Assert.AreEqual(0, new BugComparer().Compare(expectedBug, bugSaved));

        }

        [TestMethod]
        public void CreateNull()
        {
            Assert.ThrowsException<NonexistentBugException>(() => bugDataAccess.Create(null));

        }

        [TestMethod]
        public void GetNonExistant()
        {
            Assert.ThrowsException<NonexistentBugException>(() => bugDataAccess.GetById(90));
        }

        [TestMethod]
        public void UpdateNonExistant()
        {
            Assert.ThrowsException<NonexistentBugException>(() => bugDataAccess.Update(90, null));
        }

        [TestMethod]
        public void AddsIdFromProjectName()
        {
            Project project1 = new Project()
            {
                Name = "Project5",
                Id = 5
            };

            bugManagerContext.Add(project1);
            bugManagerContext.SaveChanges();

            bugDataAccess.Create(new Bug()
            {
                Id = 1,
                Name = "Error",
                Description = "Erorr critico",
                Version = "2.0",
                IsActive = true,
                ProjectName = "Project5"
            });
            var bugSaved1 = bugDataAccess.GetById(1);
            Assert.AreEqual(bugSaved1.ProjectId, 5);
        }


        [TestMethod]
        public void AddsIdFromProjectNameMultipleProjects()
        {
            Project project1 = new Project()
            {
                Name = "Project 1",
                Id = 2
            };

            Project project2 = new Project()
            {
                Name = "Project 2",
                Id = 3
            };

            bugManagerContext.Add(project1);
            bugManagerContext.SaveChanges();
            bugManagerContext.Add(project2);
            bugManagerContext.SaveChanges();

            bugDataAccess.Create(new Bug()
            {
                Id = 1,
                Name = "Error",
                Description = "Erorr critico",
                Version = "2.0",
                IsActive = true,
                ProjectName = "Project 1"
            });

            bugDataAccess.Create(new Bug()
            {
                Id = 2,
                Name = "Error",
                Description = "Erorr critico",
                Version = "2.0",
                IsActive = true,
                ProjectName = "Project 2"
            });

            var bugSaved1 = bugDataAccess.GetById(1);
            var bugSaved2 = bugDataAccess.GetById(2);
            Assert.AreEqual(bugSaved1.ProjectId, 2);
            Assert.AreEqual(bugSaved2.ProjectId, 3);
        }


        [TestMethod]
        public void Delete()
        {
            string token = "sdfg-uytr-fds-dsdf";
            string username = "jose";
            int id = 3;

            Admin admin = new Admin()
            {
                Username = username,
                Name = "ivan",
                Email = "dfgh@fghj.com",
                Id = id,
                Lastname = "dfgh",
                Password = "122334"
            };

            LoginToken loginToken = new LoginToken
            {
                Token = token,
                Username = username
            };

            bugManagerContext.Add(admin);
            loginDataAccess.SaveLogin(loginToken);

            Bug notExpectedBug = new Bug()
            {
                Id = 1,
                Name = "Error",
                Description = "Erorr critico",
                ProjectId = 1,
                Version = "2.0",
                IsActive = true
            };
            bugDataAccess.Create(notExpectedBug);
            bugDataAccess.Delete(notExpectedBug.Id);
            var bugsSaved = bugDataAccess.GetAll(token).ToList();

            CollectionAssert.DoesNotContain(bugsSaved, notExpectedBug);

        }

        [TestMethod]
        public void ResolveBug()
        {

            string token = "sdfg-uytr-fds-dsdf";
            string username = "jose";
            int id = 3;

            Developer dev = new Developer()
            {
                Username = username,
                Name = "ivan",
                Email = "dfgh@fghj.com",
                Id = id,
                Lastname = "dfgh",
                Password = "122334"
            };

            LoginToken loginToken = new LoginToken
            {
                Token = token,
                Username = username
            };

            bugManagerContext.Add(dev);
            loginDataAccess.SaveLogin(loginToken);


            Project project = new Project()
            {
                Name = "project",
                Id = 4,
                Developers = new List<Developer>() { dev }
            };
            bugManagerContext.Add(project);
            bugManagerContext.SaveChanges();

            Bug bug = bugDataAccess.Create(new Bug
            {

                Id = 2,
                Name = "b",
                Description = "a",
                Version = "1.0",
                ProjectId = 4,
                IsActive = true,
                CompletedById = null,
                CompletedBy = null,
            });

            var bugUpdated = new Bug
            {
                Id = 2,
                Name = "b",
                Description = "a",
                ProjectId = 4,
                Version = "1.0",
                IsActive = false,
                CompletedById = id,
                CompletedBy = dev
            };

            Bug bugModified = bugDataAccess.ResolveBug(bug.Id, token);
            bugManagerContext.SaveChanges();


            Assert.AreEqual(0, new BugComparer().Compare(bugUpdated, bugModified));

        }

        [TestMethod]
        public void UnresolveBug()
        {

            string token = "sdfg-uytr-fds-dsdf";
            string username = "jose";
            int id = 3;

            Developer dev = new Developer()
            {
                Username = username,
                Name = "ivan",
                Email = "dfgh@fghj.com",
                Id = id,
                Lastname = "dfgh",
                Password = "122334"
            };

            LoginToken loginToken = new LoginToken
            {
                Token = token,
                Username = username
            };

            bugManagerContext.Add(dev);
            loginDataAccess.SaveLogin(loginToken);


            Project project = new Project()
            {
                Name = "project",
                Id = 4,
                Developers = new List<Developer>() { dev }
            };
            bugManagerContext.Add(project);
            bugManagerContext.SaveChanges();

            Bug bug = bugDataAccess.Create(new Bug
            {
                Id = 2,
                Name = "b",
                Description = "a",
                ProjectId = 4,
                Version = "1.0",
                IsActive = false,
                CompletedById = id

            });

            var bugUpdated = new Bug
            {
                Id = 2,
                Name = "b",
                Description = "a",
                Version = "1.0",
                ProjectId = 4,
                IsActive = true,
                CompletedById = null
            };

            Bug bugModified = bugDataAccess.UnresolveBug(bug.Id, token);
            bugManagerContext.SaveChanges();


            Assert.AreEqual(0, new BugComparer().Compare(bugUpdated, bugModified));

        }
    }
}
