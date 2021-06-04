using BankWebbApp.Controllers;
using BankWebbApp.Data;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
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
            sut = new WithdrawalController( accountRepositoryMock.Object , transactionRepositoryMock.Object);

        }

        [TestMethod]
        public void Should_not_withdraw_more_than_the_Account_total_amount()
        {
            var viewModel = new NewWithdrawalViewModel
            {
                AccountId = 1,
                Amount = 111,
                Operation = "Withdrawal in cash"

            };
            
            Assert.ThrowsException<InvalidDataException>(() => Withdrawal.Show(viewModel.AccountId));
        }

        //[TestMethod]
        //public void AccountNotFound()
        //{
        //    var viewModel = fixture.Create<NewWithdrawalViewModel>();
        //    viewModel.AccountId = 998877665;
        //    Assert.ThrowsException<InvalidDataException>(() => Withdrawal.Show(viewModel.AccountId));
        //}
    }

    internal class NewWithdrawalViewModel
    {
        public int TransactionId { get; set; }

       
        public int AccountId { get; set; }

       
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
     
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
    }
}
