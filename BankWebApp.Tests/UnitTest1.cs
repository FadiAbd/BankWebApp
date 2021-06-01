using BankWebbApp.Controllers;
using BankWebbApp.Data;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankWebApp.Tests
{
    public class BaseTest
    {
        protected AutoFixture.Fixture fixture = new AutoFixture.Fixture();
        private ApplicationDbContext ctx;

    }
        [TestClass]
    public class WithdrawalControllerTests : BaseTest
    {

        private WithdrawalController sut;
        private Mock<ITransactionRepository> transactionRepositoryMock;
        private Mock<IAccountRepository> accountRepositoryMock;
        private KrisInfo info;
        private ApplicationDbContext ctx;

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
