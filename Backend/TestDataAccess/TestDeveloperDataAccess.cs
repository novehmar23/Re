using Domain;
using Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace TestDataAccess
{
    [TestClass]
    public class TestDeveloperDataAccess : TestUserDataAccess<Developer>
    {

        private readonly DeveloperDataAccess devDataAccess;
        public TestDeveloperDataAccess() : base()
        {
            devDataAccess = new DeveloperDataAccess(bugManagerContext);
            userDataAccess = devDataAccess;
            user = new Developer();
            users = bugManagerContext.Developer;
            userDifferentRole = new Admin();
            role = Roles.Dev;
        }

        [TestMethod]
        public void QuantityBugsResolved()
        {
            bugManagerContext.Add(new Project()
            {
                Name = "project",
                Id = 2
            });
            bugManagerContext.SaveChanges();

            Developer expectedDev = new Developer
            {
                Id = 2,
                Username = "developerPedro",
                Name = "Pedro",
                Lastname = "López",
                Password = "fransico234",
                Email = "pedrooo2@hotmail.com"

            };
            devDataAccess.Create(expectedDev);
            bugManagerContext.Add(new Bug()
            {
                CompletedById = 2,
                Id = 1,
                ProjectId = 2
            });
            bugManagerContext.Add(new Bug()
            {
                CompletedById = 2,
                Id = 2,
                ProjectId = 2
            });
            bugManagerContext.Add(new Bug()
            {
                Id = 3,
                ProjectId = 2

            });
            bugManagerContext.SaveChanges();
            int expectedQuantity = 2;

            int result = devDataAccess.GetQuantityBugsResolved(expectedDev.Id);

            Assert.AreEqual(expectedQuantity, result);
        }

        [TestMethod]
        public void GetAll()
        {
            var devsExpected = new List<Developer>
            {
                new Developer()
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
            bugManagerContext.Add(new Developer()
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
            List<Developer> devsDataBase = devDataAccess.GetAllDevs().ToList();

            Assert.AreEqual(1, devsDataBase.Count);
            CollectionAssert.AreEqual(devsExpected, devsDataBase, new UserComparer());
        }

    }
}
