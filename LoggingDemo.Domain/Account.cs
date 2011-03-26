using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoggingDemo.Domain
{
    public class Account : PrimaryKeyBase
    {
        private CreditCardInfo _creditCardInfo;
        public CreditCardInfo CreditCardInfo
        {
            get { return _creditCardInfo; }
            set { _creditCardInfo = value; }
        }
    }
}
