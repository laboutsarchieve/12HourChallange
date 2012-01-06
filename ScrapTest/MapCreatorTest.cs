using ScapWars.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ScrapTest
{
    
    
    /// <summary>
    ///This is a test class for MapCreatorTest and is intended
    ///to contain all MapCreatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MapCreatorTest
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


        /// <summary>
        ///A test for MapCreator Constructor
        ///</summary>
        [TestMethod()]
        public void MapCreatorConstructorTest()
        {
            MapCreator target = new MapCreator();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CreateMap
        ///</summary>
        [TestMethod()]
        public void CreateMapTest()
        {
            MapCreator target = new MapCreator(); // TODO: Initialize to an appropriate value
            Map expected = null; // TODO: Initialize to an appropriate value
            Map actual;
            actual = target.CreateMap();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateBasics
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ScrapWars.exe")]
        public void CreateBasicsTest()
        {
            MapCreator_Accessor target = new MapCreator_Accessor(); // TODO: Initialize to an appropriate value
            Map_Accessor newMap = null; // TODO: Initialize to an appropriate value
            target.CreateBasics(newMap);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
