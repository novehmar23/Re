using Domain;
using Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System.Collections.Generic;
using System.Linq;
using TestDataAccess;

namespace TestTesterDataAccess
{
    [TestClass]
    public class TestTesterDataAccess : TestUserDataAccess<Tester>
    {
        private readonly TesterDataAccess testerDataAccess;
        public TestTesterDataAccess() : base()
        {
            testerDataAccess = new TesterDataAccess(bugManagerContext);
            userDataAccess = testerDataAccess;
            user = new Tester();
            users = bugManagerContext.Tester;
            userDifferentRole = new Developer();
            role = Roles.Tester;

        }

        [TestMethod]
        public void FilterBugsByStatus()
        {

            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    Id = 1,
                    Name = "bug1",
                    IsActive = true,
                    ProjectId = 23,
                },
                new Bug()
                {
                    Id = 2,
                    Name = "bug2",
                    IsActive = true,
                    ProjectId = 24,

                },
            };
            Project project1 = new Project()
            {
                Name = "project",
                Id = 23,

            };
            bugManagerContext.Add(project1);

            Project project2 = new Project()
            {
                Name = "project",
                Id = 24,

            };
            bugManagerContext.Add(project2);
            bugManagerContext.SaveChanges();

            bugManagerContext.Add(new Bug()
            {
                Id = 1,
                Name = "bug1",
                IsActive = true,
                ProjectId = 23,

            });
            bugManagerContext.Add(new Bug()
            {
                Id = 2,
                Name = "bug2",
                IsActive = true,
                ProjectId = 24,

            });
            bugManagerContext.SaveChanges();

            Tester tester = new Tester()
            {
                Id = 45,
                Name = "Agus",
                Username = "agustina",
                Projects = { project1, project2 }
            };

            bugManagerContext.Add(tester);
            bugManagerContext.SaveChanges();


            List<Bug> bugsResult = testerDataAccess.GetBugsByStatus(tester.Id, true);
            CollectionAssert.AreEqual(bugsExpected, bugsResult, new BugComparer());

        }

        [TestMethod]
        public void FilterBugsByName()
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    Id = 1,
                    Name = "bug1",
                    ProjectId = 23,

                },
                new Bug()
                {
                    Id = 2,
                    Name = "bug1",
                    ProjectId = 24,

                },
            };
            Tester tester = new Tester()
            {
                Name = "Agus",
                Username = "agustina",
            };

            bugManagerContext.Add(tester);
            bugManagerContext.SaveChanges();
            bugManagerContext.Add(new Project()
            {
                Name = "project",
                Id = 23,
                Testers = new List<Tester>() { tester }
            });
            bugManagerContext.Add(new Project()
            {
                Name = "project",
                Id = 24,
                Testers = new List<Tester>() { tester }

            });
            bugManagerContext.SaveChanges();

            bugManagerContext.Add(new Bug()
            {
                Id = 1,
                Name = "bug1",
                ProjectId = 23,


            });
            bugManagerContext.Add(new Bug()
            {
                Id = 2,
                Name = "bug1",
                ProjectId = 24,

            });
            bugManagerContext.SaveChanges();


            List<Bug> bugsResult = testerDataAccess.GetBugsByName(tester.Id, "bug1");
            CollectionAssert.AreEqual(bugsExpected, bugsResult, new BugComparer());
        }

        [TestMethod]
        public void FilterBugsByProject()
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    Id = 1,
                    Name = "bug3",
                    ProjectId = 23
                },
                new Bug()
                {
                    Id = 2,
                    Name = "bug4",
                    ProjectId = 23
                },
            };
            Tester tester = new Tester()
            {
                Name = "Agus",
                Username = "agustina",
            };

            bugManagerContext.Add(tester);
            bugManagerContext.SaveChanges();
            bugManagerContext.Add(new Project()
            {
                Name = "project",
                Id = 23,
                Testers = new List<Tester>() { tester }

            });
            bugManagerContext.SaveChanges();

            bugManagerContext.Add(new Bug()
            {
                Id = 1,
                Name = "bug3",
                ProjectId = 23

            });
            bugManagerContext.Add(new Bug()
            {
                Id = 2,
                Name = "bug4",
                ProjectId = 23

            });
            bugManagerContext.SaveChanges();


            List<Bug> bugsResult = testerDataAccess.GetBugsByProject(tester.Id, 23);
            CollectionAssert.AreEqual(bugsExpected, bugsResult, new BugComparer());
        }

        [TestMethod]
        public void GetAll()
        {
            var testersExpected = new List<Tester>
            {
                new Tester()
                {
                    Id = 1,
                    Name = "a",
                    Cost =5,
                    Email = "fghjk@ghj",
                    Lastname = "gimenez",
                    Password = "352683clave",
                    Username = "ertyu",
                }
             };
            bugManagerContext.Add(new Tester()
            {
                Id = 1,
                Name = "a",
                Cost = 5,
                Email = "fghjk@ghj",
                Lastname = "gimenez",
                Password = "352683clave",
                Username = "ertyu",
            });
            bugManagerContext.SaveChanges();
            List<Tester> testerDataBase = testerDataAccess.GetAllTesters().ToList();

            Assert.AreEqual(1, testerDataBase.Count);
            CollectionAssert.AreEqual(testersExpected, testerDataBase, new UserComparer());
        }
    }
}
