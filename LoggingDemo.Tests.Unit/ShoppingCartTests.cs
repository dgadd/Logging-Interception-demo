using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingDemo.Domain;
using NUnit.Framework;

namespace LoggingDemo.Tests.Unit
{
    [TestFixture]
    public class ShoppingCartTests
    {
        [Test]
        public void TwoInstances_SameIdInput_AreEqual()
        {
            const int identityId = 13251235;
            var sut1 = new ShoppingCart { Id = identityId };
            var sut2 = new ShoppingCart { Id = identityId };
            Assert.AreEqual(sut1, sut2);
        }

        [Test]
        public void ToStringMethod_IdInput_ReturnsConcatPropertyDescription()
        {
            var sut = new ShoppingCart
                          {
                              Id = 12351235,
                              Account = new Account
                                            {
                                                Id = 1235,
                                                CreditCardInfo = new CreditCardInfo
                                                                     {
                                                                         Id = 1235123
                                                                     }
                                            },
                              Total = 35.27M
                          };
            const string expected = "[type:ShoppingCart] Id=12351235 Properties:Account=[type:Account] Id=1235 Properties:CreditCardInfo=[type:CreditCardInfo] Id=1235123 Properties:none||Total=35.27|";
            Assert.AreEqual(expected, sut.ToString());
        }
    }
}
