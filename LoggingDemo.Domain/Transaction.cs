using System;

namespace LoggingDemo.Domain
{
    public class Transaction : PrimaryKeyBase
    {
        private decimal _grossAmount;

        public decimal GrossAmount
        {
            get {
                return _grossAmount;
            }
            set {
                _grossAmount = value;
            }
        }
    }
}