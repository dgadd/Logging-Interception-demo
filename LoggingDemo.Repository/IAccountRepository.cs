using LoggingDemo.Domain;

namespace LoggingDemo.Repository
{
    public interface IAccountRepository
    {
        ShoppingCart GetCurrentShoppingCart(int accountId);
    }
}