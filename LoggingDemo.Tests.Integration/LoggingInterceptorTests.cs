using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoggingDemo.Utils;
using NUnit.Framework;

namespace LoggingDemo.Tests.Integration
{
    [TestFixture]
    public class LoggingInterceptorTests
    {
        [Test]
        public void LogNowMethod_StringInput_WritesToLogFile()
        {
            var loggingInterceptor = new LoggingInterceptor();
            loggingInterceptor.LogNow(this.GetType(), "test");
        }
    }
}
