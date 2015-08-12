using SqlPasswordUpgrader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SqlPasswordUpgraderTests
{
    
    
    /// <summary>
    ///This is a test class for PasswordCheckTest and is intended
    ///to contain all PasswordCheckTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PasswordCheckTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        [DeploymentItem("SqlPasswordUpgrader.exe")]
        public void PasswordContainingOnlyLettersNotComplexEnough()
        {
            PasswordCheck_Accessor target = new PasswordCheck_Accessor();

            string password = "OhGreatAUnitTest";
            bool expected = false;

            bool actual = target.ComplexEnough(password);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DeploymentItem("SqlPasswordUpgrader.exe")]
        public void PasswordContainingLowercaseAndNumbersNotComplexEnough()
        {
            PasswordCheck_Accessor target = new PasswordCheck_Accessor();

            string password = "jumpover53goats";
            bool expected = false;

            bool actual = target.ComplexEnough(password);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DeploymentItem("SqlPasswordUpgrader.exe")]
        public void PasswordContainingUppercaseLowercaseAndNumberIsValid()
        {
            PasswordCheck_Accessor target = new PasswordCheck_Accessor();

            string password = "The3rdBiggestParty";
            bool expected = true;

            bool actual = target.ComplexEnough(password);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DeploymentItem("SqlPasswordUpgrader.exe")]
        public void PasswordContainingUppercaseLowercaseAndSymbolsIsValid()
        {
            PasswordCheck_Accessor target = new PasswordCheck_Accessor();

            string password = "Cat#DogWoof";
            bool expected = true;

            bool actual = target.ComplexEnough(password);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DeploymentItem("SqlPasswordUpgrader.exe")]
        public void PasswordContainingAllSortsIsValid()
        {
            PasswordCheck_Accessor target = new PasswordCheck_Accessor();

            string password = "MegaStrong!Password087&LetMeTellYou";
            bool expected = true;

            bool actual = target.ComplexEnough(password);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LongEnough
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SqlPasswordUpgrader.exe")]
        public void LongEnoughTest()
        {
            PasswordCheck_Accessor target = new PasswordCheck_Accessor(); // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.LongEnough(password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CheckAndFixLength
        ///</summary>
        [TestMethod()]
        public void CheckAndFixLengthTest()
        {
            PasswordCheck target = new PasswordCheck(); // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.CheckAndFixLength(password, username);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

    }
}
