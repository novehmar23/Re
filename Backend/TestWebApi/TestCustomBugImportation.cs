using BusinessLogicInterfaces;
using CustomBugImportation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebApi.Controllers;

namespace TestWebApi
{
    [TestClass]
    public class TestCustomBugImportation
    {
        [TestMethod]
        public void GetImporters()
        {

            List<ImporterInfo> expectedImportersInfo = new List<ImporterInfo>
            {
                new ImporterInfo {
                                ImporterName = "Empty Importer",
                                Params = new List<Parameter>{}
                },
                new ImporterInfo {
                                ImporterName = "Empty Importer 2",
                                Params = new List<Parameter>{}
                }
            };

            var mock = new Mock<IBugBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.GetCustomImportersInfo(null)).Returns(expectedImportersInfo);
            var controller = new BugController(mock.Object);

            var result = controller.GetCustomImportersInfo(); ;
            var okResult = result as OkObjectResult;
            var actualImportersInfo = okResult.Value as IEnumerable<ImporterInfo>;


            mock.VerifyAll();
            CollectionAssert.AreEquivalent(expectedImportersInfo, (System.Collections.ICollection)actualImportersInfo);
        }

        [TestMethod]
        public void ImportBugs()
        {

            List<Parameter> parameters = new List<Parameter>()
            {
                new Parameter(){
                    Name = "path",
                    Type = ParameterType.STRING,
                    Value = "example.com"
                },
                new Parameter(){
                    Name = "port",
                    Type = ParameterType.INTEGER,
                    Value = "80"
                }
            };

            List<ImportedBug> expectedBugs = new List<ImportedBug>
            {
                new ImportedBug(){
                    Name = "Bug1",
                    Description = "The first bug",
                    IsActive = true,
                    ProjectName =  "The mega project"
                },
                new ImportedBug(){
                    Name = "Bug2",
                    Description = "The second bug",
                    Time = 67,
                    ProjectId =  2
                },
                new ImportedBug(){
                    Name = "Bug3",
                    Description = "The third bug",
                    IsActive = false,
                    CompletedById =  2,
                    Version = "1.0.0"
                }
            };
            var mock = new Mock<IBugBusinessLogic>(MockBehavior.Strict);
            mock.Setup(b => b.ImportBugsCustom("ImporterName", parameters, null));
            var controller = new BugController(mock.Object);

            ImporterInfo info = new ImporterInfo()
            {
                ImporterName = "ImporterName",
                Params = parameters
            };
            var result = controller.ImportBugsCustom(info);
            var okResult = result as OkObjectResult;


            mock.VerifyAll();


        }
    }
}
