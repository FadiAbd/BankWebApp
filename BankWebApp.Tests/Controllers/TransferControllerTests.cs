using BankWebbApp.Controllers;
using BankWebbApp.Data;
using BankWebbApp.Repository;
using BankWebbApp.Services;
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
    class TransferControllerTests : BaseTest
    {

        public class BaseTest
        {
            protected AutoFixture.Fixture fixture = new AutoFixture.Fixture();
            private ApplicationDbContext ctx;

        }

        [TestClass]
        public class TransferControllerTests : BaseTest
        {
            private TransferController transfer;
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
                transfer = new TransferController( accountRepositoryMock.Object ,transactionRepositoryMock.Object);


            }

            //[TestMethod]
            //public void AccountIdNotFound()
            //{
            //    var viewModel = fixture.Create<NewTransferViewModel>();
            //    viewModel.AccountId = 63336631;
            //    Assert.ThrowsException<InvalidDataException>(() => transfer.Show(viewModel.AccountId));
            //}

        }
    }
}
