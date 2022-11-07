using CustomBugImportation;
using JSONImporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TestJSONImporter
{
    [TestClass]
    public class TestImporter
    {
        const string baseDirectory = "../../../TestFiles/";
        private Importer jsonImporter;

        [TestInitialize]
        public void CreateImporterInstance()
        {
            jsonImporter = new Importer();
        }

        [TestMethod]
        public void ImportOneBug()
        {
            string fullPath = baseDirectory + "OneBug.json";
            List<Parameter> parameters = new List<Parameter>()
            {
                new Parameter(){
                    Name = "path",
                    Type = ParameterType.STRING,
                    Value = fullPath
                }
            };
            List<ImportedBug> actualBugs = jsonImporter.ImportBugs(parameters);

            List<ImportedBug> expectedBugs = new List<ImportedBug>()
            {
                new ImportedBug(){
                Name = "Bug1",
                Description = "This is the first bug from the json",
                Version = "1.00",
                IsActive = true,
                ProjectId = 3,
                ProjectName = "The Project",
                Time = 100
                }
            };

            Assert.IsTrue(actualBugs.SequenceEqual(expectedBugs));
        }

        [TestMethod]
        public void ImportThreeBugs()
        {
            string fullPath = baseDirectory + "ThreeBugs.json";
            List<Parameter> parameters = new List<Parameter>()
            {
                new Parameter(){
                    Name = "path",
                    Type = ParameterType.STRING,
                    Value = fullPath
                }
            };
            List<ImportedBug> actualBugs = jsonImporter.ImportBugs(parameters);

            List<ImportedBug> expectedBugs = new List<ImportedBug>()
            {
                new ImportedBug(){
                Name = "Bug1",
                Description = "This is the first bug from the json",
                Version = "1.00",
                IsActive = true,
                ProjectId = 3,
                ProjectName = "The Project",
                Time = 100
                },
                new ImportedBug(){
                Name = "Bug2",
                Description = "This is the second bug from the json",
                Version = "2.2",
                IsActive = false,
                ProjectId = 5,
                ProjectName = "The Project Revange",
                CompletedById = 2,
                Time = 200
                },
                new ImportedBug(){
                Name = "Third Bug",
                IsActive = true,
                ProjectId = 5,
                ProjectName = "The Project Revange",
                Time = 350
                }
            };

            Assert.IsTrue(actualBugs.SequenceEqual(expectedBugs));
        }


        [TestMethod]
        public void ImportEmptyBug()
        {
            string fullPath = baseDirectory + "Empty.json";
            List<Parameter> parameters = new List<Parameter>()
            {
                new Parameter(){
                    Name = "path",
                    Type = ParameterType.STRING,
                    Value = fullPath
                }
            };
            List<ImportedBug> actualBugs = jsonImporter.ImportBugs(parameters);


            Assert.IsTrue(actualBugs.Count == 0);
        }

        [TestMethod]
        public void MissingInfoBug()
        {
            string fullPath = baseDirectory + "MissingInfoBug.json";
            List<Parameter> parameters = new List<Parameter>()
            {
                new Parameter(){
                    Name = "path",
                    Type = ParameterType.STRING,
                    Value = fullPath
                }
            };
            List<ImportedBug> actualBugs = jsonImporter.ImportBugs(parameters);

            List<ImportedBug> expectedBugs = new List<ImportedBug>()
            {
                new ImportedBug(){
                Name = "Missing info  Bug",
                IsActive = true,
                ProjectId = 5,
                ProjectName = "The Project Revange",
                Time = 200
                }
            };

            Assert.IsTrue(actualBugs.SequenceEqual(expectedBugs));
        }

        [TestMethod]
        public void FailedJsonImport()
        {
            string fullPath = baseDirectory + "FailedJSON.json";
            List<Parameter> parameters = new List<Parameter>()
            {
                new Parameter(){
                    Name = "path",
                    Type = ParameterType.STRING,
                    Value = fullPath
                }
            };
            Assert.ThrowsException<CustomImporterException>(() =>
                            jsonImporter.ImportBugs(parameters)
            );

        }

        [TestMethod]
        public void NoFileImport()
        {
            string fullPath = baseDirectory + "adsfasdf";
            List<Parameter> parameters = new List<Parameter>()
            {
                new Parameter(){
                    Name = "path",
                    Type = ParameterType.STRING,
                    Value = fullPath
                }
            };
            Assert.ThrowsException<CustomImporterException>(() =>
                            jsonImporter.ImportBugs(parameters)
            );

        }

        [TestMethod]
        public void FolderImport()
        {
            string fullPath = baseDirectory;
            List<Parameter> parameters = new List<Parameter>()
            {
                new Parameter(){
                    Name = "path",
                    Type = ParameterType.STRING,
                    Value = fullPath
                }
            };
            Assert.ThrowsException<CustomImporterException>(() =>
                            jsonImporter.ImportBugs(parameters)
            );

        }

        [TestMethod]
        public void GetImporterInfo()
        {
            ImporterInfo actualImporterInfo = jsonImporter.GetImporterInfo();

            var expectedImporterInfo = new ImporterInfo
            {
                ImporterName = "JSON Importer",
                Params = new List<Parameter>
                {
                    new Parameter(){
                        Name = "path",
                        Type = ParameterType.STRING
                    }
                }
            };

            Assert.AreEqual(expectedImporterInfo, actualImporterInfo);
        }
    }
}
