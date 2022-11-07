using BusinessLogic;
using BusinessLogicInterfaces;
using Domain;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoryInterfaces;
using System;
using TechTalk.SpecFlow;
using WebApi.Controllers;

namespace SpecflowTests.Specs.Steps
{
    [Binding]
    public sealed class ClasificacionBugsSteps
    {

        public string name { get; set; }
        public string description { get; set; }
        public string comments { get; set; }
        public string value { get; set; }


        private int resultCode;

        [Given(@"the name is usuario no unico")]
        public void GivenTheNameIsUsuarioNoUnico()
        {
            name = "usuario no unico";
        }
        
        [Given(@"the description is acepta más de un usuario con el mismo nombre")]
        public void GivenTheDescriptionIsAceptaMasDeUnUsuarioConElMismoNombre()
        {
            description = "acepta más de un usuario con el mismo nombre";
        }
        
        [Given(@"the comments is solo esta único el id")]
        public void GivenTheCommentsIsSoloEstaUnicoElId()
        {
            comments = "solo esta único el id";
        }
        
        [Given(@"the value is Alta")]
        public void GivenTheValueIsAlta()
        {
            value = "Alta";
        }


        [Given(@"the name is vacio")]
        public void GivenTheNameIsVacio()
        {
            name = "";
        }

        [Given(@"the name is usuario repetido")]
        public void GivenTheNameIsUsuarioRepetido()
        {
            name = "usuario repetido";
        }

        [Given(@"the description is vacia")]
        public void GivenTheDescriptionIsVacia()
        {
            description = "";
        }

        [Given(@"the comments is vacio")]
        public void GivenTheCommentsIsVacio()
        {
            comments = "";
        }


        [Given(@"the value is Holaaa")]
        public void GivenTheValueIsHolaaa()
        {
            value = "Holaaa";
        }


        [When(@"Click Classification button")]
        public void WhenClickClassificationButton()
        {
            BugTypeDTO prioridad = new BugTypeDTO();
            prioridad.Name = name;
            prioridad.Description = description;
            prioridad.Comments = comments;
            prioridad.Value = value;

            Bug bugABuscar = new Bug();
            bugABuscar.Id = 1;
            bugABuscar.Name = "bug de prueba";

            Bug bugActualizado = new Bug();
            bugActualizado = bugABuscar;
            bugActualizado.Priority = prioridad.ConvertToDomain();

            var repositoryMock = new Mock<IBugDataAccess>(MockBehavior.Strict);
            repositoryMock.Setup(b => b.GetById(1)).Returns(bugABuscar);
            repositoryMock.Setup(b => b.Update(1,bugActualizado)).Returns(bugABuscar);

            var bugLogic = new BugBusinessLogic(repositoryMock.Object);
            BugController bugController = new BugController(bugLogic);

            resultCode = 200;

            try
            {
                bugController.ClassifyPriority(1, prioridad);
            }
            catch (ValidationException e)
            {
                resultCode = 400;
            }

        }
        
        [Then(@"Expect Code (.*)")]
        public void ThenExpectCode(int p0)
        {
            var expectedCode = p0;

            Assert.AreEqual(expectedCode, resultCode);
        }
    }
}
