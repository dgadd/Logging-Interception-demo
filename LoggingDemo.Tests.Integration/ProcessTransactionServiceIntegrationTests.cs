using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingDemo.Domain;
using LoggingDemo.ServiceLayer;
using NUnit.Framework;

namespace LoggingDemo.Tests.Integration
{
    [TestFixture]
    public class ProcessTransactionServiceIntegrationTests
    {
        [Test]
        public void GetPaymentApprovalAndGenerateTransactionMethod_AccountPassedToConstructor_GeneratesApprovalAndTransaction()
        {
            const int accountId = 12351;
            var sut = new ProcessTransactionService(new FakeAccountRepository(), new FakeTransactionsRepository(),
                                                    new FakePaymentGateway());
            Transaction transaction = sut.GetPaymentApprovalAndGenerateTransaction(accountId);
            Assert.IsTrue(transaction.GrossAmount > 0);
        }

        [Test]
        public void GetPaymentApprovalAndGenerateTransactionMethod_NoInputParams_GeneratesTransaction()
        {
            const int accountId = 12351;
            var sut = new ProcessTransactionService();
            Transaction transaction = sut.GetPaymentApprovalAndGenerateTransaction(accountId);
            Assert.IsTrue(transaction.GrossAmount > 0);
        }
    }
}
