using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingDemo.Domain;
using LoggingDemo.Domain.DataTransferObjects;
using LoggingDemo.Repository;
using LoggingDemo.ServiceLayer;
using NUnit.Framework;
using Rhino.Mocks;

namespace LoggingDemo.Tests.Unit
{
    [TestFixture]
    public class ProcessTransactionServiceTests
    {
        private MockRepository _mockRepository;
        private IAccountRepository _accountRepository;
        private ITransactionsRepository _transactionsRepository;
        private IPaymentGateway _paymentGateway;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _accountRepository = _mockRepository.StrictMock<IAccountRepository>();
            _transactionsRepository = _mockRepository.StrictMock<ITransactionsRepository>();
            _paymentGateway = _mockRepository.StrictMock<IPaymentGateway>();
        }

        [Test]
        public void GetPaymentApprovalAndGenerateTransactionsMethod_AccountPassedToConstructor_GeneratesApprovalAndTransaction()
        {
            // declare constants
            const decimal totalAmount = 35.27M;
            var shoppingCart = new ShoppingCart
                                   {
                                       Id = 12531515,
                                       Total = totalAmount,
                                       Account = new Account
                                                     {
                                                         CreditCardInfo = new CreditCardInfo()
                                                     }
                                   };

            var paymentGatewayResultDto = new PaymentGatewayResultDTO
                                              {
                                                  ApprovedTotal = totalAmount
                                            };

            var transaction = new Transaction
                                  {
                                      GrossAmount = totalAmount
                                  };

            // set expectations
            Expect.Call(_accountRepository.GetShoppingCart()).Return(shoppingCart);
            Expect.Call(_paymentGateway.ApprovePayment(shoppingCart)).Return(paymentGatewayResultDto);
            _transactionsRepository.Save(transaction);

            _mockRepository.ReplayAll();

            var sut = new ProcessTransactionService(_accountRepository, _transactionsRepository, _paymentGateway);
            sut.GetPaymentApprovalAndGenerateTransactions();

            _mockRepository.VerifyAll();

            Assert.AreEqual(shoppingCart.Total, paymentGatewayResultDto.ApprovedTotal);
        }

    }
}
