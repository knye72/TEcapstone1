using System;
//using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;

namespace CapstoneTests
{
    [TestClass()]
    public class PurchaseTests
    {
        [TestMethod]
        public void MakeSureUserCanAddToBalanceTests()
        {
            Purchase purchase = new Purchase();
            //arrange

            double actualResult = purchase.AddToBalance(5.00);
            //act

            Assert.AreEqual( 5.00, actualResult);
            //assert
        }

        //public void GiveCorrectChangeOwed()
        //{
        //    Purchase purchase = new Purchase();

        //    string actualResult = purchase.ChangeOwed();

        //    Assert.AreEqual( 0, actualResult);
        //}








    }
   
}
