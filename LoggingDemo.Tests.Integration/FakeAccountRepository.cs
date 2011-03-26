using System;
using LoggingDemo.Domain;
using LoggingDemo.Repository;

namespace LoggingDemo.Tests.Integration
{
    public class FakeAccountRepository : IAccountRepository
    {
        public ShoppingCart GetCurrentShoppingCart(int accountId)
        {
            // happy path
            return new ShoppingCart
            {
                Id = 12531515,
                Total = 35.00M,
                Account = new Account
                {
                    Id = accountId,
                    CreditCardInfo = new CreditCardInfo()
                }
            };
        }
    }
}