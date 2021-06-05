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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankWebApp.Tests
{
    
    

       

        [TestClass]
        public class TransferControllerTests 
        {
            private TransferController sut;
            private IMock<ITransactionRepository> transactionRepositoryMock;
            private IMock<IAccountRepository> accountRepositoryMock;
            private ApplicationDbContext ctx;

            public TransferControllerTests()
            {
                transactionRepositoryMock = new Mock<ITransactionRepository>();
                accountRepositoryMock = new Mock<IAccountRepository>();

                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
                    .Options;
                ctx = new ApplicationDbContext(options);
                sut = new TransferController( accountRepositoryMock.Object ,transactionRepositoryMock.Object);


            }

        [TestMethod]
        public void When_Transfered_Amount_Is_Not_Positiv()
        {
            var accountTest = new Account
            {
                AccountId = 132,
                Balance = 4000,
                Created = DateTime.Now,
                Frequency = "M"
            };
            var accountTest2 = new Account
            {
                AccountId = 222,
                Balance = 500,
                Created = DateTime.Now,
                Frequency = "M"
            };

            accountRepositoryMock.Object.AddAccount(accountTest);
            accountRepositoryMock.Object.AddAccount(accountTest2);
            accountRepositoryMock.Object.Save();

            var newTransfer = new NewTransferViewModel
            {
                AccountId = 132,
                AccountReceiversId = 222,
                Amount = -330
            };

            sut.NewTransfer(newTransfer);
            Assert.IsFalse(sut.ViewData.ModelState.IsValid);

        }

        [TestMethod]
        public void When_Transfering_more_fund_than_the_Account_Balance()
        {
            var account = new Account
            {
                AccountId = 1,
                Balance = 10,
                Created = DateTime.Now,
                Frequency = "M"
            };
            var account2 = new Account
            {
                AccountId = 4,
                Balance = 100,
                Created = DateTime.Now,
                Frequency = "M"
            };

            accountRepositoryMock.Object.AddAccount(account);
            accountRepositoryMock.Object.AddAccount(account2);
            accountRepositoryMock.Object.Save();

            var newTransfer = new NewTransferViewModel
            {
                AccountId = 1,
                AccountReceiversId = 4,
                Amount = 50
            };

            sut.NewTransfer(newTransfer);

            Assert.IsFalse(sut.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void When_Account_Is_Not_Found()
        {
            var a = new Account
            {
                AccountId = 4,
                Balance = 1000,
                Created = DateTime.Now,
                Frequency = "M"
            };
            var a2 = new Account
            {
                AccountId = 7,
                Balance = 5000,
                Created = DateTime.Now,
                Frequency = "M"
            };

            accountRepositoryMock.Object.AddAccount(a);
            accountRepositoryMock.Object.AddAccount(a2);
            accountRepositoryMock.Object.Save();

            var newTransfer = new NewTransferViewModel
            {
                AccountId = 3,
                AccountReceiversId = 7,
                Amount = 500
            };

            sut.NewTransfer(newTransfer);

            Assert.IsFalse(sut.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void When_A_New_Transaction_Is_Correct()
        {
            var account = new Account
            {
                AccountId = 30,
                Balance = 10000,
                Created = DateTime.Now,
                Frequency = "M"
            };
            var accountMot = new Account
            {
                AccountId = 100,
                Balance = 1000,
                Created = DateTime.Now,
                Frequency = "M"
            };

            accountRepositoryMock.Object.AddAccount(account);
            accountRepositoryMock.Object.DeleteAccount(accountMot);
            accountRepositoryMock.Object.Save();
            var viewModel = new NewTransferViewModel
            {
                AccountId = 30,
                AccountReceiversId = 100,
                Amount = 500
            };

            var result = sut.NewTransfer(viewModel);
            var resultAction = result as ActionResult;
            Assert.IsInstanceOfType(resultAction, typeof(ViewResult));
        }

        //[TestMethod]
        //public void AccountIdNotFound()
        //{
        //    var viewModel = fixture.Create<NewTransferViewModel>();
        //    viewModel.AccountId = 99999999;
        //    Assert.ThrowsException<InvalidDataException>(() => transfer.Show(viewModel.AccountId));
        //}

    }
    
}
