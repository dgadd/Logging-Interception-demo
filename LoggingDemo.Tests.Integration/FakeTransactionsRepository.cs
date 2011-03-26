using System;
using LoggingDemo.Domain;
using LoggingDemo.Repository;

namespace LoggingDemo.Tests.Integration
{
    public class FakeTransactionsRepository : ITransactionsRepository
    {
        public void Save(Transaction transaction)
        {
            // happy path
            transaction.Id = 1253123;
        }
    }
}