﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment1.Controllers;
using Moq;
using Assignment1.Models;
using System.Linq;
using System.Web.Mvc;

namespace Assignment1.Tests.Controllers
{

    [TestClass]
    public class Table1ControllerTest
    {
        Table1Controller controller;
        Mock<IMockTable1Repository> mock;
        List<Table1> Table;

        [TestInitialize]

        public void TestInitialize()
        {

            // runs automatically before each unit test
            // instantiate the mock object
            mock = new Mock<IMockTable1Repository>();

            // instantiate the mock table1 data
            Table = new List<Table1>
            {

                new Table1 { customerID =1, companyname ="Table 1"},
                new Table1 { customerID =2, companyname ="Table 2"},
                new Table1 { customerID =3, companyname ="Table 3"}

            };
            // bind the  data to the mock
            mock.Setup(m => m.Table1).Returns(Table.AsQueryable());
            // initialize the controller and inject the dependency
            controller = new Table1Controller(mock.Object);

        }

        [TestMethod]

        public void IndexViewLoads()
        {


            //act 
            var actual = controller.Index();

            // assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]

        public void IndexLoadsTable1()
        {


            //act - cast actionresult to viewresult, then model to list of  table1
            var actual = (List<Table1>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(Table, actual);
        }

        [TestMethod]

        public void DetailsValidTableId()
        {


            //act - valid id's: 1/2/3

            var actual = (Table1)((ViewResult)controller.Details(1)).Model;


            // assert
            Assert.AreEqual(Table[0], actual);
        }

        [TestMethod]

        public void DetailsInvalidTableId()
        {


            //act - expect the index view to load if no matching table

            var actual = (ViewResult)controller.Details(4);


            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]

        public void DetailsnoTableId()
        {


            //act

            var actual = (ViewResult)controller.Details(null);


            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Edit
        [TestMethod]

        public void EditGetValidId()
        {

            // act
            var actual = ((ViewResult)controller.Edit(1)).Model;

            //assert
            Assert.AreEqual(Table[0], actual);


}
        [TestMethod]

        public void EditGetInvalidId()
        {

            // act
            var actual = (ViewResult)controller.Edit(4);

            //assert
            Assert.AreEqual("Error", actual.ViewName);


        }

        [TestMethod]

        public void EditGetNoId()
        {
            int? id = null;

            // act
            var actual = (ViewResult)controller.Edit(id);

            //assert
            Assert.AreEqual("Error", actual.ViewName);


        }

        // POSt: Edit
        [TestMethod]

        public void EditPostValid()
        {

            // act
            var actual = (RedirectToRouteResult)controller.Edit(Table[0]);

            //assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);


        }

      
        [TestMethod]

        public void EditPostInvalid()
        {
            // arrange - manually set model state to invalid
            controller.ModelState.AddModelError("Key", "update error");

            // act
            var actual = (ViewResult)controller.Edit(Table[0]);

            //assert
            Assert.AreEqual("Edit", actual.ViewName);


        }

        // create
        [TestMethod]

        public void CreateViewLoads()
        {

            // act
            var actual = (ViewResult)controller.Create();

            //assert
            Assert.AreEqual("Create", actual.ViewName);


        }

        [TestMethod]

        public void CreateValid()
        {

            // arrange
            Table1 b = new Table1
            {

                Name = "New table"


            };

            // act
            var actual = (RedirectToRouteResult)controller.Create(b);

            //assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);


        }

        [TestMethod]

        public void CreateInvalid()
        {
            // arrange
            Table1 b = new Table1
            {

                Name = "New table"


            };

            controller.ModelState.AddModelError("Key", "create error");

            // act
            var actual = (ViewResult)controller.Create();

            //assert
            Assert.AreEqual("Create", actual.ViewName);


        }

        // Delete
        [TestMethod]

        public void DeleteGetValidId()
        {

            // act
            var actual = ((ViewResult)controller.Delete(1)).Model;

            //assert
            Assert.AreEqual(Table[0], actual);


        }

        [TestMethod]

        public void DeleteGetInValidId()
        {

            // act
            var actual = (ViewResult)controller.Delete(4);

            //assert
            Assert.AreEqual("Error", actual.ViewName);


        }

        [TestMethod]

        public void DeleteGetNoId()
        {

            // act
            var actual = (ViewResult)controller.Delete(null);

            //assert
            Assert.AreEqual("Error", actual.ViewName);


        }

        [TestMethod]

        public void DeletePostValid()
        {

            // act
            var actual = (RedirectToRouteResult)controller.DeleteConfirmed(1);

            //assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);


        }

    }
}
