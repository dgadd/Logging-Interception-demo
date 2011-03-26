using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingDemo.Domain;
using NUnit.Framework;

namespace LoggingDemo.Tests.Unit
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void TwoInstances_SameIdInput_AreEqual()
        {
            const int identityId = 13251235;
            var sut1 = new Transaction { Id = identityId };
            var sut2 = new Transaction { Id = identityId };
            Assert.AreEqual(sut1, sut2);
        }
    }
}
