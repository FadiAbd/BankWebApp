using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankAppTests
{
    [TestClass]
    public class WithdrawalControllerTests
    {
        private WithdrawalController sut = new WithdrawalController();
        [TestMethod]
        public void Should-not-withdraw-more-than-the-Account-totalAmount  ()
        {
            //
        }
    }
}
