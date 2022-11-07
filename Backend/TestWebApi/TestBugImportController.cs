using BusinessLogicInterfaces;
using Domain;
using Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApi.Controllers;


namespace TestWebApi
{
    [TestClass]
    public class TestBugImportController
    {


        [TestMethod]
        public void ImportXML()
        {
            List<Bug> bugsExpected = new List<Bug>()
            {
                new Bug(){
                Name = "Not working button",
                Description = "Upload button not working",
                Version = "1",
                IsActive = true,
                CompletedBy = null,
                Id = 0
                },
                new Bug(){
                Name = "Not working button",
                Description = "Upload button not working",
                Version = "1",
                IsActive = true,
                CompletedBy = null,
                Id = 1
                }
            };

            string path = "file.xml";
            var mock = new Mock<IBugBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.ImportBugs(path, ImportCompany.XML, null));
            var controller = new BugController(mock.Object);

            controller.ImportBugs(path, ImportCompany.XML.ToString());

            mock.VerifyAll();
        }

    }
}

