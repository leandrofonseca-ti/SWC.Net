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
    public class UnitTestUser
    {
        public readonly IUserRepository MockRepository;

        public UnitTestUser()
        {
            Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();

            // create some mock tasks
            clsUser user = new clsUser { USERID = 1, NAME = "User 1" };

            // Return all the tasks
            //mockScheduleRepository.Setup(mr => mr.LoadTasksByPlan(It.IsAny<clsPlan>())).Returns(tasks);
            mockRepository.Setup(mr => mr.ExistUserPhone(It.IsAny<string>())).Returns(user);

 

            this.MockRepository = mockRepository.Object;

        }


        [TestMethod]
        public void Test_ExistUserPhone()
        {
            var result = this.MockRepository.ExistUserPhone("5551653234543");

            Assert.IsNotNull(result); // Test if null

            Assert.IsTrue(result.USERID > 0); // Verify the correct 

        }

        /* TODO:
        [TestMethod]
        public void Test_Get()
        {
            var result = this.MockRepository.Get
        }

        [TestMethod]
        public void Test_GetByEmail()
        {
            var result = this.MockRepository.GetByEmail

                }

        [TestMethod]
        public void Test_GetPhones()
        {
            var result = this.MockRepository.GetPhones
                }

        [TestMethod]
        public void Test_List()
        {
            var result = this.MockRepository.List
                }

        [TestMethod]
        public void Test_ListUsuario()
        {
            var result = this.MockRepository.ListUsuario
                }

        [TestMethod]
        public void Test_Remove()
        {
            var result = this.MockRepository.Remove
                }

        [TestMethod]
        public void Test_Save()
        {
            var result = this.MockRepository.Save
                }

        [TestMethod]
        public void Test_UpdatePasswordMD5()
        {
            var result = this.MockRepository.UpdatePasswordMD5
                }

        [TestMethod]
        public void Test_ValidateUser()
        {
            var result = this.MockRepository.ValidateUser
                }

        [TestMethod]
        public void Test_ValidateUserPhone()
        {
            var result = this.MockRepository.ValidateUserPhone

        }
        */

    }
}
