using LoggingDemo.Domain;

namespace LoggingDemo.Repository
{
    public interface ITransactionsRepository
    {
        void Save(Transaction transaction);
    }
}