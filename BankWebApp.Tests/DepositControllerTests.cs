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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankWebApp.Tests
{
    [TestClass]
    public class DepositControllerTests
    {
        private DepositController  sut;
        private Mock<ITransactionRepository> transactionRepositoryMock;
        private Mock<IAccountRepository> accountRepositoryMock;
        private ApplicationDbContext ctx;

        public DepositControllerTests()
        {
            transactionRepositoryMock = new Mock<ITransactionRepository>();
            accountRepositoryMock = new Mock<IAccountRepository>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .EnableSensitiveDataLogging()
                            .Options;
            ctx = new ApplicationDbContext(options);
            sut = new DepositController(accountRepositoryMock.Object, transactionRepositoryMock.Object);

        }

        [TestMethod]
        public void When_The_Deposit_Is_Right()
        {
            var account = new Account
            {
                AccountId = 7,
                Balance = 20,
                Created = DateTime.Now,
                Frequency = "M"
            };
            accountRepositoryMock.Object.AddAccount(account);
            accountRepositoryMock.Object.Save();
            var viewModel = new DepositViewModel
            {
                AccountId = 7,
                Amount = 100
            };

            var result = sut.NewDeposit(viewModel);
            var resultAction = result as ActionResult;
            Assert.IsInstanceOfType(resultAction, typeof(ViewResult));
        }

        [TestMethod]
        public void When_Deposit_Amount_Is_Negative()
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
            var viewModel = new DepositViewModel
            {
                AccountId = 1,
                Amount = -10
            };

            sut.NewDeposit(viewModel);
            Assert.IsFalse(sut.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void When_Account_Is_Not_Found()
        {
            var account = new Account
            {
                AccountId = 5,
                Balance = 200,
                Created = DateTime.Now,
                Frequency = "M"
            };
            accountRepositoryMock.Object.AddAccount(account);
            accountRepositoryMock.Object.Save();
            var viewModel = new DepositViewModel
            {
                AccountId = 10,
                Amount = 10
            };

            sut.NewDeposit(viewModel);
            Assert.IsFalse(sut.ViewData.ModelState.IsValid);
        }
    }
}
