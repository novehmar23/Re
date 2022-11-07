using BugParser;
using Domain;
using Domain.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml;

namespace TestBugParser
{
    [TestClass]
    public class TestXMLBugParser
    {
        const string baseDirectory = "../../../TestFiles/XMLTestFiles/";
        private BugParserXML bugParser;
        [TestInitialize]
        public void CreateBugParserInstance()
        {
            bugParser = new BugParserXML();
        }
        [TestMethod]
        public void ImportOneBug()
        {
            string fullPath = baseDirectory + "OneBug.xml";
            List<Bug> actualBugs = bugParser.GetBugs(fullPath);

            List<Bug> expectedBugs = new List<Bug>()
            {
                new Bug(){
                Name = "Error en el envío de correo",
                Description = "El error se produce cuando el usuario no tiene un correo asignado",
                Version = "1.0",
                IsActive = true,
                CompletedBy = null,
                ProjectName = "Nombre del Proyecto"
                }
            };

            CollectionAssert.AreEqual(expectedBugs, actualBugs, new BugComparer());
        }


        [TestMethod]
        public void ImportTwoBugs()
        {
            string fullPath = baseDirectory + "TwoBugs.xml";
            List<Bug> actualBugs = bugParser.GetBugs(fullPath);

            List<Bug> expectedBugs = new List<Bug>()
            {
                new Bug(){
                Name = "Error en el envío de correo",
                Description = "El error se produce cuando el usuario no tiene un correo asignado",
                Version = "1.0",
                IsActive = true,
                CompletedBy = null,
                ProjectName = "Nombre del Proyecto"
                },
                new Bug()
                {
                 Name = "Error en el envío de correo 2",
                Description = "El error se produce cuando el usuario no tiene un correo asignado 2",
                Version = "1.0",
                IsActive = true,
                CompletedBy = null,
                ProjectName = "Nombre del Proyecto",
                }
            };

            CollectionAssert.AreEqual(expectedBugs, actualBugs, new BugComparer());
        }

        [TestMethod]
        public void ImportThreeBugs()
        {
            string fullPath = baseDirectory + "ThreeBugs.xml";
            List<Bug> actualBugs = bugParser.GetBugs(fullPath);

            List<Bug> expectedBugs = new List<Bug>()
            {
                new Bug(){
                Name = "Error en el envío de correo",
                Description = "El error se produce cuando el usuario no tiene un correo asignado",
                Version = "1.0",
                IsActive = true,
                CompletedBy = null,
                ProjectName = "Nombre del Proyecto"
                },
                new Bug()
                {
                 Name = "Error en el envío de correo 2",
                Description = "El error se produce cuando el usuario no tiene un correo asignado 2",
                Version = "1.0",
                IsActive = true,
                CompletedBy = null,
                ProjectName = "Nombre del Proyecto",
                },
                new Bug()
                {
                 Name = "Error en el envío de correo 3",
                Description = "El error se produce cuando el usuario no tiene un correo asignado 3",
                Version = "1.0",
                IsActive = true,
                CompletedBy = null,
                ProjectName = "Nombre del Proyecto",
                }
            };

            CollectionAssert.AreEqual(expectedBugs, actualBugs, new BugComparer());
        }

        [TestMethod]
        public void ImportNoBugs()
        {
            string fullPath = baseDirectory + "NoBugs.xml";
            List<Bug> actualBugs = bugParser.GetBugs(fullPath);


            Assert.IsTrue(actualBugs.Count == 0);

        }

        [TestMethod]
        public void InvalidXML()
        {
            string fullPath = baseDirectory + "InvalidXML.xml";
            Assert.ThrowsException<XmlException>(() => bugParser.GetBugs(fullPath));
        }

        [TestMethod]
        public void InvalidBugs()
        {
            string fullPath = baseDirectory + "InvalidBugs.xml";
            Assert.ThrowsException<XmlException>(() => bugParser.GetBugs(fullPath));

        }

        [TestMethod]
        public void BugParser()
        {
            ImportCompany xml = ImportCompany.XML;
            ParserFactory parser = new ParserFactory();
            IBugParser actual = parser.GetBugParser(xml);
            Assert.IsTrue(actual is BugParserXML);

        }

        [TestMethod]
        public void BugParserException()
        {
            try
            {
                ImportCompany wrongFormat = ImportCompany.WrongFormat;
                ParserFactory parser = new ParserFactory();
                IBugParser actual = parser.GetBugParser(wrongFormat);
            }
            catch (NotImplementedException e)
            {
                string messageExpected = "Bug parsers for this company not available";
                Assert.AreEqual(e.Message, messageExpected);
            }
        }

    }

}