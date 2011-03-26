using System;
using LoggingDemo.Domain;
using LoggingDemo.Domain.DataTransferObjects;
using LoggingDemo.Repository;

namespace LoggingDemo.Tests.Integration
{
    public class FakePaymentGateway : IPaymentGateway
    {
        public PaymentGatewayResultDTO ApprovePayment(ShoppingCart shoppingCart)
        {
            // happy path
            return new PaymentGatewayResultDTO
                       {
                           ApprovedTotal = shoppingCart.Total
                       };
        }
    }
}