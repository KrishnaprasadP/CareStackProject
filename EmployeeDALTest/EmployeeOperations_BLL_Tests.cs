using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeOperations;
using EmployeeDAL;
using Moq;
using EmployeeModels;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeDALTest
{
    [TestClass]
    public class EmployeeOperations_BLL_Tests
    {
        EmpOperations empOps;
        Mock<IUnitOfWork> MockUnitofWork;
        Mock<IRepository<EmployeeEntity>> MockEmpRep;
        Mock<IRepository<PermEmployeeEntity>> MockPermEmpRep;

        public EmployeeOperations_BLL_Tests()
        {
            MockUnitofWork = new Mock<IUnitOfWork>();
            MockEmpRep = new Mock<IRepository<EmployeeEntity>>();
            MockPermEmpRep = new Mock<IRepository<PermEmployeeEntity>>();
        }

        [TestMethod]
        public void AddEmployeeTest()
        {
            //Arrange            
            MockUnitofWork.SetupGet(x => x.EmpRepository).Returns(MockEmpRep.Object);
            empOps = new EmpOperations(MockUnitofWork.Object);

            //Act
            var res = empOps.AddEmployee(new Employee());

            //Assert
            MockEmpRep.Verify(x => x.Add(It.IsAny<EmployeeEntity>()), Times.Exactly(1));
            MockUnitofWork.Verify(x => x.SaveChanges(), Times.Exactly(1));
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void AddEmployeeTest_AddFails()
        {
            //Arrange       
            MockEmpRep.Setup(x => x.Add(It.IsAny<EmployeeEntity>())).Throws(new Exception());
            MockUnitofWork.SetupGet(x => x.EmpRepository).Returns(MockEmpRep.Object);
            empOps = new EmpOperations(MockUnitofWork.Object);

            //Act
            var res = empOps.AddEmployee(new Employee());

            //Assert
            MockEmpRep.Verify(x => x.Add(It.IsAny<EmployeeEntity>()), Times.Exactly(1));
            MockUnitofWork.Verify(x => x.SaveChanges(), Times.Exactly(0));
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void AddPermEmployeeTest()
        {
            //Arrange            
            MockUnitofWork.SetupGet(x => x.PermEmpRepository).Returns(MockPermEmpRep.Object);
            empOps = new EmpOperations(MockUnitofWork.Object);

            //Act
            var res = empOps.AddPermEmployee(new PermEmployee());

            //Assert
            MockPermEmpRep.Verify(x => x.Add(It.IsAny<PermEmployeeEntity>()), Times.Exactly(1));
            MockUnitofWork.Verify(x => x.SaveChanges(), Times.Exactly(1));
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void AddPermEmployeeTest_AddFails()
        {
            //Arrange
            MockPermEmpRep.Setup(x => x.Add(It.IsAny<PermEmployeeEntity>())).Throws(new Exception());
            MockUnitofWork.SetupGet(x => x.PermEmpRepository).Returns(MockPermEmpRep.Object);
            empOps = new EmpOperations(MockUnitofWork.Object);

            //Act
            var res = empOps.AddPermEmployee(new PermEmployee());

            //Assert
            MockPermEmpRep.Verify(x => x.Add(It.IsAny<PermEmployeeEntity>()), Times.Exactly(1));
            MockUnitofWork.Verify(x => x.SaveChanges(), Times.Exactly(0));
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void GetPermEmployeeTest_WithoutPay()
        {
            //Arrange
            var permEmpMock = new List<PermEmployeeEntity>
            {
                new PermEmployeeEntity { Id = 1,  HoursWorked =1, Name = "Name1", PayRate = 1 },
                new PermEmployeeEntity { Id = 2,  HoursWorked =2, Name = "Name2", PayRate = 2 }
            };
            MockPermEmpRep.Setup(x => x.Get()).Returns(permEmpMock);
            MockUnitofWork.SetupGet(x => x.PermEmpRepository).Returns(MockPermEmpRep.Object);
            empOps = new EmpOperations(MockUnitofWork.Object);

            //Act
            var res = empOps.GetPermEmployees(false).ToList();
            
            //Assert            
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count, 2);
            Assert.AreEqual(res[0].Id, 1);
            Assert.AreEqual(res[0].Name, "Name1");
            Assert.AreEqual(res[0].HoursWorked, 1);
            Assert.AreEqual(res[0].PayRate, 1);
            Assert.AreEqual(res[0].TotalPay, 0);
        }

        [TestMethod]
        public void GetPermEmployeeTest_WithPay()
        {
            //Arrange
            var permEmpMock = new List<PermEmployeeEntity>
            {
                new PermEmployeeEntity { Id = 1,  HoursWorked =1, Name = "Name1", PayRate = 1 },
                new PermEmployeeEntity { Id = 2,  HoursWorked =2, Name = "Name2", PayRate = 2 }
            };
            MockPermEmpRep.Setup(x => x.Get()).Returns(permEmpMock);
            MockUnitofWork.SetupGet(x => x.PermEmpRepository).Returns(MockPermEmpRep.Object);
            empOps = new EmpOperations(MockUnitofWork.Object);

            //Act
            var res = empOps.GetPermEmployees(true).ToList();

            //Assert            
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count, 2);
            Assert.AreEqual(res[0].Id, 1);
            Assert.AreEqual(res[0].Name, "Name1");
            Assert.AreEqual(res[0].HoursWorked, 1);
            Assert.AreEqual(res[0].PayRate, 1);
            Assert.AreEqual(res[0].TotalPay, 1);
        }

        [TestMethod]
        public void GetTempEmployeeTest_WithoutPay()
        {
            //Arrange
            var empMock = new List<EmployeeEntity>
            {
                new EmployeeEntity { Id = 1,  HoursWorked =1, Name = "Name1", PayRate = 1 },
                new EmployeeEntity { Id = 2,  HoursWorked =2, Name = "Name2", PayRate = 2 }
            };
            MockEmpRep.Setup(x => x.Get()).Returns(empMock);
            MockUnitofWork.SetupGet(x => x.EmpRepository).Returns(MockEmpRep.Object);
            empOps = new EmpOperations(MockUnitofWork.Object);

            //Act
            var res = empOps.GetTempEmployees(false).ToList();

            //Assert            
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count, 2);
            Assert.AreEqual(res[0].Id, 1);
            Assert.AreEqual(res[0].Name, "Name1");
            Assert.AreEqual(res[0].HoursWorked, 1);
            Assert.AreEqual(res[0].PayRate, 1);
            Assert.AreEqual(res[0].TotalPay, 0);
        }

        [TestMethod]
        public void GetTempEmployeeTest_WithPay()
        {
            //Arrange
            var empMock = new List<EmployeeEntity>
            {
                new EmployeeEntity { Id = 1,  HoursWorked =1, Name = "Name1", PayRate = 1 },
                new EmployeeEntity { Id = 2,  HoursWorked =2, Name = "Name2", PayRate = 2 }
            };
            MockEmpRep.Setup(x => x.Get()).Returns(empMock);
            MockUnitofWork.SetupGet(x => x.EmpRepository).Returns(MockEmpRep.Object);
            empOps = new EmpOperations(MockUnitofWork.Object);

            //Act
            var res = empOps.GetTempEmployees(true).ToList();

            //Assert            
            Assert.IsNotNull(res);
            Assert.AreEqual(res.Count, 2);
            Assert.AreEqual(res[0].Id, 1);
            Assert.AreEqual(res[0].Name, "Name1");
            Assert.AreEqual(res[0].HoursWorked, 1);
            Assert.AreEqual(res[0].PayRate, 1);
            Assert.AreEqual(res[0].TotalPay, 0.5);
        }
    }
}
