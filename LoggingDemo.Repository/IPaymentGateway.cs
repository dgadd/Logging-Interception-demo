using LoggingDemo.Domain;
using LoggingDemo.Domain.DataTransferObjects;

namespace LoggingDemo.Repository
{
    public interface IPaymentGateway
    {
        PaymentGatewayResultDTO ApprovePayment(ShoppingCart shoppingCart);
    }
}