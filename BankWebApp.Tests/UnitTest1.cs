using BankWebbApp.Controllers;
using BankWebbApp.Data;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;

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
        private ApplicationDbContext ctx;

        public WithdrawalControllerTests()
        {
            transactionRepositoryMock = new Mock<ITransactionRepository>();
            accountRepositoryMock = new Mock<IAccountRepository>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .EnableSensitiveDataLogging()
                            .Options;
            ctx = new ApplicationDbContext(options);
            sut = new WithdrawalController(transactionRepositoryMock.Object,accountRepositoryMock.Object);

        }

        [TestMethod]
        public void AccountNotFound()
        {
            var viewModel = fixture.Create<NewWithdrawalViewModel>();
            viewModel.AccountId = 998877665;
            Assert.ThrowsException<InvalidDataException>(() => Withdrawal.Show(viewModel.AccountId));
        }
    }
}
