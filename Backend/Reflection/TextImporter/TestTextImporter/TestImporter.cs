using CustomBugImportation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TextImporter;

namespace TestTextImporter
{
    [TestClass]
    public class TestImporter
    {
        private Parameter baseDirectoy;
        private Importer textImporter;

        [TestInitialize]
        public void CreateImporterInstance()
        {
            textImporter = new Importer();
            baseDirectoy = new Parameter()
            {
                Name = "Folder path",
                Type = ParameterType.STRING,
                Value = "../../../TestFiles/"
            };
        }

        [TestMethod]
        public void GetImporterInfo()
        {
            ImporterInfo actualImporterInfo = textImporter.GetImporterInfo();

            var expectedImporterInfo = new ImporterInfo
            {
                ImporterName = "Positional Text Importer",
                Params = new List<Parameter>
                {
                    new Parameter(){
                    Name = "Folder path",
                    Type = ParameterType.STRING,
                },
                new Parameter(){
                    Name = "File Name",
                    Type = ParameterType.INTEGER,
                }
                }
            };

            Assert.AreEqual(expectedImporterInfo, actualImporterInfo);
        }



        [TestMethod]
        public void ImportOneBug()
        {
            List<Parameter> parameters = new List<Parameter>()
            {
                baseDirectoy,
                new Parameter(){
                    Name = "File Name",
                    Type = ParameterType.INTEGER,
                    Value = "1"
                }
            };
            List<ImportedBug> actualBugs = textImporter.ImportBugs(parameters);

            List<ImportedBug> expectedBugs = new List<ImportedBug>()
            {
                new ImportedBug(){
                Name = "Error en el envío de correo",
                Description = "El error se produce cuando el usuario no tiene un correo asignado.",
                Version = "1.0",
                IsActive = true,
                ProjectName = "Nombre del Proyecto 1",
                Time = 23
                }
            };
            Assert.IsTrue(actualBugs.SequenceEqual(expectedBugs));
        }

        [TestMethod]
        public void ImportTwoBug()
        {
            List<Parameter> parameters = new List<Parameter>()
            {
                baseDirectoy,
                new Parameter(){
                    Name = "File Name",
                    Type = ParameterType.INTEGER,
                    Value = "2"
                }
            };

            List<ImportedBug> actualBugs = textImporter.ImportBugs(parameters);
            List<ImportedBug> expectedBugs = new List<ImportedBug>()
            {
                new ImportedBug(){
                Name = "Error en el envío de correo",
                Description = "El error se produce cuando el usuario no tiene un correo asignado.",
                Version = "1.0",
                IsActive = true,
                ProjectName = "Nombre del Proyecto 1",
                Time = 2
                },
                new ImportedBug(){
                Name = "Error en el envío de correo2",
                Description = "El error se produce cuando el usuario no tiene un correo asignado 2.",
                Version = "1.0",
                IsActive = true,
                ProjectName = "Nombre del Proyecto 2",
                Time = 9999
                }
            };
            Assert.IsTrue(actualBugs.SequenceEqual(expectedBugs));
        }

        [TestMethod]
        public void ImportNoBug()
        {
            List<Parameter> parameters = new List<Parameter>()
            {
                baseDirectoy,
                new Parameter(){
                    Name = "File Name",
                    Type = ParameterType.INTEGER,
                    Value = "0"
                }
            };

            Assert.ThrowsException<CustomImporterException>(() =>
                            textImporter.ImportBugs(parameters)
            );
        }

        [TestMethod]
        public void NoFileImport()
        {
            List<Parameter> parameters = new List<Parameter>()
            {
                baseDirectoy,
                new Parameter(){
                    Name = "File Name",
                    Type = ParameterType.INTEGER,
                    Value = "1232131"
                }
            };
            Assert.ThrowsException<CustomImporterException>(() =>
                            textImporter.ImportBugs(parameters)
            );
        }

        [TestMethod]
        public void FailedFileImport()
        {
            List<Parameter> parameters = new List<Parameter>()
            {
                baseDirectoy,
                new Parameter(){
                    Name = "File Name",
                    Type = ParameterType.INTEGER,
                    Value = "-1"
                }
            };
            Assert.ThrowsException<CustomImporterException>(() =>
                            textImporter.ImportBugs(parameters)
            );
        }

        [TestMethod]
        public void ParamNotNumber()
        {
            List<Parameter> parameters = new List<Parameter>()
            {
                baseDirectoy,
                new Parameter(){
                    Name = "File Name",
                    Type = ParameterType.INTEGER,
                    Value = "Bug"
                }
            };
            Assert.ThrowsException<CustomImporterException>(() =>
                            textImporter.ImportBugs(parameters)
            );
        }
    }
}
