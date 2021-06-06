using BankWebbApp.Controllers;
using BankWebbApp.Data;
using BankWebbApp.Models;
using BankWebbApp.Repository;
using BankWebbApp.Services;
using BankWebbApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;

namespace BankWebApp.Tests
{
    //public class BaseTest
    //{
    //    protected AutoFixture.Fixture fixture = new AutoFixture.Fixture();
    //    private ApplicationDbContext ctx;

    //}
        [TestClass]
    public class WithdrawalControllerTests 
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
            var account = new Account
            {
                AccountId = 1,
                Balance = 20,
                Created = DateTime.Now,
                Frequency = "M"
            };
            accountRepositoryMock.Object.AddAccount(account);
            accountRepositoryMock.Object.Save();
            WithdrawalViewModel tom = new WithdrawalViewModel
            {
                AccountId = 1,
                Amount = 40
            };
            sut.NewWithdrawal(tom);
            Assert.IsFalse(sut.ViewData.ModelState.IsValid);

        }
        [TestMethod]
        public void When_Account_Is_Not_Found()
        {
            var a = new Account
            {
                AccountId = 8,
                Balance = 2200,
                Created = DateTime.Now,
                Frequency = "M"
            };
            accountRepositoryMock.Object.AddAccount(a);
            accountRepositoryMock.Object.Save();
            var viewModel = new WithdrawalViewModel
            {
                AccountId = 10,
                Amount = 100
            };

            sut.NewWithdrawal(viewModel);
            Assert.IsFalse(sut.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void When_Amount_Is_Negative()
        {
            var account = new Account
            {
                AccountId = 1,
                Balance = 100,
                Created = DateTime.Now,
                Frequency = "M"
            };
            accountRepositoryMock.Object.AddAccount(account);
            accountRepositoryMock.Object.Save();
            var viewModel = new WithdrawalViewModel
            {
                AccountId = 1,
                Amount = -20
            };
            sut.NewWithdrawal(viewModel);
            Assert.IsFalse(sut.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void When_The_Withdrawal_Is_Right()
        {
            var account = new Account
            {
                AccountId = 9,
                Balance = 1000,
                Created = DateTime.Now,
                Frequency = "M"
            };

            accountRepositoryMock.Object.AddAccount(account);
            accountRepositoryMock.Object.Save();
            var viewModel = new WithdrawalViewModel
            {
                AccountId = 9,
                Amount = 500
            };

            var result = sut.NewWithdrawal(viewModel);
            var resultAction = result as ActionResult;
            Assert.IsInstanceOfType(resultAction, typeof(ViewResult));
        }

       
    }

   
}
