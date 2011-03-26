using System;
using LoggingDemo.Domain;
using LoggingDemo.Repository;

namespace LoggingDemo.ServiceLayer
{
    public class ProcessTransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IPaymentGateway _paymentGateway;

        public ProcessTransactionService(IAccountRepository accountRepository, ITransactionsRepository transactionsRepository, IPaymentGateway paymentGateway)
        {
            _accountRepository = accountRepository;
            _transactionsRepository = transactionsRepository;
            _paymentGateway = paymentGateway;
        }

        public void GetPaymentApprovalAndGenerateTransactions()
        {
            var shoppingCart = _accountRepository.GetShoppingCart();
            var paymentGatewayResultDto = _paymentGateway.ApprovePayment(shoppingCart);
            var transaction = new Transaction
                                  {
                                      GrossAmount = paymentGatewayResultDto.ApprovedTotal
                                  };
            _transactionsRepository.Save(transaction);
        }
    }
}