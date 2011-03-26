using System;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
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

        public ProcessTransactionService()
        {
            var container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            _accountRepository = container.Resolve<IAccountRepository>();
            _transactionsRepository = container.Resolve<ITransactionsRepository>();
            _paymentGateway = container.Resolve<IPaymentGateway>();
        }

        public Transaction GetPaymentApprovalAndGenerateTransaction(int accountId)
        {
            var shoppingCart = _accountRepository.GetCurrentShoppingCart(accountId);
            var paymentGatewayResultDto = _paymentGateway.ApprovePayment(shoppingCart);
            var transaction = new Transaction
                                  {
                                      GrossAmount = paymentGatewayResultDto.ApprovedTotal
                                  };
            _transactionsRepository.Save(transaction);
            return transaction;
        }
    }
}