using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWC.Data.Interface;
using Moq;
using SWC.Data.Service;
using SWC.Data.Entity;
using MySql.Data.MySqlClient;
using SWC.Data.Helper;

namespace SWC.UnitTest
{
    [TestClass]
    public class UnitTestHelper
    {
        [TestMethod]
        public void Test_Check_EncryptDecrypt()
        {
            ICryptographyRepository repository = new Cryptography();

            var content = "COMPARE TEXT";

            var resultEncrypt = repository.Encrypt(content);

            var result = repository.Decrypt(resultEncrypt);

            Assert.AreEqual(content, result); 

        }


    }
}
