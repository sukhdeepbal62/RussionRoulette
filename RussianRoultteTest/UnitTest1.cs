using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RussionRoulette;

namespace RussianRoultteTest
{
    [TestClass]
    public class UnitTest1
    {
        //creating an instance of the class
        ClassGame MyClassRoulette = new ClassGame();

//Test function to test if the current chamber is set to 1
        [TestMethod]
        public void TestMethod1()
        {
            //calling method from the instance
            int testresultCurrentChamber = MyClassRoulette.CurrentChamberID;
           //checking the return value of the function
            Assert.AreEqual(testresultCurrentChamber < 1, testresultCurrentChamber  >1);
        }

        // test function to test if bullet is between 1 and 6
        [TestMethod]
        public void TestMethod2()
        {
            int testresult = MyClassRoulette.SecretChamberID;
            //checking return value
            Assert.IsTrue(testresult < 7 && testresult> 0);
        }

    }
}
