using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingDemo.Domain;
using NUnit.Framework;

namespace LoggingDemo.Tests.Unit
{
    [TestFixture]
    public class PrimaryKeyBaseTests
    {
        [Test]
        public void TwoInstances_SameIdInput_AreEqual()
        {
            const int identityId = 13251235;
            var sut1 = new PrimaryKeyBase { Id = identityId };
            var sut2 = new PrimaryKeyBase { Id = identityId };
            Assert.AreEqual(sut1, sut2);
        }

        [Test]
        public void ToStringMethod_IdInput_ReturnsConcatPropertyDescription()
        {
            var sut = new PrimaryKeyBase {Id = 12351235};
            const string expected = "[type:PrimaryKeyBase] Id=12351235 Properties:none";
            Assert.AreEqual(expected, sut.ToString());
        }
    }
}
