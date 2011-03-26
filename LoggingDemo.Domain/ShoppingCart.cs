using System;

namespace LoggingDemo.Domain
{
    public class ShoppingCart : PrimaryKeyBase
    {
        private Account _account;
        private decimal _total;

        public Account Account
        {
            get {
                return _account;
            }
            set {
                _account = value;
            }
        }

        public decimal Total
        {
            get {
                return _total;
            }
            set {
                _total = value;
            }
        }
    }
}