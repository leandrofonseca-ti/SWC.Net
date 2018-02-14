using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWC.Data.Interface;
using Moq;
using SWC.Data.Service;
using SWC.Data.Entity;
using MySql.Data.MySqlClient;

namespace SWC.UnitTest
{
    [TestClass]
    public class UnitTestSchedule
    {
        public readonly IScheduleRepository MockScheduleRepository;

        public UnitTestSchedule()
        {
            Mock<IScheduleRepository> mockScheduleRepository = new Mock<IScheduleRepository>();

            IList<clsTask> tasks = new List<clsTask>()
            {
                new clsTask{ ID = 1, NAME = "Create Project"},
                new clsTask{ ID = 2, NAME= "Update Task"},
                new clsTask{  ID = 3, NAME= "Delete Schedule" }
            };

            mockScheduleRepository.Setup(mr => mr.LoadTasksByPlan(It.IsAny<int>())).Returns(tasks);

            this.MockScheduleRepository = mockScheduleRepository.Object;

        }

        [TestMethod]
        public void Test_LoadTasksByPlan()
        {
            var result = this.MockScheduleRepository.LoadTasksByPlan(10);

            Assert.IsNotNull(result); // Test if null

            Assert.AreEqual(3, result.Count); // Verify the correct Number
        }


        [TestMethod]
        public void Test_LoadTasksByItemId()
        {
            var result = this.MockScheduleRepository.LoadTasksByItemId(10);            
        }


        /* TODO: 
        [TestMethod]
        public void Test_GetScheduleItem()
        {
            var result = this.MockScheduleRepository.
        }

        [TestMethod]
        public void Test_GetScheduleItem()
        {
            var result = this.MockScheduleRepository.GetScheduleItem
        }

        [TestMethod]
        public void Test_InsertSchedulePaymentStaff()
        {
            var result = this.MockScheduleRepository.InsertSchedulePaymentStaff
        }

        [TestMethod]
        public void Test_LoadExtraTasks()
        {
            var result = this.MockScheduleRepository.LoadExtraTasks
        }

        [TestMethod]
        public void Test_LoadImagesByItemId()
        {
            var result = this.MockScheduleRepository.LoadImagesByItemId
        }


        [TestMethod]
        public void Test_UpdateScheduleStaff()
        {
            var result = this.MockScheduleRepository.UpdateScheduleStaff
        }


        [TestMethod]
        public void Test_LoadScheduleByUser()
        {
            var result = this.MockScheduleRepository.LoadScheduleByUser
        }
        */
    }
}
